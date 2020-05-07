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
        [NotNull] private readonly Dictionary<string, ParameterExpression> _parameters = new Dictionary<string, ParameterExpression>();

        public TypeReplacerExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap)
        {
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeDescriptor = ReplaceType(node.Type).Descriptor();
            var newConstructor = newTypeDescriptor.GetDeclaredConstructors().Single(i => !i.IsPrivate && Match(node.Constructor.GetParameters(), i.GetParameters()));
            return Expression.New(newConstructor, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var newType = ReplaceType(node.Type);
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    var newOperand = Visit(node.Operand);
                    if (newOperand == null)
                    {
                        return base.VisitUnary(node);
                    }

                    return newOperand.Convert(newType);

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Descriptor();
            var newMethod = newDeclaringType.GetDeclaredMethods().SingleOrDefault(i => i.Name == node.Method.Name && Match(node.Method.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException($"Cannot find method {node.Method} in the {node.Method.DeclaringType}.", new InvalidOperationException());
            }

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
            if (newExpression == null)
            {
                return base.VisitMember(node);
            }

            return Expression.MakeMemberAccess(newExpression, newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node.Name, out var newNode))
            {
                return newNode;
            }

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (node.IsByRef)
            {
                newNode = Expression.Parameter(ReplaceType(node.Type).MakeByRefType(), node.Name);
            }
            else
            {
                newNode = Expression.Parameter(ReplaceType(node.Type), node.Name);
            }

            _parameters[node.Name] = newNode;
            return newNode;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Type type)
            {
                return Expression.Constant(ReplaceType(type), node.Type);
            }

            return Expression.Constant(node.Value, ReplaceType(node.Type));
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Select(VisitParameter).Cast<ParameterExpression>();
            var body = Visit(node.Body);
            if (body == null)
            {
                return base.VisitLambda(node);
            }

            return Expression.Lambda(ReplaceType(node.Type), body, parameters);
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
            if (newExpression == null)
            {
                return node;
            }

            return Expression.ListInit(newExpression, node.Initializers.Select(VisitInitializer));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Assign:
                    return Expression.Assign(Visit(node.Left) ?? throw new InvalidOperationException(), Visit(node.Right) ?? throw new InvalidOperationException());
            }

            return base.VisitBinary(node);
        }

        private ElementInit VisitInitializer(ElementInit node)
        {
            var newDeclaringType = ReplaceType(node.AddMethod.DeclaringType).Descriptor();
            var newMethod = newDeclaringType.GetDeclaredMethods().SingleOrDefault(i => i.Name == node.AddMethod.Name && Match(node.AddMethod.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException($"Cannot find method {node.AddMethod} in the {node.AddMethod.DeclaringType}.", new InvalidOperationException());
            }

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.AddMethod.GetGenericArguments()));
            }

            return Expression.ElementInit(newMethod, ReplaceAll(node.Arguments));
        }

        private bool Match(ParameterInfo[] baseParams, ParameterInfo[] newParams)
        {
            if (baseParams.Length != newParams.Length)
            {
                return false;
            }

            for (var i = 0; i < baseParams.Length; i++)
            {
                if (baseParams[i].Name != newParams[i].Name)
                {
                    return false;
                }

                var paramTypeDescriptor = newParams[i].ParameterType.Descriptor();
                if (paramTypeDescriptor.IsGenericParameter())
                {
                    return true;
                }

                if (ReplaceType(baseParams[i].ParameterType).Descriptor().GetId() != paramTypeDescriptor.GetId())
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private Type[] ReplaceTypes(Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
            {
                types[i] = ReplaceType(types[i]);
            }

            return types;
        }

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
