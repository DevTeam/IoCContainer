namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IContainer: IDisposable
    {
        [NotNull] IDisposable Register([NotNull] IEnumerable<Key> keys, [NotNull] IFactory factory, [CanBeNull] ILifetime lifetime = null);

        bool TryGetResolver(Key key, out IResolver resolver);
    }
}
