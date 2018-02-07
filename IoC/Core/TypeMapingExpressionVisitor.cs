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
        private readonly bool _isConstructedGenericType;

        public TypeMapingExpressionVisitor([NotNull] Type type, [NotNull] IDictionary<Type, Type> typesMap)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
            _typeInfo = type.Info();
            _isConstructedGenericType = _typeInfo.IsConstructedGenericType;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            UpdateMap(node.Type);
            return base.VisitNew(node);
        }

        private void UpdateMap(Type targetType)
        {
            if (!_isConstructedGenericType)
            {
                return;
            }

            UpdateMap(_typeInfo, targetType.Info());
        }


        private void UpdateMap(ITypeInfo typeInfo, ITypeInfo targetTypeInfo)
        {
            var isConstructedGenericType = typeInfo.IsConstructedGenericType;
            ITypeInfo genTypeInfo = null;
            if (isConstructedGenericType)
            {
                genTypeInfo = typeInfo.GetGenericTypeDefinition().Info();
            }

            if (!isConstructedGenericType)
            {
                return;
            }

            if (!targetTypeInfo.IsConstructedGenericType)
            {
                return;
            }

            ITypeInfo realTargetTypeInfo = null;
            if (genTypeInfo.IsInterface)
            {
                realTargetTypeInfo = targetTypeInfo.ImplementedInterfaces.FirstOrDefault(t => genTypeInfo.Type == t.Info().GetGenericTypeDefinition())?.Info();
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

            if (realTargetTypeInfo != null)
            {
                var targetGenTypes = realTargetTypeInfo.GenericTypeArguments;
                var genTypes = typeInfo.GenericTypeArguments;
                if (targetGenTypes.Length != genTypes.Length)
                {
                    return;
                }

                for (var i = 0; i < targetGenTypes.Length; i++)
                {
                    _typesMap[targetGenTypes[i]] = genTypes[i];
                }
            }
        }
    }
}
