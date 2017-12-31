namespace IoC
{
    [PublicAPI]
    public interface IFactory
    {
        [NotNull] object Create(ResolvingContext context);
    }
}
