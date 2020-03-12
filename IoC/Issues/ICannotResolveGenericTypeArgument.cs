namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot resolve the generic type argument of an instance type.
    /// </summary>
    [PublicAPI]

    public interface ICannotResolveGenericTypeArgument
    {
        /// <summary>
        /// Resolves the generic type argument of an instance type.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="type">Registered type.</param>
        /// <param name="genericTypeArgPosition">The generic type argument position in the registered type.</param>
        /// <param name="genericTypeArg">The generic type argument in the registered type.</param>
        /// <returns>The resoled generic type argument.</returns>
        [NotNull] Type Resolve([NotNull] IBuildContext buildContext, [NotNull] Type type, int genericTypeArgPosition, [NotNull] Type genericTypeArg);
    }
}
