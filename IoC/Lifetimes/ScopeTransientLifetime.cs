namespace IoC.Lifetimes
{
    using System.Linq.Expressions;
    using Core;

    public sealed class ScopeTransientLifetime: ILifetime
    {
        public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public ILifetime CreateLifetime() => new ScopeTransientLifetime();

        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression.Type.Descriptor().IsDisposable())
            {
                return ExpressionBuilderExtensions.BuildGetOrCreateInstance(this, context, bodyExpression, nameof(GetOrCreateInstance));
            }

            return bodyExpression;
        }

        public void Dispose() { }

        internal T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            var instance = resolver(container, args);
            Scope.Current.Register(instance.AsDisposable());
            return instance;
        }
    }
}