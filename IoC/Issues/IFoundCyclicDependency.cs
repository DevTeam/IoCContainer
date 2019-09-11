namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when a cyclic dependency was detected.
    /// </summary>
    [PublicAPI]
    public interface IFoundCyclicDependency
    {
        /// <summary>
        /// Resolves the scenario when a cyclic dependency was detected.
        /// </summary>
        /// <param name="key">The resolving key.</param>
        /// <param name="reentrancy">The level of reentrancy.</param>
        void Resolve(Key key, int reentrancy);
    }
}
