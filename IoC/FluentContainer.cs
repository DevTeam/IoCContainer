namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Extension methods for IoC containers and configurations.
    /// </summary>
    [PublicAPI]
    public static class FluentContainer
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parentContainer">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IContainer parentContainer, [NotNull] string name = "")
        {
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parentContainer.Resolve<IMutableContainer>(name);
        }

        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="token">The parent container token.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IToken token, [NotNull] string name = "")
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return token.Container.Resolve<IMutableContainer>(name);
        }
    }
}