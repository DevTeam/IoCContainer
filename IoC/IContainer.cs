namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Inversion of Control container.
    /// </summary>
    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IDisposable
    {
        /// <summary>
        /// The parent container or null if it has no a parent.
        /// </summary>
        [CanBeNull] IContainer Parent { get; }

        /// <summary>
        /// Tries registering the binding to the target container.
        /// </summary>
        /// <param name="keys">The set of keys to register.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="registrationToken">The registration token.</param>
        /// <returns>True if successful.</returns>
        bool TryRegister([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IDisposable registrationToken);

        /// <summary>
        /// Tries getting the dependency and the its lifetime.
        /// </summary>
        /// <param name="key">The key to get a dependency.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>True if successful.</returns>
        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The type of instance producing by the resolver.</typeparam>
        /// <param name="type">The binding's type.</param>
        /// <param name="tag">The binding's tag or null if there is no tag.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="error">The error occurring during resolving.</param>
        /// <param name="resolvingContainer">The resolving container and null if it is the current container.</param>
        /// <returns>True if successful.</returns>
        bool TryGetResolver<T>([NotNull] Type type, [CanBeNull] object tag, out Resolver<T> resolver, out Exception error, [CanBeNull] IContainer resolvingContainer = null);
    }
}
