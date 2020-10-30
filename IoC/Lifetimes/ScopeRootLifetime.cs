namespace IoC.Lifetimes
{
    using System;

    public class ScopeRootLifetime: TrackedLifetime
    {
        [CanBeNull] private IDisposable _scopeToken;

        public override ILifetime CreateLifetime() => new ScopeRootLifetime();

        protected override void BeforeCreating(IContainer container, object[] args)
        {
            var scope = container.Resolve<IScope>();
            _scopeToken = scope.Activate();
            base.BeforeCreating(container, args);
        }

        protected override object AfterCreation(object newInstance, IContainer container, object[] args)
        {
            _scopeToken?.Dispose();
            return base.AfterCreation(newInstance, container, args);
        }
    }
}
