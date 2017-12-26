namespace IoC.Internal
{
    internal interface IInstanceStore
    {
        [NotNull] object GetOrAdd([NotNull] IInstanceKey key, Context context, [NotNull] IFactory factory);
    }
}
