namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot extract generic type arguments.
    /// </summary>
    [PublicAPI]
    public interface ICannotGetGenericTypeArguments
    {
        /// <summary>
        /// Resolves the scenario when cannot extract generic type arguments.
        /// </summary>
        /// <param name="type">The instance type.</param>
        /// <returns>The extracted generic type arguments.</returns>
        [NotNull] [ItemNotNull] Type[] Resolve([NotNull] Type type);
    }
}
