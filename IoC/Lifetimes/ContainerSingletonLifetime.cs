namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// For a singleton instance per container.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: KeyBasedLifetime<IContainer>
    {
        private readonly bool _supportOnNewInstanceCreated;
        private readonly bool _supportOnInstanceReleased;

        /// <summary>
        /// Creates new a new lifetime instance.
        /// </summary>
        public ContainerSingletonLifetime()
            : this(true, true)
        {
        }

        internal ContainerSingletonLifetime(bool supportOnNewInstanceCreated, bool supportOnInstanceReleased)
            : base(supportOnNewInstanceCreated, supportOnInstanceReleased)
        {
            _supportOnNewInstanceCreated = supportOnNewInstanceCreated;
            _supportOnInstanceReleased = supportOnInstanceReleased;
        }

        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerSingletonLifetime(_supportOnNewInstanceCreated, _supportOnInstanceReleased);

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

#if NET5 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (newInstance is IAsyncDisposable asyncDisposable)
            {
                targetContainer.RegisterResource(asyncDisposable.ToDisposable());
            }
#endif

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, IContainer targetContainer)
        {
            if (releasedInstance is IDisposable disposable)
            {
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }

#if NET5 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (releasedInstance is IAsyncDisposable asyncDisposable)
            {
                disposable = asyncDisposable.ToDisposable();
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }
#endif
        }
    }
}
