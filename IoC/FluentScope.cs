namespace IoC
{
    using System;

    /// <summary>
    /// Represents extensions dealing with scopes.
    /// </summary>
    public static class FluentScope
    {
        /// <summary>
        /// Creates new resolving scope. Can be used with <c>ScopeSingleton</c>.
        /// </summary>
        /// <param name="container">A container to resolve a scope.</param>
        /// <returns>Tne new scope instance.</returns>
        [NotNull]
        public static IScope CreateScope([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Resolve<IScope>();
        }
    }
}
