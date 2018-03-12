namespace IoC.Lifetimes
{
    using System;

    /// <summary>
    /// Represents singleton per scope lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: SingletonBasedLifetime<Scope>
    {
        /// <inheritdoc />
        protected override Scope CreateKey(IContainer container, object[] args)
        {
            return Scope.Current;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Lifetime.ScopeSingleton.ToString();
        }

        /// <inheritdoc />
        public override ILifetime Clone()
        {
            return new ScopeSingletonLifetime();
        }

        /// <inheritdoc />
        protected override void OnNewInstanceCreated<T>(T newInstance, Scope scope, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                scope.AddResource(disposable);
            }
        }
    }
}
