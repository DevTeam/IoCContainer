namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IContainer: IDisposable
    {
        IContainer Parent { get; }

        bool TryRegister([NotNull][ItemNotNull] IEnumerable<Key> keys, [NotNull] IFactory factory, [CanBeNull] ILifetime lifetime, out IDisposable registrationToken);

        bool TryGetResolver([NotNull] Key key, out IResolver resolver);
    }
}
