namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class LifetimeExpressionBuilder : IExpressionBuilder<ILifetime>
    {
        public static readonly IExpressionBuilder<ILifetime> Shared = new LifetimeExpressionBuilder();

        private LifetimeExpressionBuilder() { }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, ILifetime lifetime)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            return lifetime?.Build(buildContext, bodyExpression) ?? bodyExpression;
        }
    }
}
