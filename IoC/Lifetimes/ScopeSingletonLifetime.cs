namespace IoC.Lifetimes
{
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// For a singleton instance per scope.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<IScope>
    {
        /// <inheritdoc />
        protected override IScope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime CreateLifetime() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override object AfterCreation(object newInstance, IScope scope, IContainer container, object[] args)
        {
            if (scope is IResourceRegistry resourceRegistry)
            {
                resourceRegistry.Register(newInstance.AsDisposable());
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnRelease(object releasedInstance, IScope scope) =>
            scope.UnregisterAndDispose(releasedInstance.AsDisposable());
    }
}
