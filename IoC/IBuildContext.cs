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
        /// The target key to build resolver.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// The depth of current context in the build tree.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// The current autowiring strategy.
        /// </summary>
        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull] ParameterExpression ArgsParameter { get; }

        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull] ParameterExpression ContainerParameter { get; }

        /// <summary>
        /// Creates a child context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        [NotNull] IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Create the expression.
        /// </summary>
        /// <param name="defaultExpression">The default expression.</param>
        /// <returns>The expression.</returns>
        [NotNull] Expression CreateExpression([CanBeNull] Expression defaultExpression = null);

        /// <summary>
        /// Adds types mapping.
        /// </summary>
        /// <param name="fromType">Type to map.</param>
        /// <param name="toType">The target type.</param>
        void MapType([NotNull] Type fromType, [NotNull] Type toType);

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="parameterExpression">The parameters expression to add.</param>
        void AddParameter([NotNull] ParameterExpression parameterExpression);

        /// <summary>
        /// Finalizes an expression and adds a lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression FinalizeExpression([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);

        /// <summary>
        /// Compiles a lambda expression to delegate.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression to compile.</param>
        /// <param name="lambdaCompiled">The compiled lambda.</param>
        /// <param name="error">Compilation error.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error);
    }
}