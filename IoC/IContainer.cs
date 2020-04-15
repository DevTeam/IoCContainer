namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract Inversion of Control container.
    /// </summary>
    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IResourceRegistry
    {
        /// <summary>
        /// Provides a parent container or <c>null</c> if it does not have a parent.
        /// </summary>
        [CanBeNull] IContainer Parent { get; }

        /// <summary>
        /// Provides a dependency and a lifetime for the registered key.
        /// </summary>
        /// <param name="key">The key to get a dependency.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>True if successful.</returns>
        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        /// <summary>
        /// Provides a resolver for a specific type and tag or error if something goes wrong.
        /// </summary>
        /// <typeparam name="T">The type of instance producing by the resolver.</typeparam>
        /// <param name="type">The binding type.</param>
        /// <param name="tag">The binding tag or null if there is no tag.</param>
        /// <param name="resolver">The resolver to get an instance.</param>
        /// <param name="error">Error that occurs when resolving.</param>
        /// <param name="resolvingContainer">The resolving container and null if the resolving container is the current container.</param>
        /// <returns><c>True</c> if successful and a resolver was provided.</returns>
        bool TryGetResolver<T>([NotNull] Type type, [CanBeNull] object tag, out Resolver<T> resolver, out Exception error, [CanBeNull] IContainer resolvingContainer = null);
    }
}
