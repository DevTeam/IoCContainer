namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract of configurable Inversion of Control container.
    /// </summary>
    [PublicAPI]

    public interface IMutableContainer: IContainer, IDisposable
    {
        /// <summary>
        /// Registers the dependency and the lifetime for the specified dependency key.
        /// </summary>
        /// <param name="keys">The set of keys to register.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="dependencyToken">The dependency token to unregister this dependency key.</param>
        /// <returns><c>True</c> if is registered successfully.</returns>
        bool TryRegisterDependency([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IToken dependencyToken);
    }
}
