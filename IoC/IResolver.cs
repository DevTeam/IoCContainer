namespace IoC
{
    using System;

    [PublicAPI]
    public interface IResolver
    {
        [CanBeNull] object Resolve([NotNull] IContainer resolvingContainer, [NotNull] Type contractType, [NotNull] params object[] args);
    }
}
