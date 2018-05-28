namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using static Core.CollectionExtensions;

    /// <summary>
    /// Represents extensions to resolve from a native container.
    /// </summary>
    [PublicAPI]
    public static class FluentNativeResolve
    {
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
            return ((Resolver<T>)container.ResolversByType.GetByRef(HashCode<T>.Shared, typeof(T))
                    ?? container.GetResolver<T>(typeof(T)))(container, Container.EmptyArgs);
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
        public static T Resolve<T>([NotNull] this Container container, Tag tag)
        {
            var key = new Key(typeof(T), tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(typeof(T), tag))(container, Container.EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(HashCode<T>.Shared, typeof(T))
                    ?? container.GetResolver<T>(typeof(T)))(container, args);
        }

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
        public static T Resolve<T>([NotNull] this Container container, Tag tag, [NotNull] [ItemCanBeNull] params object[] args)
        {
            var key = new Key(typeof(T), tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(typeof(T), tag))(container, args);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(type.GetHashCode(), typeof(T))
                    ?? container.GetResolver<T>(type))(container, Container.EmptyArgs);
        }

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
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag)
        {
            var key = new Key(type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(type, tag))(container, Container.EmptyArgs);
        }

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
        public static object Resolve<T>([NotNull] this Container container, [NotNull] Type type, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(type.GetHashCode(), typeof(T))
                    ?? container.GetResolver<T>(type))(container, args);
        }

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
        public static object Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag, [NotNull] [ItemCanBeNull] params object[] args)
        {
            var key = new Key(type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(type, tag))(container, args);
        }

        private static class HashCode<T>
        {
            public static readonly int Shared = typeof(T).GetHashCode();
        }
    }
}