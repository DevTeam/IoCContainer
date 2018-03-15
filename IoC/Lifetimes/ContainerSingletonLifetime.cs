namespace IoC.Lifetimes
{
    using System;
    using Core;

    /// <summary>
    /// Represents singleton per container lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: SingletonBasedLifetime<IContainer>
    {
        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Clone() => new ContainerSingletonLifetime();

        /// <inheritdoc />
        protected override void OnNewInstanceCreated<T>(T newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable && targetContainer is IResourceStore resourceStore)
            {
                resourceStore.AddResource(disposable);
            }
        }
    }
}
