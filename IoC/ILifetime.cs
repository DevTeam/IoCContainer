namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        [NotNull] T GetOrCreate<T>([NotNull] Key key, [NotNull] IContainer container, [NotNull][ItemCanBeNull] object[] args, [NotNull] Resolver<T> resolver);
    }
}
