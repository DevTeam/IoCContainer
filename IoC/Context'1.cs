namespace IoC
{
    [PublicAPI]
    public sealed class Context<T>: Context
    {
        public readonly T It;

        public Context(
            T it,
            Key key,
            [NotNull] IContainer container,
            [NotNull] [ItemCanBeNull] params object[] args)
            : base(key, container, args)
        {
            It = it;
        }
    }
}
