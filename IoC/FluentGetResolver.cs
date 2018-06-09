namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to get a resolver from a container.
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
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag)
            => container.TryGetResolver<T>(type, tag.Value, out var resolver, container) ? resolver : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type, tag));

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, Tag tag)
            => container.GetResolver<T>(TypeDescriptor<T>.Type, tag);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type)
            => container.TryGetResolver<T>(type, out var resolver, container) ? resolver : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type));

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container)
            => container.GetResolver<T>(TypeDescriptor<T>.Type);

        /// <summary>
        /// Creates tag.
        /// </summary>
        /// <param name="tagValue">The tage value.</param>
        /// <returns>The tag.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Tag AsTag([CanBeNull] this object tagValue) => new Tag(tagValue);
    }
}