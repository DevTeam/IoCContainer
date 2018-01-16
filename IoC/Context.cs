namespace IoC
{
    [PublicAPI]
    public struct Context
    {
        [NotNull] public readonly Key Key;
        [NotNull] public readonly IContainer Container;
        [NotNull][ItemCanBeNull] public readonly object[] Args;

        public Context(
            [NotNull] Key key,
            [NotNull] IContainer container,
            [NotNull][ItemCanBeNull] params object[] args)
        {
            Key = key;
            Container = container;
            Args = args;
        }
    }
}
