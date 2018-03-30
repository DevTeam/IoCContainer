namespace IoC
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for an autowirin method.
    /// </summary>
    [PublicAPI]
    public interface IAutowiringStrategy
    {
        /// <summary>
        /// Selects a constructor from a set of available constructors.
        /// </summary>
        /// <param name="constructors">The set of available constructors.</param>
        /// <returns>The selected constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> SelectConstructor([NotNull][ItemNotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);

        /// <summary>
        /// Provides methods from a set of available methods in the appropriate order.
        /// </summary>
        /// <param name="methods">The set of available methods.</param>
        /// <returns>The set of initializing methods in the appropriate order.</returns>
        [NotNull][ItemNotNull] IEnumerable<IMethod<MethodInfo>> GetMethods([NotNull][ItemNotNull] IEnumerable<IMethod<MethodInfo>> methods);
    }
}
