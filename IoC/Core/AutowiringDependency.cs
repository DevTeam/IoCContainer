namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    internal sealed class AutowiringDependency : IDependency
    {
        private readonly Expression _expression;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public AutowiringDependency(
            [NotNull] LambdaExpression constructor,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params LambdaExpression[] statements)
            :this(
                constructor?.Body ?? throw new ArgumentNullException(nameof(constructor)),
                autoWiringStrategy,
                statements?.Select(i => i.Body)?.ToArray() ?? throw new ArgumentNullException(nameof(statements)))
        {
        }

        public AutowiringDependency(
            [NotNull] Expression constructorExpression,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params Expression[] statementExpressions)
        {
            _expression = constructorExpression ?? throw new ArgumentNullException(nameof(constructorExpression));
            _autoWiringStrategy = autoWiringStrategy;
            _isComplexType = Autowiring.IsComplexType(_expression.Type.Descriptor());
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public Expression Expression { get; }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                baseExpression = Autowiring.ReplaceTypes(buildContext, _isComplexType, _expression);
                baseExpression = Autowiring.ApplyInitializers(
                    buildContext,
                    _autoWiringStrategy ?? buildContext.AutowiringStrategy,
                    baseExpression.Type.Descriptor(),
                    _isComplexType,
                    baseExpression,
                    _statements);

                if (_statements.Length == 0)
                {
                    if (_isComplexType)
                    {
                        baseExpression = buildContext.ReplaceTypes(baseExpression);
                    }

                    baseExpression = buildContext.InjectDependencies(baseExpression);
                }

                baseExpression = buildContext.AddLifetime(baseExpression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                baseExpression = default(Expression);
                return false;
            }
        }

        public override string ToString() => $"{_expression}";
    }
}
