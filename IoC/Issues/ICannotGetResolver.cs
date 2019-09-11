namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot get a resolver.
    /// </summary>
    [PublicAPI]
    public interface ICannotGetResolver
    {
        /// <summary>
        /// Resolves the scenario when cannot get a resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resolver.</returns>
        [NotNull] Resolver<T> Resolve<T>([NotNull] IContainer container, Key key, [NotNull] Exception error);
    }
}
