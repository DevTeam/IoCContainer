namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions dealing with scopes.
    /// </summary>
    public static class FluentScope
    {
        /// <summary>
        /// Creates a new resolving scope. Can be used with <c>ScopeSingleton</c>.
        /// </summary>
        /// <param name="container">A container to resolve a scope.</param>
        /// <returns>Tne new scope instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IScope CreateScope([NotNull] this IContainer container) =>
            (container ?? throw new ArgumentNullException(nameof(container))).Resolve<IScope>();
    }
}
