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
        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime CreateLifetime() => new ContainerSingletonLifetime();

        /// <inheritdoc />
        protected override object AfterCreation(object newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            targetContainer.Register(newInstance.AsDisposable());
            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnRelease(object releasedInstance, IContainer targetContainer) =>
            targetContainer.UnregisterAndDispose(releasedInstance.AsDisposable());
    }
}
