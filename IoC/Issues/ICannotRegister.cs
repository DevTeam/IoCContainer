namespace IoC.Issues
{
    using System.Collections.Generic;

    /// <summary>
    /// Resolves the scenario when a new binding cannot be registered.
    /// </summary>
    [PublicAPI]
    public interface ICannotRegister
    {
        /// <summary>
        /// Resolves the scenario when a new binding cannot be registered.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="keys">The set of binding keys.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns>The dependency token.</returns>
        [NotNull] IToken Resolve([NotNull] IContainer container, [NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime);
    }
}
