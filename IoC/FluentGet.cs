namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to get an instance from a continer.
    /// </summary>
    public static class FluentGet
    {
        /// <summary>
        /// Specifies the tag of the instance.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static Resolving Tag([NotNull] this IContainer container, [CanBeNull] object tag)
        {
            return new Resolving(container, tag);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="resolving"></param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static T Get<T>(this Resolving resolving, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return resolving.Container.GetResolver<T>(typeof(T), resolving.Tag)(resolving.Container, args);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="resolving"></param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static T Get<T>(this Resolving resolving, [NotNull] Type type, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return resolving.Container.GetResolver<T>(type, resolving.Tag)(resolving.Container, args);
        }

        /// <summary>
        /// Represents the resolving token.
        /// </summary>
        public struct Resolving
        {
            /// <summary>
            /// The target container.
            /// </summary>
            [NotNull] public readonly IContainer Container;

            /// <summary>
            /// The tag value for resolving.
            /// </summary>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            [CanBeNull] public readonly object Tag;

            internal Resolving([NotNull] IContainer container, [CanBeNull] object tag)
            {
                Container = container;
                Tag = tag;
            }
        }
    }
}