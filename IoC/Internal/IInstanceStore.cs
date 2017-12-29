namespace IoC.Internal
{
    internal interface IInstanceStore
    {
        [NotNull] object GetOrAdd<T>(T key, Context context, [NotNull] IFactory factory);
    }
}
