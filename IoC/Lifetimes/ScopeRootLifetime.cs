namespace IoC.Lifetimes
{
    using System;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Automatically creates a new scope.
    /// </summary>
    [PublicAPI]
    internal sealed class ScopeRootLifetime: ILifetime
    {
        private readonly Func<IScope> _scopeFactory;

        public ScopeRootLifetime(Func<IScope> scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public ILifetime CreateLifetime() => new ScopeRootLifetime(_scopeFactory);

        public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression) =>
            ExpressionBuilderExtensions.BuildGetOrCreateInstance(this, context, bodyExpression, nameof(GetOrCreateInstance));

        internal T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            var scope = _scopeFactory();
            using (scope.Activate())
            {
                return resolver(scope.Container, args);
            }
        }

        public void Dispose() { }
    }
}
