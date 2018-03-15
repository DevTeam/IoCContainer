namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The IoC container.
    /// </summary>
    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IDisposable
    {
        /// <summary>
        /// The parent container.
        /// </summary>
        [CanBeNull] IContainer Parent { get; }

        /// <summary>
        /// Registers the binding to the target container.
        /// </summary>
        /// <param name="keys">The set of keys.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="registrationToken">The registration token.</param>
        /// <returns>True if successful.</returns>
        bool TryRegister([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IDisposable registrationToken);

        /// <summary>
        /// Registers the dependency and lifetime.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>True if successful.</returns>
        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="container">The resolving container.</param>
        /// <returns>True if successful.</returns>
        bool TryGetResolver<T>([NotNull] Type type, [CanBeNull] object tag, out Resolver<T> resolver, [CanBeNull] IContainer container = null);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="container">The resolving container.</param>
        /// <returns>True if successful.</returns>
        bool TryGetResolver<T>([NotNull] Type type, out Resolver<T> resolver, [CanBeNull] IContainer container = null);
    }
}
