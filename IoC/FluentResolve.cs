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
        /// Resolves an instance.
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
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args)
        {
            return container.GetResolver<T>(type)(container, args);
        }
    }
}