namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal class TypeMappingExpressionVisitor : ExpressionVisitor
    {
        private readonly IDictionary<Type, Type> _typesMap;
        private readonly TypeDescriptor _typeDescriptor;

        public TypeMappingExpressionVisitor([NotNull] Type type, [NotNull] IDictionary<Type, Type> typesMap)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
            _typeDescriptor = type.Descriptor();
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
            UpdateMap(_typeDescriptor, targetType.Descriptor());
        }

        private void UpdateMap(TypeDescriptor typeDescriptor, TypeDescriptor targetTypeDescriptor)
        {
            var isConstructedGenericType = typeDescriptor.IsConstructedGenericType();
            TypeDescriptor genTypeDescriptor;
            if (isConstructedGenericType)
            {
                genTypeDescriptor = typeDescriptor.GetGenericTypeDefinition().Descriptor();
            }
            else
            {
                if (targetTypeDescriptor.IsConstructedGenericType())
                {
                    return;
                }

                if (targetTypeDescriptor.IsGenericTypeArgument())
                {
                    _typesMap[targetTypeDescriptor.AsType()] = typeDescriptor.AsType();
                }

                return;
            }

            if (!targetTypeDescriptor.IsConstructedGenericType())
            {
                return;
            }

            TypeDescriptor realTargetTypeDescriptor = null;
            if (genTypeDescriptor.IsInterface())
            {
                realTargetTypeDescriptor = targetTypeDescriptor.GetImplementedInterfaces().FirstOrDefault(t =>
                {
                    var curTypeDescriptor = t.Descriptor();
                    return curTypeDescriptor.IsConstructedGenericType() && genTypeDescriptor.AsType() == curTypeDescriptor.GetGenericTypeDefinition();
                })?.Descriptor();
            }
            else
            {
                var curType = targetTypeDescriptor;
                while (curType != null)
                {
                    if (!curType.IsConstructedGenericType())
                    {
                        break;
                    }

                    if (curType.GetGenericTypeDefinition() == genTypeDescriptor.AsType())
                    {
                        realTargetTypeDescriptor = curType;
                        break;
                    }

                    curType = curType.GetBaseType()?.Descriptor();
                }
            }

            if (realTargetTypeDescriptor == null)
            {
                realTargetTypeDescriptor = targetTypeDescriptor;
            }

            var targetGenTypes = realTargetTypeDescriptor.GetGenericTypeArguments();
            var genTypes = typeDescriptor.GetGenericTypeArguments();
            if (targetGenTypes.Length != genTypes.Length)
            {
                return;
            }

            for (var i = 0; i < targetGenTypes.Length; i++)
            {
                var targetType = targetGenTypes[i];
                var type = genTypes[i];
                targetTypeDescriptor = targetType.Descriptor();
                if (!targetTypeDescriptor.IsGenericTypeArgument())
                {
                    continue;
                }
                
                _typesMap[targetType] = type;
                UpdateMap(type.Descriptor(), targetTypeDescriptor);
            }
        }
    }
}
