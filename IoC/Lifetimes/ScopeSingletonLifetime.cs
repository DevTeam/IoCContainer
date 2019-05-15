namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// Represents singleton per scope lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<Scope>
    {
        /// <inheritdoc />
        protected override Scope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, Scope scope, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                scope.RegisterResource(disposable);
            }

#if NETCOREAPP3_0
            if (newInstance is IAsyncDisposable asyncDisposable)
            {
                scope.RegisterResource(asyncDisposable.ToDisposable());
            }
#endif

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, Scope scope)
        {
            if (releasedInstance is IDisposable disposable)
            {
                scope.UnregisterResource(disposable);
                disposable.Dispose();
            }

#if NETCOREAPP3_0
            if (releasedInstance is IAsyncDisposable asyncDisposable)
            {
                disposable = asyncDisposable.ToDisposable();
                scope.UnregisterResource(disposable);
                disposable.Dispose();
            }
#endif
        }
    }
}
