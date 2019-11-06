namespace IoC.Issues
{
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
        [NotNull] IToken Resolve([NotNull] IContainer container, [NotNull] Key[] keys);
    }
}
