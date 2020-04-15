namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract build context.
    /// </summary>
    [PublicAPI]
    public interface IBuildContext
    {
        /// <summary>
        /// The parent of the current build context.
        /// </summary>
        [CanBeNull] IBuildContext Parent { get; }

        /// <summary>
        /// The target key to build resolver.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The depth of current context in the build tree.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// The current autowiring strategy.
        /// </summary>
        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        /// <summary>
        /// Gets the dependency expression.
        /// </summary>
        /// <param name="defaultExpression">The default expression.</param>
        /// <returns>The dependency expression.</returns>
        [NotNull] Expression GetDependencyExpression([CanBeNull] Expression defaultExpression = null);

        /// <summary>
        /// Creates a child build context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        [NotNull] IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Binds a raw type to a target type.
        /// </summary>
        /// <param name="originalType">The registered type.</param>
        /// <param name="targetType">The target type.</param>
        void BindTypes([NotNull] Type originalType, [NotNull]Type targetType);

        /// <summary>
        /// Tries to replace generic types' markers by related types.
        /// </summary>
        /// <param name="originalType">The target raw type.</param>
        /// <param name="targetType">The replacing type.</param>
        /// <returns></returns>
        bool TryReplaceType([NotNull] Type originalType, out Type targetType);

        /// <summary>
        /// Prepares base expression replacing generic types' markers by related types.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression ReplaceTypes([NotNull] Expression baseExpression);

        /// <summary>
        /// Prepares base expression injecting appropriate dependencies.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="instanceExpression">The instance expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression InjectDependencies([NotNull] Expression baseExpression, [CanBeNull] ParameterExpression instanceExpression = null);

        /// <summary>
        /// Prepares base expression adding the appropriate lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression AddLifetime([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);
    }
}