namespace IoC
{
    using System;

    [PublicAPI]
    public interface IResolver: IDisposable
    {
        [NotNull] object Resolve(
            Key resolvingKey,
            [NotNull] IContainer resolvingContainer,
            int argsIndexOffset = 0,
            [NotNull][ItemCanBeNull] params object[] args);
    }
}
