namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a IoC dependency.
    /// </summary>
    [PublicAPI]
    public interface IDependency
    {
        /// <summary>
        /// The expression for dependency which is used to create a build graph.
        /// </summary>
        [NotNull] Expression Expression { get; }
    }
}
