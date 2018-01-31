namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IContainer: IEnumerable<Key>, IObservable<ContainerEvent>, IDisposable
    {
        [CanBeNull] IContainer Parent { get; }

        bool TryRegister([NotNull][ItemNotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IDisposable registrationToken);

        bool TryGetDependency([NotNull] Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        bool TryGetResolver<T>([NotNull] Key key, out Resolver<T> resolver, [CanBeNull] IContainer container = null);

        bool TryGet([NotNull] Type type, [CanBeNull] object tag, out object instance, [NotNull] [ItemCanBeNull] params object[] args);

        bool TryGet<T>([CanBeNull] object tag, out T instance, [NotNull] [ItemCanBeNull] params object[] args);
    }
}
