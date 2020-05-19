namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;

    /// <summary>
    /// Represents the dependency based on expressions and a map of types.
    /// </summary>
    internal sealed class TypesMapDependency : IDependency
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        private readonly Expression _createInstanceExpression;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] [ItemNotNull] private readonly IEnumerable<Expression> _initializeInstanceExpressions;

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        /// <param name="typesMap">The type mapping dictionary.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public TypesMapDependency(
            [NotNull] Expression instanceExpression,
            [NotNull] [ItemNotNull] IEnumerable<Expression> initializeInstanceExpressions,
            [NotNull] IDictionary<Type, Type> typesMap,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _createInstanceExpression = instanceExpression ?? throw new ArgumentNullException(nameof(instanceExpression));
            _initializeInstanceExpressions = initializeInstanceExpressions ?? throw new ArgumentNullException(nameof(initializeInstanceExpressions));
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
            _autowiringStrategy = autowiringStrategy;
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var autoWiringStrategy = _autowiringStrategy ?? buildContext.AutowiringStrategy;
                var typeDescriptor = _createInstanceExpression.Type.Descriptor();
                expression = ReplaceTypes(buildContext, _createInstanceExpression, _typesMap);
                var thisVar = Expression.Variable(expression.Type);

                var initializeExpressions =
                    GetInitializers(autoWiringStrategy, typeDescriptor)
                        .Select(initializer => (Expression)Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext)))
                        .Concat(_initializeInstanceExpressions.Select(statementExpression => ReplaceTypes(buildContext, statementExpression, _typesMap)))
                        .ToArray();

                if (initializeExpressions.Length > 0)
                {
                    expression = Expression.Block(
                        new[] {thisVar},
                        Expression.Assign(thisVar, expression),
                        Expression.Block(initializeExpressions),
                        thisVar
                    );
                }

                expression = DependencyInjectionExpressionBuilder.Shared.Build(expression, buildContext, thisVar);
                expression = buildContext.FinalizeExpression(expression, lifetime);

                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"{_createInstanceExpression}";

        private static Expression ReplaceTypes(IBuildContext buildContext, Expression expression, IDictionary<Type, Type> typesMap) =>
            TypeReplacerExpressionBuilder.Shared.Build(expression, buildContext, typesMap);

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IEnumerable<IMethod<MethodInfo>> GetInitializers(IAutowiringStrategy autoWiringStrategy, TypeDescriptor typeDescriptor)
        {
            var methods = typeDescriptor.GetDeclaredMethods().Select(info => new Method<MethodInfo>(info));
            if (autoWiringStrategy.TryResolveInitializers(methods, out var initializers))
            {
                return initializers;
            }

            if (DefaultAutowiringStrategy.Shared == autoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(methods, out initializers))
            {
                initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            }

            return initializers;
        }
    }
}
