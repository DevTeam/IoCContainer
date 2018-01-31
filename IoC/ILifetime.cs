namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        [NotNull] T GetOrCreate<T>([NotNull] IContainer container, [NotNull] object[] args, [NotNull] Resolver<T> resolver);
    }
}
