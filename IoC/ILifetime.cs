namespace IoC
{
    [PublicAPI]
    public interface ILifetime
    {
        [NotNull] object GetOrCreate(ResolvingContext context, [NotNull] IFactory factory);
    }
}
