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
        /// <param name="buildContext">The build context.</param>
        void Resolve([NotNull] IBuildContext buildContext);
    }
}
