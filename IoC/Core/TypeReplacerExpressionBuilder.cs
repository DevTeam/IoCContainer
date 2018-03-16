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

        public Expression Build(Expression expression, IBuildContext buildContext, IDictionary<Type, Type> typesMap)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            typesMap = typesMap ?? new Dictionary<Type, Type>();
            var typeMapingExpressionVisitor = new TypeMapingExpressionVisitor(buildContext.Key.Type, typesMap);
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
