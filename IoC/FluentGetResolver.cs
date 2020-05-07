namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Issues;

    /// <summary>
    /// Represents extensions to get a resolver from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentGetResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag) =>
            container is Container nativeContainer 
                ? nativeContainer.GetResolver<T>(type, tag)
                : container.TryGetResolver<T>(type, tag.Value, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container"></param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, tag.Value, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, Tag tag) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>(typeof(T), tag)
                : container.GetResolver<T>(typeof(T), tag);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(typeof(T), tag, out resolver);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>(type)
                : container.TryGetResolver<T>(type, null, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, null, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>()
                : container.GetResolver<T>(typeof(T));

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(typeof(T), out resolver);

        /// <summary>
        /// Creates tag.
        /// </summary>
        /// <param name="tagValue">The tag value.</param>
        /// <returns>The tag.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static Tag AsTag([CanBeNull] this object tagValue) => new Tag(tagValue);
    }
}