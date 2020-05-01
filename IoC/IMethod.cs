namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for autowiring method.
    /// </summary>
    /// <typeparam name="TMethodInfo">The type of method info.</typeparam>
    [PublicAPI]
    public interface IMethod<out TMethodInfo>
        where TMethodInfo: MethodBase
    {
        /// <summary>
        /// The methods information.
        /// </summary>
        [NotNull] TMethodInfo Info { get; }

        /// <summary>
        /// Provides a set of parameters expressions.
        /// </summary>
        /// <returns>Parameters' expressions</returns>
        [NotNull][ItemNotNull] IEnumerable<Expression> GetParametersExpressions([NotNull] IBuildContext buildContext);

        /// <summary>
        /// Specifies the expression of method parameter at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="parameterExpression">The parameter expression.</param>
        void SetExpression(int parameterPosition, [NotNull] Expression parameterExpression);

        /// <summary>
        /// Specifies the dependency type and tag for method parameter at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencyTag">The optional dependency tag value.</param>
        /// <param name="isOptional"><c>True</c> if it is optional dependency.</param>
        /// <param name="args">The optional arguments.</param>
        void SetDependency(int parameterPosition, [NotNull] Type dependencyType, [CanBeNull] object dependencyTag = null, bool isOptional = false, params object[] args);
    }
}
