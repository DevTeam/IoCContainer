namespace IoC.Lifetimes
{
    using System.Linq.Expressions;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// Automatically calls a <c>Disposable()</c> method for disposable instances after a container has disposed.
    /// </summary>
    [PublicAPI]
    public sealed class DisposingLifetime: ILifetime
    {
        public ILifetime CreateLifetime() => new DisposingLifetime();

        public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression.Type.Descriptor().IsDisposable())
            {
                return ExpressionBuilderExtensions.BuildGetOrCreateInstance(this, context, bodyExpression, nameof(GetOrCreateInstance));
            }

            return bodyExpression;
        }

        public void Dispose()
        {
        }

        internal T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            var instance = resolver(container, args);
            // ReSharper disable once AssignNullToNotNullAttribute
            container.RegisterResource(instance.AsDisposable());
            return instance;
        }
    }
}
