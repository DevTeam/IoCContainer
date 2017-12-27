namespace IoC.Internal
{
    internal interface IInstanceStore
    {
        [NotNull] object GetOrAdd(object key, Context context, [NotNull] IFactory factory);
    }
}
