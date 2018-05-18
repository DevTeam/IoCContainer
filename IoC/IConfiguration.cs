namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The container's configuration.
    /// </summary>
    [PublicAPI]
    public interface IConfiguration
    {
        /// <summary>
        /// Apply the configuration for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <returns>The enumeration of dependency tokens.</returns>
        [NotNull][ItemNotNull] IEnumerable<IDisposable> Apply([NotNull] IContainer container);
    }
}
