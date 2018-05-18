namespace IoC.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Allows to specify behaviour for cases with issue.
    /// </summary>
    [PublicAPI]
    public interface IIssueResolver
    {
        /// <summary>
        /// Handles the scenario when binding cannot be registered.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="keys">The set of binding keys.</param>
        /// <returns>The dependency token.</returns>
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull] Key[] keys);

        /// <summary>
        /// Handles the scenario when the dependency cannot be resolved.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <returns>The pair of the dependency and of the lifetime.</returns>
        [NotNull] Tuple<IDependency, ILifetime> CannotResolveDependency([NotNull] IContainer container, Key key);

        /// <summary>
        /// Handles the scenario when cannot get a resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resolver.</returns>
        [NotNull] Resolver<T> CannotGetResolver<T>([NotNull] IContainer container, Key key, [NotNull] Exception error);

        /// <summary>
        /// Handles the scenario when cannot extract generic type arguments.
        /// </summary>
        /// <param name="type">The instance type.</param>
        /// <returns>The extracted generic type arguments.</returns>
        [NotNull][ItemNotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        /// <summary>
        /// Handles the scenario when a cyclic dependence was detected.
        /// </summary>
        /// <param name="key">The resolving key.</param>
        /// <param name="reentrancy">The level of reentrancy.</param>
        void CyclicDependenceDetected(Key key, int reentrancy);

        /// <summary>
        /// Handles the scenario when cannot parse a type from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a type metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="typeName">The text with a type metadata.</param>
        /// <returns></returns>
        [NotNull] Type CannotParseType([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);

        /// <summary>
        /// Handles the scenario when cannot parse a lifetime from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a lifetime metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="lifetimeName">The text with a lifetime metadata.</param>
        /// <returns></returns>
        Lifetime CannotParseLifetime([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);

        /// <summary>
        /// Handles the scenario when cannot parse a tag from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a tag metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="tag">The text with a tag metadata.</param>
        /// <returns></returns>
        [CanBeNull] object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);

        /// <summary>
        /// Handles the scenario when cannot build expression.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="dependency">The dependeny.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression CannotBuildExpression([NotNull] IBuildContext buildContext, [NotNull] IDependency dependency, ILifetime lifetime = null);

        /// <summary>
        /// Handles the scenario when cannot resolve a constructor.
        /// </summary>
        /// <param name="constructors">Available constructors.</param>
        /// <returns>The constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> CannotResolveConstructor([NotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);

        /// <summary>
        /// Handles the scenario when cannot resolve the instance type.
        /// </summary>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <returns>The type to create an instance.</returns>
        Type CannotResolveType([NotNull] Type registeredType, [NotNull] Type resolvingType);
    }
}
