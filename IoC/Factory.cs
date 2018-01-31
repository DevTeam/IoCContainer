namespace IoC
{
    [PublicAPI]
    [NotNull] public delegate T Factory<out T>([NotNull] Key key, [NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args);
}
