namespace IoC.Issues
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Resolves the scenario when cannot resolve a constructor.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveConstructor
    {
        /// <summary>
        /// Resolves the scenario when cannot resolve a constructor.
        /// </summary>
        /// <param name="constructors">Available constructors.</param>
        /// <returns>The constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> Resolve([NotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);
    }
}
