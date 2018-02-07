namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        ILifetime Clone();

        [NotNull] T GetOrCreate<T>([NotNull] IContainer container, [NotNull] object[] args, [NotNull] Resolver<T> resolver);
    }
}
