namespace IoC.Internal.Factories
{
    internal class ChildRefFactory : IFactory
    {
        private readonly Key _key;
        private readonly int _argsIndexOffset;

        public ChildRefFactory(Key key, int argsIndexOffset)
        {
            _key = key;
            _argsIndexOffset = argsIndexOffset;
        }

        public object Create(Context context)
        {
            using (var childContainer = context.ResolvingContainer.CreateChild())
            {
                return !childContainer.TryGetResolver(_key, out var resolver) ? context.ResolvingContainer.Get<IIssueResolver>().CannotResolve(childContainer, _key) : resolver.Resolve(context.ResolvingContainer, _key.ContractType, _argsIndexOffset, context.Args);
            }
        }
    }
}
