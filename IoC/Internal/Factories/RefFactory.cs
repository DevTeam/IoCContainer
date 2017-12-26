namespace IoC.Internal.Factories
{
    internal class RefFactory : IFactory
    {
        private readonly Key _key;
        [CanBeNull] private IContainer _lastContainer;
        private IResolver _lastResolver;

        public RefFactory(Key key)
        {
            _key = key;
        }

        public object Create(Context context)
        {
            var container = context.ResolvingContainer;
            IResolver resolver;
            if (!Equals(_lastContainer, container))
            {
                if (!container.TryGetResolver(_key, out resolver))
                {
                    return context.ResolvingContainer.Get<IIssueResolver>().CannotResolve(container, _key);
                }

                _lastContainer = container;
                _lastResolver = resolver;
            }
            else
            {
                resolver = _lastResolver;
            }

            return resolver.Resolve(context.ResolvingContainer, _key.ContractType);
        }
    }
}
