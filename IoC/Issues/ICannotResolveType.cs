namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot resolve the instance type.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveType
    {
        /// <summary>
        /// Resolves the scenario when cannot resolve the instance type.
        /// </summary>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <returns>The type to create an instance.</returns>
        Type Resolve([NotNull] Type registeredType, [NotNull] Type resolvingType);
    }
}
