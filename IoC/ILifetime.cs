namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of container lifetime.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IBuilder, IDisposable
    {
        /// <summary>
        /// Creates a similar lifetime to use with generic instances.
        /// </summary>
        /// <returns>The new lifetime instance.</returns>
        ILifetime CreateLifetime();

        /// <summary>
        /// Select a container to resolve dependencies.
        /// </summary>
        /// <param name="registrationContainer">The container where a dependency was registered.</param>
        /// <param name="resolvingContainer">The container which is used to resolve an instance.</param>
        /// <returns>The selected container.</returns>
        [NotNull] IContainer SelectContainer([NotNull] IContainer registrationContainer, [NotNull] IContainer resolvingContainer);
    }
}
