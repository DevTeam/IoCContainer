﻿namespace IoC
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
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container)
        {
            var bucket = container.ResolversByType.Buckets[TypeDescriptor<T>.HashCode & container.ResolversByType.Divisor].KeyValues;
            foreach (var item in bucket)
            {
                if (typeof(T) == item.Key)
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(typeof(T)), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T)), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, Tag tag)
        {
            var key = new Key(typeof(T), tag);
            var bucket = container.Resolvers.Buckets[key.GetHashCode() & container.Resolvers.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (key.Equals(item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(typeof(T), tag.Value), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T), tag), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type)
        {
            var bucket = container.ResolversByType.Buckets[type.GetHashCode() & container.ResolversByType.Divisor].KeyValues;
            foreach (var item in bucket)
            {
                if (type == item.Key)
                {
                    return (Resolver<T>) item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(type), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type, Tag tag)
        {
            var key = new Key(type, tag);
            var bucket = container.Resolvers.Buckets[key.GetHashCode() & container.Resolvers.Divisor].KeyValues;
            foreach (var item in bucket)
            {
                if (key.Equals(item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(type, tag.Value), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);
        }
    }
}