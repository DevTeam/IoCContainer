// ReSharper disable UnusedParameter.Global
namespace IoC
{
    /// <summary>
    /// Injection extensions.
    /// </summary>
    [PublicAPI]
    public static class Injections
    {
        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container)
        {
            return default(T);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container, [CanBeNull] object tag)
        {
            return default(T);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static void Inject<T>(this IContainer container, [NotNull] T destination, [CanBeNull] T source)
        {
        }
    }
}
