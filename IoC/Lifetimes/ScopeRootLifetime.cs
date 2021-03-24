namespace IoC.Lifetimes
{
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Automatically creates a new scope.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeRootLifetime: ILifetime
    {
        public ILifetime CreateLifetime() => new ScopeRootLifetime();

        public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression) =>
            ExpressionBuilderExtensions.BuildGetOrCreateInstance(this, context, bodyExpression, nameof(GetOrCreateInstance));

        internal T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            var scope = container.Resolve<IScope>();
            using (scope.Activate())
            {
                return  resolver(container, args);
            }
        }

        public void Dispose() { }
    }
}
