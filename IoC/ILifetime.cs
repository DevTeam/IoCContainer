namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        [CanBeNull] object GetOrCreate(Context context, [NotNull] IFactory factory);
    }
}
