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
    public static class FluentNativeGetResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container)
        {
            var items = container.ResolversByType.GetBucket(TypeDescriptor<T>.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (typeof(T) == item.Key)
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(typeof(T), null, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T)), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, Tag tag)
        {
            var key = new Key(typeof(T), tag);
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(typeof(T), tag.Value, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T), tag), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type)
        {
            var items = container.ResolversByType.GetBucket(type.GetHashCode());
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (type == item.Key)
                {
                    return (Resolver<T>) item.Value;
                }
            }

            return container.TryGetResolver<T>(type, null, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type, Tag tag)
        {
            var key = new Key(type, tag);
            var items = container.Resolvers.GetBucket(key.HashCode);
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(type, tag.Value, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);
        }
    }
}