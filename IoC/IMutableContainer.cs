namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The configurable Inversion of Control container.
    /// </summary>
    [PublicAPI]

    public interface IMutableContainer: IContainer, IDisposable
    {
        /// <summary>
        /// Register the dependency with lifetime.
        /// </summary>
        /// <param name="keys">The set of keys to register.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="dependencyToken">The dependency token.</param>
        /// <returns>True if successful.</returns>
        bool TryRegisterDependency([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IToken dependencyToken);
    }
}
