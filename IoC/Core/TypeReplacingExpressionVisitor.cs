namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class TypeReplacingExpressionVisitor: ExpressionVisitor
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;

        public TypeReplacingExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap)
        {
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeInfo = ReplaceType(node.Type).Info();
            var newConstructor = newTypeInfo.DeclaredConstructors.Single(i => Match(node.Constructor.GetParameters(), i.GetParameters()));
            var newArgs = ReplaceAll(node.Arguments).ToList();
            return Expression.New(newConstructor, newArgs);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var newType = ReplaceType(node.Type);
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    return Expression.Convert(node.Operand, newType);

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Info();
            var newMethod = newDeclaringType.DeclaredMethods.Single(i => Match(node.Method.GetParameters(), i.GetParameters()));
            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.Method.GetGenericArguments()));
            }

            var newArgs = ReplaceAll(node.Arguments).ToList();
            return node.Object != null ? Expression.Call(Visit(node.Object), newMethod, newArgs) : Expression.Call(newMethod, newArgs);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var newDeclaringType = ReplaceType(node.Member.DeclaringType).Info();
            var newMember = newDeclaringType.DeclaredMembers.Single(i => i.Name == node.Member.Name);
            return Expression.MakeMemberAccess(Visit(node.Expression), newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return Expression.Parameter(ReplaceType(node.Type), node.Name);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return Expression.Constant(node.Value, ReplaceType(node.Type));
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

                if (ReplaceType(baseParams[i].ParameterType).Info().Id != newParams[i].ParameterType.Info().Id)
                {
                    return false;
                }
            }

            return true;
        }

        private Type[] ReplaceTypes(Type[] types)
        {
            return types.Select(ReplaceType).ToArray();
        }

        private Type ReplaceType(Type type)
        {
            var baseTypeInfo = type.Info();
            if (!baseTypeInfo.IsConstructedGenericType)
            {
                if (_typesMap.TryGetValue(type, out var newType))
                {
                    return newType;
                }

                return type;
            }

            var newGenericTypes = new Type[baseTypeInfo.GenericTypeArguments.Length];
            var genericTypes = ReplaceTypes(baseTypeInfo.GenericTypeArguments);
            for (var i = 0; i < genericTypes.Length; i++)
            {
                var genericType = genericTypes[i];
                if (_typesMap.TryGetValue(genericType, out var newGenericType))
                {
                    newGenericTypes[i] = newGenericType;
                }
                else
                {
                    newGenericTypes[i] = genericType;
                }
            }

            return baseTypeInfo.GetGenericTypeDefinition().Info().MakeGenericType(newGenericTypes);
        }

        private IEnumerable<Expression> ReplaceAll(IEnumerable<Expression> expressions)
        {
            return expressions.Select(Visit);
        }
    }
}
