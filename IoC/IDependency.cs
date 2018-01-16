namespace IoC
{
    using System;

    [PublicAPI]
    public interface IDependency
    {
        [NotNull] Type Type { get; }
    }
}
