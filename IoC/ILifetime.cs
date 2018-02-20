namespace IoC
{
    /// <summary>
    /// Represents a lifetime for an instance.
    /// </summary>
    [PublicAPI]
    public interface ILifetime
    {
        /// <summary>
        /// Clone this lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Clone();

        /// <summary>
        /// Gets or creates an instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="args">The resolving arguments.</param>
        /// <param name="resolver">The base resolver.</param>
        /// <returns>The instance.</returns>
        [NotNull] T GetOrCreate<T>([NotNull] IContainer container, [NotNull] object[] args, [NotNull] Resolver<T> resolver);
    }
}
