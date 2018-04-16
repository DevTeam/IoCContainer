namespace IoC
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for an autowirin method.
    /// </summary>
    /// <typeparam name="TMethodInfo">The type of method info.</typeparam>
    [PublicAPI]
    public interface IMethod<out TMethodInfo>
        where TMethodInfo: MethodBase
    {
        /// <summary>
        /// The method's information.
        /// </summary>
        [NotNull] TMethodInfo Info { get; }

        /// <summary>
        /// Provides parameters' expressions.
        /// </summary>
        /// <returns>Parameters' expressions</returns>
        [NotNull][ItemNotNull] IEnumerable<Expression> GetParametersExpressions();

        /// <summary>
        /// Sets the parameter expression at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="parameterExpression">The parameter expression.</param>
        void SetParameterExpression(int parameterPosition, [NotNull] Expression parameterExpression);
    }
}
