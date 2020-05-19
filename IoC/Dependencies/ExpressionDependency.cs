namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Represents the dependency based on expressions.
    /// </summary>
    public sealed class ExpressionDependency : IDependency
    {
        private readonly LambdaExpression _instanceExpression;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] [ItemNotNull] private readonly LambdaExpression[] _initializeInstanceExpressions;

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public ExpressionDependency(
            [NotNull] LambdaExpression instanceExpression,
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceExpressions)
            :this(instanceExpression, null, initializeInstanceExpressions)
        {
        }

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public ExpressionDependency(
            [NotNull] LambdaExpression instanceExpression,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceExpressions)
        {
            _instanceExpression = instanceExpression ?? throw new ArgumentNullException(nameof(instanceExpression));
            _autowiringStrategy = autowiringStrategy;
            _initializeInstanceExpressions = initializeInstanceExpressions ?? throw new ArgumentNullException(nameof(initializeInstanceExpressions));
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var typesMap = new Dictionary<Type, Type>();
            try
            {
                if (_instanceExpression.Type.Descriptor().IsGenericTypeDefinition())
                {
                    TypeMapper.Shared.Map(_instanceExpression.Type, buildContext.Key.Type, typesMap);
                    foreach (var mapping in typesMap)
                    {
                        buildContext.MapType(mapping.Key, mapping.Value);
                    }
                }
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }

            return 
                new TypesMapDependency(
                    _instanceExpression.Body,
                    _initializeInstanceExpressions.Select(i => i.Body),
                    typesMap,
                    _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        /// <inheritdoc />
        public override string ToString() => $"{_instanceExpression}";
    }
}
