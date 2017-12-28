namespace IoC
{
    using System;

    [PublicAPI]
    public interface IResolver
    {
        [NotNull] object Resolve([NotNull] IContainer resolvingContainer, [NotNull] Type targetContractType, int argsIndexOffset = 0, [NotNull][ItemCanBeNull] params object[] args);
    }
}
