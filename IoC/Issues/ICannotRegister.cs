namespace IoC.Issues
{
    using System;

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
        /// <returns>The dependency token.</returns>
        [NotNull] IDisposable Resolve([NotNull] IContainer container, [NotNull] Key[] keys);
    }
}
