namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class TypeReplacerExpressionVisitor : ExpressionVisitor
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        [NotNull] private readonly Dictionary<ParameterExpression, ParameterExpression> _parameters = new Dictionary<ParameterExpression, ParameterExpression>();

        public TypeReplacerExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap) =>
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeDescriptor = ReplaceType(node.Type).Descriptor();
            if (newTypeDescriptor.IsAbstract() || newTypeDescriptor.Type == node.Type && node.Arguments.Count == 0)
            {
                return node;
            }

            var newConstructor = newTypeDescriptor.GetDeclaredConstructors().FirstOrDefault(i => !i.IsPrivate && Match(node.Constructor.GetParameters(), i.GetParameters()));
            if (newConstructor == null)
            {
                if (newTypeDescriptor.IsValueType())
                {
                    return Expression.Default(newTypeDescriptor.Type);
                }
                
                throw new BuildExpressionException($"Cannot find a constructor with parameters({string.Join(", ", node.Constructor.GetParameters().Select(i => i.Name))}) for type {newTypeDescriptor.Type}({newTypeDescriptor.Trace()}), replaced type: {node.Type.Descriptor().Trace()}.", null);
            }

            return Expression.New(newConstructor, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Descriptor();
            var newMethod = newDeclaringType
                .GetDeclaredMethods()
                .SingleOrDefault(i => i.Name == node.Method.Name && Match(node.Method.GetParameters(), i.GetParameters()))
                ?? throw new BuildExpressionException($"Cannot find method {node.Method} in the {node.Method.DeclaringType}.", new InvalidOperationException());

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.Method.GetGenericArguments()));
            }

            return node.Object != null 
                ? Expression.Call(Visit(node.Object), newMethod, ReplaceAll(node.Arguments))
                : Expression.Call(newMethod, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var newDeclaringTypeDescriptor = ReplaceType(node.Member.DeclaringType).Descriptor();
            if (newDeclaringTypeDescriptor.IsConstructedGenericType())
            {
                newDeclaringTypeDescriptor = ReplaceType(newDeclaringTypeDescriptor.AsType()).Descriptor();
            }

            var newMember = newDeclaringTypeDescriptor.GetDeclaredMembers().Single(i => i.Name == node.Member.Name);
            var newExpression = Visit(node.Expression);
            return newExpression == null ? base.VisitMember(node) : Expression.MakeMemberAccess(newExpression, newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node, out var newNode))
            {
                return newNode;
            }

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            var newType = ReplaceType(node.Type);
            if (node.IsByRef)
            {
                newType = newType.MakeByRefType();
            }

            if (node.Type == newType)
            {
                return node;
            }

            newNode = Expression.Parameter(newType, node.Name);
            _parameters[node] = newNode;
            return newNode;
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Type type)
            {
                return Expression.Constant(ReplaceType(type), node.Type);
            }

            var newType = ReplaceType(node.Type);
            if (newType == node.Type)
            {
                return node;
            }

            var value = node.Value;
            return node.Value == null ? (Expression) Expression.Default(newType) : Expression.Constant(value, newType);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Select(VisitParameter).Cast<ParameterExpression>();
            var body = Visit(node.Body);
            return body == null ? base.VisitLambda(node) : Expression.Lambda(ReplaceType(node.Type), body, parameters);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            var elementType = ReplaceType(node.Type.GetElementType());
            var elements = ReplaceAll(node.Expressions).Select(i => i.Convert(elementType));
            return Expression.NewArrayInit(elementType, elements);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var newExpression = (NewExpression)Visit(node.NewExpression);
            return newExpression == null ? node : Expression.ListInit(newExpression, node.Initializers.Select(VisitInitializer));
        }

        private ElementInit VisitInitializer(ElementInit node)
        {
            var newDeclaringType = ReplaceType(node.AddMethod.DeclaringType).Descriptor();
            var newMethod = newDeclaringType
                .GetDeclaredMethods()
                .SingleOrDefault(i => i.Name == node.AddMethod.Name && Match(node.AddMethod.GetParameters(), i.GetParameters()))
                ?? throw new BuildExpressionException($"Cannot find method \"{node.AddMethod.Name}\" in the {node.AddMethod.DeclaringType}.", new InvalidOperationException());

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.AddMethod.GetGenericArguments()));
            }

            return Expression.ElementInit(newMethod, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    return Expression.Convert(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.ConvertChecked:
                    return Expression.ConvertChecked(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.Unbox:
                    return Expression.Unbox(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.TypeAs:
                    return Expression.TypeAs(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.TypeIs:
                    return Expression.TypeIs(Visit(node.Operand), ReplaceType(node.Type));

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node) =>
            Expression.TypeIs(Visit(node.Expression), ReplaceType(node.TypeOperand));

        protected override Expression VisitConditional(ConditionalExpression node) =>
            Expression.Condition(Visit(node.Test), Visit(node.IfTrue), Visit(node.IfFalse), ReplaceType(node.Type));

        [MethodImpl((MethodImplOptions)0x200)]
        private bool Match(IList<ParameterInfo> baseParams, IList<ParameterInfo> newParams)
        {
            if (baseParams.Count != newParams.Count)
            {
                return false;
            }

            for (var i = 0; i < baseParams.Count; i++)
            {
                var baseParam = baseParams[i];
                var newParam = newParams[i];
                if (baseParam.Name != newParam.Name)
                {
                    return false;
                }

                var newParamTypeDescriptor = newParam.ParameterType.Descriptor();
                if (newParamTypeDescriptor.IsGenericParameter())
                {
                    return true;
                }

                var baseParamType = ReplaceType(baseParam.ParameterType).Descriptor();
                if (baseParamType.IsGenericTypeDefinition())
                {
                    baseParamType = baseParamType.GetGenericTypeDefinition().Descriptor();
                }

                string baseParamAssembly = null;
                string newParamAssembly = null;
                try
                {
                    baseParamAssembly = baseParamType.GetAssembly().FullName;
                    newParamAssembly = newParamTypeDescriptor.GetAssembly().FullName;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }

                if (!Equals(baseParamType.Type.Name, newParamTypeDescriptor.Type.Name) || !Equals(baseParamAssembly, newParamAssembly))
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private Type[] ReplaceTypes(Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
            {
                types[i] = ReplaceType(types[i]);
            }

            return types;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private Type ReplaceType(Type type)
        {
            if (_typesMap.TryGetValue(type, out var newType))
            {
                return newType;
            }

            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsArray())
            {
                var elementType = typeDescriptor.GetElementType();
                var newElementType = ReplaceType(typeDescriptor.GetElementType());
                if (elementType != newElementType)
                {
                    return newElementType.MakeArrayType();
                }

                return type;
            }

            if (typeDescriptor.IsConstructedGenericType())
            {
                var genericTypes = ReplaceTypes(typeDescriptor.GetGenericTypeArguments());
                return typeDescriptor.GetGenericTypeDefinition().Descriptor().MakeGenericType(ReplaceTypes(genericTypes));
            }

            return type;
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private IEnumerable<Expression> ReplaceAll(IEnumerable<Expression> expressions) => 
            expressions.Select(Visit);
    }
}
