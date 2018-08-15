namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Extension method for IoC container.
    /// </summary>
    [PublicAPI]
    public static class Fluent
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parent">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parent.GetResolver<IContainer>(WellknownContainers.NewChild.AsTag())(parent, name);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        internal static IIssueResolver GetIssueResolver([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Resolve<IIssueResolver>();
        }
    }
}
