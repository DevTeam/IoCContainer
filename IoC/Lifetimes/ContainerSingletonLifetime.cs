namespace IoC.Lifetimes
{
    using System;

    /// <summary>
    /// Represents singleton per container lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: SingletonBasedLifetime<IContainer>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        /// <inheritdoc />
        public ContainerSingletonLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory)
            : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args)
        {
            return container;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Lifetime.ContainerSingleton.ToString();
        }

        /// <inheritdoc />
        public override ILifetime Clone()
        {
            return new ContainerSingletonLifetime(_singletonLifetimeFactory);
        }
    }
}
