namespace IoC.Lifetimes
{
    using System;
    using Core; // ReSharper disable once RedundantUsingDirective

    /// <summary>
    /// For a singleton instance per state.
    /// </summary>
    internal sealed class ContainerStateSingletonLifetime<TValue> : KeyBasedLifetime<IContainer, TValue>
        where TValue : class
    {
        private readonly bool _isDisposable;
        private IDisposable _containerSubscription = Disposable.Empty;

        public ContainerStateSingletonLifetime(bool isDisposable)
        {
            _isDisposable = isDisposable;
        }

        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerStateSingletonLifetime<TValue>(_isDisposable);

        /// <inheritdoc />
        protected override TValue OnNewInstanceCreated(TValue newInstance, IContainer targetContainer, IContainer container, object[] args)
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

            if (_isDisposable && newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(TValue releasedInstance, IContainer targetContainer)
        {
            _containerSubscription.Dispose();
            if (_isDisposable && releasedInstance is IDisposable disposable)
            {
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _containerSubscription.Dispose();
            base.Dispose();
        }
    }
}
