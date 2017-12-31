namespace IoC.Internal
{
    internal interface IInstanceStore
    {
        [NotNull] object GetOrAdd<T>(T key, ResolvingContext context, [NotNull] IFactory factory);
    }
}
