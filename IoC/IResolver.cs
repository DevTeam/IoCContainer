namespace IoC
{
    [PublicAPI]
    public interface IResolver
    {
        [NotNull] object Resolve(
            [NotNull] Key resolvingKey,
            [NotNull] IContainer resolvingContainer,
            int argsIndexOffset = 0,
            [NotNull][ItemCanBeNull] params object[] args);
    }
}
