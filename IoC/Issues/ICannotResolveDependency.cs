namespace IoC.Issues
{
    /// <summary>
    /// Resolves issue with unknown dependency.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveDependency
    {
        /// <summary>
        /// Resolves the scenario when the dependency was not found.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <returns>The pair of the dependency and of the lifetime.</returns>
        DependencyDescription Resolve([NotNull] IBuildContext buildContext);
    }
}
