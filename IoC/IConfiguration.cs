namespace IoC
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract containers configuration.
    /// </summary>
    [PublicAPI]
    public interface IConfiguration
    {
        /// <summary>
        /// Applies a configuration to the target mutable container.
        /// </summary>
        /// <param name="container">The target mutable container to configure.</param>
        /// <returns>The enumeration of configuration tokens which allows to cancel that changes.</returns>
        [NotNull][ItemNotNull] IEnumerable<IToken> Apply([NotNull] IMutableContainer container);
    }
}
