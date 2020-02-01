namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the abstraction for build context.
    /// </summary>
    [PublicAPI]
    public interface IBuildContext
    {

        /// <summary>
        /// The parent build context.
        /// </summary>
        [CanBeNull] IBuildContext Parent { get; }

        /// <summary>
        /// The target key.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The depth of current context.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// Autowiring strategy.
        /// </summary>
        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        /// <summary>
        /// The dependency expression.
        /// </summary>
        [NotNull] Expression DependencyExpression { get; }

        /// <summary>
        /// Creates a child context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        [NotNull] IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Prepares base expression, replacing generic types' markers.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression ReplaceTypes([NotNull] Expression baseExpression);

        /// <summary>
        /// Bind a raw type to a target type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="targetType">The target type.</param>
        void BindTypes([NotNull] Type type, [NotNull]Type targetType);

        /// <summary>
        /// Try replacing generic types' markers.
        /// </summary>
        /// <param name="type">The target raw type.</param>
        /// <param name="targetType">The replacing type.</param>
        /// <returns></returns>
        bool TryReplaceType([NotNull] Type type, out Type targetType);

        /// <summary>
        /// Prepares base expression, injecting dependencies.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="instanceExpression">The instance expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression InjectDependencies([NotNull] Expression baseExpression, [CanBeNull] ParameterExpression instanceExpression = null);

        /// <summary>
        /// Prepares base expression, adding a lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression AddLifetime([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);
    }
}