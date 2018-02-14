namespace IoC.Core.Lifetimes
{
    using System;

    internal sealed class ContainerSingletonLifetime: SingletonBasedLifetime<IContainer>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        public ContainerSingletonLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory)
            : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        protected override IContainer CreateKey(IContainer container, object[] args)
        {
            return container;
        }

        public override string ToString()
        {
            return Lifetime.ContainerSingleton.ToString();
        }

        public override ILifetime Clone()
        {
            return new ContainerSingletonLifetime(_singletonLifetimeFactory);
        }
    }
}
