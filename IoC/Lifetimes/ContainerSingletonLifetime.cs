﻿namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// For a singleton instance per container.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: KeyBasedLifetime<IContainer, object>
    {
        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerSingletonLifetime();

        /// <inheritdoc />
        protected override object OnNewInstanceCreated(object newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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
