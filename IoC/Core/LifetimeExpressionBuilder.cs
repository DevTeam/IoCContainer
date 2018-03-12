namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Extensibility;

    internal class LifetimeExpressionBuilder : IExpressionBuilder<ILifetime>
    {
        public static readonly IExpressionBuilder<ILifetime> Shared = new LifetimeExpressionBuilder();

        private LifetimeExpressionBuilder()
        {
        }

        public Expression Build(Expression expression, BuildContext buildContext, ILifetime lifetime)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (lifetime == null)
            {
                return expression;
            }

            var resolverType = expression.Type.ToResolverType();
            var resolverExpression = Expression.Lambda(resolverType, expression, false, BuildContext.ResolverParameters);
            var resolver = buildContext.Compiler.Compile(resolverExpression);
            var resolverVar = buildContext.DefineValue(resolver, resolverType);
            return lifetime.Build(expression, buildContext, resolverVar);
        }
    }
}
