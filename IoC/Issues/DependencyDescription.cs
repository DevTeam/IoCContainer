namespace IoC.Issues
{
    /// <summary>
    /// Represents the dependency.
    /// </summary>
    [PublicAPI]
    public struct DependencyDescription
    {
        /// <summary>
        /// The resolved dependency.
        /// </summary>
        [NotNull] public readonly IDependency Dependency;
        /// <summary>
        /// The lifetime to use.
        /// </summary>
        [CanBeNull] public readonly ILifetime Lifetime;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="dependency">The resolved dependency.</param>
        /// <param name="lifetime">The lifetime to use</param>
        public DependencyDescription([NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime)
        {
            Dependency = dependency;
            Lifetime = lifetime;
        }
    }
}
