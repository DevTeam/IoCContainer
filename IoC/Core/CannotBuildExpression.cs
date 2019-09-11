namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Issues;

    internal class CannotBuildExpression : ICannotBuildExpression
    {
        public static readonly ICannotBuildExpression Shared = new CannotBuildExpression();

        private CannotBuildExpression() { }

        public Expression Resolve(IBuildContext buildContext, IDependency dependency, ILifetime lifetime, Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            throw new InvalidOperationException($"Cannot build expression for the key {buildContext.Key} in the container {buildContext.Container}.", error);
        }
    }
}