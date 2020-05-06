// ReSharper disable ForCanBeConvertedToForeach
namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to resolve from the native container.
    /// </summary>
    [PublicAPI]
    public static class FluentNativeResolve
    {
        private static readonly object[] EmptyArgs = CoreExtensions.EmptyArray<object>();

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container)
        {
            var bucket = container.ResolversByType.Buckets[typeof(T).GetHashCode() & container.ResolversByType.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (typeof(T) == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return (
                container.TryGetResolver<T>(new Key(typeof(T)), out var resolver, out var error, container)
                    ? resolver
                    : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T)), error)
            )(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag) =>
            container.GetResolver<T>(tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>()(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(tag)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type) =>
            container.GetResolver<T>(type)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag) =>
            container.GetResolver<T>(type, tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(type)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(type, tag)(container, args);
    }
}