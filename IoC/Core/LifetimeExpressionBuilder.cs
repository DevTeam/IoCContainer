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

        public Expression Build(Expression expression, IBuildContext buildContext, ILifetime lifetime)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            return lifetime?.Build(expression, buildContext) ?? expression;
        }
    }
}
