namespace IoC.Lifetimes
{
    using System;

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
        public override ILifetime Clone() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, Scope scope, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                scope.AddResource(disposable);
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, Scope scope)
        {
            if (releasedInstance is IDisposable disposable)
            {
                scope.RemoveResource(disposable);
                disposable.Dispose();
            }
        }
    }
}
