namespace IoC.Internal.Factories
{
    internal class ParentRefFactory : IFactory
    {
        private readonly Key _key;
        private readonly int _argsIndexOffset;
        [CanBeNull] private IContainer _lastContainer;
        private IResolver _lastResolver;

        public ParentRefFactory(Key key, int argsIndexOffset)
        {
            _key = key;
            _argsIndexOffset = argsIndexOffset;
        }

        public object Create(ResolvingContext context)
        {
            var container = context.ResolvingContainer.Parent;
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

            return resolver.Resolve(_key, context.ResolvingContainer, _argsIndexOffset, context.Args);
        }
    }
}
