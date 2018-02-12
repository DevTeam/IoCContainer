namespace IoC.Core.Lifetimes
{
    using System;

    internal sealed class ScopeLifetime: KeyBasedLifetime<object>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        public ScopeLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory) : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        protected override object CreateKey(IContainer container, object[] args)
        {
            return Scope.Current.ScopeKey;
        }

        public override string ToString()
        {
            return Lifetime.Scope.ToString();
        }

        public override ILifetime Clone()
        {
            return new ScopeLifetime(_singletonLifetimeFactory);
        }
    }
}
