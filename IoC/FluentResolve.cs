namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to resolve from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentResolve
    {
        private static readonly object[] EmptyArgs = CoreExtensions.EmptyArray<object>();

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container)
        {
            return container.GetResolver<T>()(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args) 
            => container.GetResolver<T>()(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, Tag tag)
            => container.GetResolver<T>(tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, Tag tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(tag)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type)
            => container.GetResolver<T>(type)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args) 
            => container.GetResolver<T>(type)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag)
            => container.GetResolver<T>(type, tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(type, tag)(container, args);
    }
}