namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        [NotNull] object GetOrCreate(Context context, [NotNull] IFactory factory);
    }
}
