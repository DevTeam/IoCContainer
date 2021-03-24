namespace IoC.Lifetimes
{
    using System;
    using Core; // ReSharper disable once RedundantUsingDirective

    /// <summary>
    /// For a singleton instance per state.
    /// </summary>
    internal sealed class ContainerStateSingletonLifetime : KeyBasedLifetime<IContainer>
    {
        private readonly bool _isDisposable;
        private IDisposable _containerSubscription = Disposable.Empty;

        public ContainerStateSingletonLifetime(bool isDisposable) =>
            _isDisposable = isDisposable;

        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime CreateLifetime() => new ContainerStateSingletonLifetime(_isDisposable);

        /// <inheritdoc />
        protected override object AfterCreation(object newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            _containerSubscription = container.Subscribe(
                value =>
                {
                    if (value.IsSuccess && (value.EventType == EventType.RegisterDependency || value.EventType == EventType.ContainerStateSingletonLifetime))
                    {
                        var curContainer = targetContainer;
                        while (curContainer != null && !Remove(container))
                        {
                            curContainer = curContainer.Parent;
                        }
                    }
                },
                e => { },
                () => { });

            if (_isDisposable)
            {
                targetContainer.Register(newInstance.AsDisposable());
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnRelease(object releasedInstance, IContainer targetContainer)
        {
            _containerSubscription.Dispose();
            targetContainer.UnregisterAndDispose(releasedInstance.AsDisposable());
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _containerSubscription.Dispose();
            base.Dispose();
        }
    }
}
