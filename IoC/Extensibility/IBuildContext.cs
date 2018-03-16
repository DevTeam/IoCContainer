namespace IoC.Extensibility
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
        /// The compiler.
        /// </summary>
        IExpressionCompiler Compiler { get; }

        /// <summary>
        /// The target key.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Creates a child context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Defines value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The value type.</param>
        /// <returns>The parameter expression.</returns>
        Expression DefineValue([CanBeNull] object value, [NotNull] Type type);

        /// <summary>
        /// Defines value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The parameter expression.</returns>
        Expression DefineValue<T>([CanBeNull] T value);

        /// <summary>
        /// Defines variable.
        /// </summary>
        /// <param name="expression">The value expression.</param>
        /// <returns>The parameter expression.</returns>
        ParameterExpression DefineVariable([NotNull] Expression expression);

        /// <summary>
        /// Closes a block of statements.
        /// </summary>
        /// <param name="targetExpression">The target expression.</param>
        /// <returns>The result expression.</returns>
        Expression CloseBlock([NotNull] Expression targetExpression);

        /// <summary>
        /// Closes block for specified expressions.
        /// </summary>
        /// <param name="targetExpression">The target expression.</param>
        /// <param name="expressions">Assigment expressions.</param>
        /// <returns>The resulting block expression.</returns>
        Expression PartiallyCloseBlock([NotNull] Expression targetExpression, [NotNull][ItemNotNull] params Expression[] expressions);
    }
}