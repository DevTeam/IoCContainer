namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class Dependency: IDependency
    {
        public Dependency([NotNull] Expression expression)
        {
            Expression = (expression as LambdaExpression ?? throw new ArgumentException(nameof(expression))).Body;
        }

        public Expression Expression { get; }
    }
}
