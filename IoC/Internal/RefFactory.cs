namespace IoC.Internal
{
    using System;

    internal struct RefFactory : IFactory
    {
        [NotNull] private readonly Type _contractType;
        private readonly Key _key;
        [NotNull] private readonly ContainerSelector _containerSelector;
        private bool _isDisposingContainer;
        [CanBeNull] private IContainer _lastContainer;
        private IResolver _lastResolver;

        public RefFactory(
            [NotNull] Type contractType,
            object tagValue,
            Scope scope)
        {
            _contractType = contractType;
            _key = new Key(contractType, tagValue);
            _lastContainer = null;
            _lastResolver = null;
            _isDisposingContainer = false;
            switch (scope)
            {
                case Scope.Current:
                    _containerSelector = context => context.ResolvingContainer;
                    break;

                case Scope.Parent:
                    _containerSelector = context => context.ResolvingContainer.Parent;
                    break;

                case Scope.Child:
                    _containerSelector = context => context.ResolvingContainer.CreateChild();
                    _isDisposingContainer = true;
                    break;

                default:
                    throw new NotSupportedException($"The scope \"{scope}\" is not supported.");
            }
        }

        public object Create(Context context)
        {
            var container = _containerSelector(context);
            IResolver resolver;
            if (!Equals(_lastContainer, container))
            {
                if (!container.TryGetResolver(_key, out resolver))
                {
                    return context.ResolvingContainer.Get<IIssueResolver>().CannotResolve(container, _key);
                }

                if (!_isDisposingContainer)
                {
                    _lastContainer = container;
                    _lastResolver = resolver;
                }
            }
            else
            {
                resolver = _lastResolver;
            }

            try
            {
                return resolver.Resolve(context.ResolvingContainer, _contractType);
            }
            finally
            {
                if (_isDisposingContainer)
                {
                    container.Dispose();
                }
            }
        }

        private delegate IContainer ContainerSelector(Context context);
    }
}
