namespace IoC
{
    [PublicAPI]
    public interface IResolver
    {
        [NotNull] object Resolve(
            Key resolvingKey,
            [NotNull] IContainer resolvingContainer,
            int argsIndexOffset = 0,
            [NotNull][ItemCanBeNull] params object[] args);
    }
}
