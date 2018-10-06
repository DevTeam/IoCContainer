namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class TypeReplacerExpressionBuilder : IExpressionBuilder<IDictionary<Type, Type>>
    {
        public static readonly IExpressionBuilder<IDictionary<Type, Type>> Shared = new TypeReplacerExpressionBuilder();

        private TypeReplacerExpressionBuilder() { }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, IDictionary<Type, Type> typesMap)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            typesMap = typesMap ?? new Dictionary<Type, Type>();
            if (bodyExpression.Type != buildContext.Key.Type)
            {
                TypeMapper.Shared.Map(bodyExpression.Type, buildContext.Key.Type, typesMap);
                if (typesMap.Count > 0)
                {
                    var typeReplacingExpressionVisitor = new TypeReplacerExpressionVisitor(typesMap);
                    var newExpression = typeReplacingExpressionVisitor.Visit(bodyExpression);
                    if (newExpression != null)
                    {
                        return newExpression;
                    }
                }
            }

            return bodyExpression;
        }
    }
}
