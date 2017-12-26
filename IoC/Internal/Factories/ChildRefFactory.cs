namespace IoC.Internal.Factories
{
    internal class ChildRefFactory : IFactory
    {
        private readonly Key _key;

        public ChildRefFactory(Key key)
        {
            _key = key;
        }

        public object Create(Context context)
        {
            using (var childContainer = context.ResolvingContainer.CreateChild())
            {
                return !childContainer.TryGetResolver(_key, out var resolver) ? context.ResolvingContainer.Get<IIssueResolver>().CannotResolve(childContainer, _key) : resolver.Resolve(context.ResolvingContainer, _key.ContractType);
            }
        }
    }
}
