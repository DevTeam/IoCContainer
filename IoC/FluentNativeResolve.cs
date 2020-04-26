// ReSharper disable ForCanBeConvertedToForeach
namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

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
            var items = container.ResolversByType.GetBucket(TypeDescriptor<T>.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (typeof(T) == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return container.GetResolver<T>(typeof(T))(container, EmptyArgs);
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
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return container.GetResolver<T>(typeof(T), tag)(container, EmptyArgs);
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
            var items = container.ResolversByType.GetBucket(TypeDescriptor<T>.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (typeof(T) == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, args);
                }
            }

            return container.GetResolver<T>(typeof(T))(container, args);
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
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return ((Resolver<T>)item.Value)(container, args);
                }
            }

            return container.GetResolver<T>(typeof(T), tag)(container, args);
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
            var items = container.ResolversByType.GetBucket(type.GetHashCode());
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (type == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return container.GetResolver<T>(type)(container, EmptyArgs);
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
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return container.GetResolver<T>(type, tag)(container, EmptyArgs);
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
            var items = container.ResolversByType.GetBucket(type.GetHashCode());
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (type == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, args);
                }
            }

            return container.GetResolver<T>(type)(container, args);
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
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return ((Resolver<T>)item.Value)(container, args);
                }
            }

            return container.GetResolver<T>(type, tag)(container, args);
        }
    }
}