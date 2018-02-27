namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal class TypeMapingExpressionVisitor : ExpressionVisitor
    {
        private readonly IDictionary<Type, Type> _typesMap;
        private readonly ITypeInfo _typeInfo;

        public TypeMapingExpressionVisitor([NotNull] Type type, [NotNull] IDictionary<Type, Type> typesMap)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
            _typeInfo = type.Info();
        }

        public override Expression Visit(Expression node)
        {
            if (node != null)
            {
                UpdateMap(node.Type);
            }

            return base.Visit(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Type type)
            {
                UpdateMap(type);
            }

            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            foreach (var nodeArgument in node.Arguments)
            {
                Visit(nodeArgument);
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            UpdateMap(node.ReturnType);
            return base.VisitLambda(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Visit(node.Operand);
            return base.VisitUnary(node);
        }

        private void UpdateMap(Type targetType)
        {
            UpdateMap(_typeInfo, targetType.Info());
        }

        private void UpdateMap(ITypeInfo typeInfo, ITypeInfo targetTypeInfo)
        {
            var isConstructedGenericType = typeInfo.IsConstructedGenericType;
            ITypeInfo genTypeInfo;
            if (isConstructedGenericType)
            {
                genTypeInfo = typeInfo.GetGenericTypeDefinition().Info();
            }
            else
            {
                if (targetTypeInfo.IsConstructedGenericType)
                {
                    return;
                }

                if (targetTypeInfo.IsGenericTypeArgument())
                {
                    _typesMap[targetTypeInfo.Type] = typeInfo.Type;
                }

                return;
            }

            if (!targetTypeInfo.IsConstructedGenericType)
            {
                return;
            }

            ITypeInfo realTargetTypeInfo = null;
            if (genTypeInfo.IsInterface)
            {
                realTargetTypeInfo = targetTypeInfo.ImplementedInterfaces.FirstOrDefault(t =>
                {
                    var curTypeInfo = t.Info();
                    return curTypeInfo.IsConstructedGenericType && genTypeInfo.Type == curTypeInfo.GetGenericTypeDefinition();
                })?.Info();
            }
            else
            {
                var curType = targetTypeInfo;
                while (curType != null)
                {
                    if (!curType.IsConstructedGenericType)
                    {
                        break;
                    }

                    if (curType.GetGenericTypeDefinition() == genTypeInfo.Type)
                    {
                        realTargetTypeInfo = curType;
                        break;
                    }

                    curType = curType.BaseType?.Info();
                }
            }

            if (realTargetTypeInfo == null)
            {
                realTargetTypeInfo = targetTypeInfo;
            }

            var targetGenTypes = realTargetTypeInfo.GenericTypeArguments;
            var genTypes = typeInfo.GenericTypeArguments;
            if (targetGenTypes.Length != genTypes.Length)
            {
                return;
            }

            for (var i = 0; i < targetGenTypes.Length; i++)
            {
                var targetType = targetGenTypes[i];
                var type = genTypes[i];
                targetTypeInfo = targetType.Info();
                if (!targetTypeInfo.IsGenericTypeArgument())
                {
                    continue;
                }
                
                _typesMap[targetType] = type;
                UpdateMap(type.Info(), targetTypeInfo);
            }
        }
    }
}
