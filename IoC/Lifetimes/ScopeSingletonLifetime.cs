namespace IoC.Lifetimes
{
    using System;

    /// <summary>
    /// Represents singleton per scope lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: SingletonBasedLifetime<object>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        /// <inheritdoc />
        public ScopeSingletonLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory) : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        /// <inheritdoc />
        protected override object CreateKey(IContainer container, object[] args)
        {
            return Scope.Current.ScopeKey;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Lifetime.ScopeSingleton.ToString();
        }

        /// <inheritdoc />
        public override ILifetime Clone()
        {
            return new ScopeSingletonLifetime(_singletonLifetimeFactory);
        }
    }
}
