namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of the resource registry.
    /// </summary>
    [PublicAPI]
    public interface IResourceRegistry
    {
        /// <summary>
        /// Registers a resource to the registry.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        void RegisterResource([NotNull] IDisposable resource);

        /// <summary>
        /// Unregisters a resource from the registry.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        void UnregisterResource([NotNull] IDisposable resource);
    }
}
