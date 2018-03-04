namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to resolve from a container.
    /// </summary>
    public static class FluentResolve
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static Resolver<T> GetResolver<T>(this IContainer container, Type type, object tag)
        {
            return container.TryGetResolver<T>(type, tag, out var resolver, container)
                ? resolver
                : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type, tag));
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container"></param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static Resolver<T> GetResolver<T>(this IContainer container, Type type)
        {
            return container.TryGetResolver<T>(type, out var resolver, container)
                ? resolver
                : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type));
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static T Resolve<T>([NotNull] this Container container)
        {
            var hashCode = HashCode<T>.Shared;
            var type = typeof(T);
            var tree = container.ResolversByType.Buckets[hashCode & (container.ResolversByType.Divisor - 1)];
            while (tree.Height != 0 && tree.Current.HashCode != hashCode)
            {
                tree = hashCode < tree.Current.HashCode ? tree.Left : tree.Right;
            }

            var treeEntry = tree.Current;
            if (tree.Height != 0 && ReferenceEquals(type, treeEntry.Key))
            {
                return ((Resolver<T>) treeEntry.Value)(container, Container.EmptyArgs);
            }

            var entryDuplicates = treeEntry.Duplicates;
            if (tree.Height != 0 && entryDuplicates != null)
            {
                for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                {
                    if (!ReferenceEquals(entryDuplicates[i].Key, type))
                    {
                        continue;
                    }

                    return ((Resolver<T>) entryDuplicates[i].Value)(container, Container.EmptyArgs);
                }
            }

            return container.GetResolver<T>(typeof(T))(container, Container.EmptyArgs);
        }


        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static T Resolve<T>([NotNull] this Container container, [NotNull][ItemCanBeNull] params object[] args)
        {
            var hashCode = HashCode<T>.Shared;
            var type = typeof(T);
            var tree = container.ResolversByType.Buckets[hashCode & (container.ResolversByType.Divisor - 1)];
            while (tree.Height != 0 && tree.Current.HashCode != hashCode)
            {
                tree = hashCode < tree.Current.HashCode ? tree.Left : tree.Right;
            }

            var treeEntry = tree.Current;
            if (tree.Height != 0 && ReferenceEquals(type, treeEntry.Key))
            {
                return ((Resolver<T>)treeEntry.Value)(container, args);
            }

            var entryDuplicates = treeEntry.Duplicates;
            if (tree.Height != 0 && entryDuplicates != null)
            {
                for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                {
                    if (!ReferenceEquals(entryDuplicates[i].Key, type))
                    {
                        continue;
                    }

                    return ((Resolver<T>)entryDuplicates[i].Value)(container, args);
                }
            }

            return container.GetResolver<T>(typeof(T))(container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args)
        {
            return container.GetResolver<T>(typeof(T))(container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static T Resolve<T>([NotNull] this IContainer container)
        {
            return container.GetResolver<T>(typeof(T))(container, Container.EmptyArgs);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static object Resolve([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
            return container.GetResolver<object>(type)(container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="type">The instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static object Resolve([NotNull] this IContainer container, [NotNull] Type type)
        {
            return container.GetResolver<object>(type)(container, Container.EmptyArgs);
        }

        private static class HashCode<T>
        {
            public static readonly int Shared = typeof(T).GetHashCode();
        }
    }
}