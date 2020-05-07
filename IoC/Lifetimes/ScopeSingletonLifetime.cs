namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// For a singleton instance per scope.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<IScope, object>
    {
        /// <inheritdoc />
        protected override IScope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override object OnNewInstanceCreated(object newInstance, IScope scope, IContainer container, object[] args)
        {
            if (!(scope is IResourceRegistry resourceRegistry))
            {
                return newInstance;
            }

            if (newInstance is IDisposable disposable)
            {
                resourceRegistry.RegisterResource(disposable);
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (newInstance is IAsyncDisposable asyncDisposable)
                {
                    resourceRegistry.RegisterResource(asyncDisposable.ToDisposable());
                }
#endif

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, IScope scope)
        {
            if (!(scope is IResourceRegistry resourceRegistry))
            {
                return;
            }

            if (releasedInstance is IDisposable disposable)
            {
                resourceRegistry.UnregisterResource(disposable);
                disposable.Dispose();
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (releasedInstance is IAsyncDisposable asyncDisposable)
            {
                disposable = asyncDisposable.ToDisposable();
                resourceRegistry.UnregisterResource(disposable);
                disposable.Dispose();
            }
#endif
        }
    }
}
