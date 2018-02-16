namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IDisposable
    {
        [CanBeNull] IContainer Parent { get; }

        bool TryRegister([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IDisposable registrationToken);

        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        bool TryGetResolver<T>([NotNull] Type type, [CanBeNull] object tag, out Resolver<T> resolver, [CanBeNull] IContainer container = null);

        bool TryGetResolver<T>([NotNull] Type type, out Resolver<T> resolver, [CanBeNull] IContainer container = null);

        [NotNull] Resolver<T> GetResolver<T>([NotNull] Type type, [CanBeNull] object tag, [CanBeNull] IContainer container = null);

        [NotNull] Resolver<T> GetResolver<T>([NotNull] Type type, [CanBeNull] IContainer container = null);
    }
}
