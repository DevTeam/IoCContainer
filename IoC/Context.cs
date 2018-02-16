namespace IoC
{
    [PublicAPI]
    public class Context
    {
        public readonly Key Key;
        [NotNull] public readonly IContainer Container;
        [NotNull][ItemCanBeNull] public readonly object[] Args;

        public Context(
            Key key,
            [NotNull] IContainer container,
            [NotNull][ItemCanBeNull] params object[] args)
        {
            Key = key;
            Container = container;
            Args = args;
        }
    }
}
