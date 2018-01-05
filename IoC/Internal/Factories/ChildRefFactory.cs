namespace IoC.Internal.Factories
{
    internal sealed class ChildRefFactory : IFactory
    {
        private readonly Key _key;
        private readonly int _argsIndexOffset;

        public ChildRefFactory(Key key, int argsIndexOffset)
        {
            _key = key;
            _argsIndexOffset = argsIndexOffset;
        }

        public object Create(ResolvingContext context)
        {
            var childContainer = context.ResolvingContainer.CreateChild();
            return !childContainer.TryGetResolver(_key, out var resolver) ? context.ResolvingContainer.Get<IIssueResolver>().CannotResolve(childContainer, _key) : resolver.Resolve(_key, context.ResolvingContainer, _argsIndexOffset, context.Args);
        }
    }
}
