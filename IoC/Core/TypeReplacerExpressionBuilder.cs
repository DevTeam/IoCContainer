namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Extensibility;

    internal class TypeReplacerExpressionBuilder : IExpressionBuilder<IDictionary<Type, Type>>
    {
        public static readonly IExpressionBuilder<IDictionary<Type, Type>> Shared = new TypeReplacerExpressionBuilder();

        private TypeReplacerExpressionBuilder()
        {
        }

        public Expression Build(Expression expression, Key key, IContainer container, IDictionary<Type, Type> typesMap)
        {
            typesMap = typesMap ?? new Dictionary<Type, Type>();
            var typeMapingExpressionVisitor = new TypeMapingExpressionVisitor(key.Type, typesMap);
            typeMapingExpressionVisitor.Visit(expression);
            if (typesMap.Count > 0)
            {
                var typeReplacingExpressionVisitor = new TypeReplacerExpressionVisitor(typesMap);
                var newExpression = typeReplacingExpressionVisitor.Visit(expression);
                if (newExpression != null)
                {
                    return newExpression;
                }
            }

            return expression;
        }
    }
}
