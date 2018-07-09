namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Extensibility;

    internal class AutowringDependency: IDependency
    {
        private readonly Expression _epression;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;

        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public AutowringDependency([NotNull] LambdaExpression constructor, [NotNull][ItemNotNull] params LambdaExpression[] statements)
            :this(
                constructor?.Body ?? throw new ArgumentNullException(nameof(constructor)),
                statements?.Select(i => i.Body)?.ToArray() ?? throw new ArgumentNullException(nameof(statements)))
        {
        }

        public AutowringDependency([NotNull] Expression constructorExpression, [NotNull][ItemNotNull] params Expression[] statementExpressions)
        {
            _epression = constructorExpression ?? throw new ArgumentNullException(nameof(constructorExpression));
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public Expression Expression { get; }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                baseExpression = buildContext.Prepare(_epression);
                if (_statements.Any())
                {
                    baseExpression = Expression.Block(CreateAutowringStatements(buildContext, baseExpression));
                }

                baseExpression = buildContext.AppendLifetime(baseExpression, lifetime);
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

        private IEnumerable<Expression> CreateAutowringStatements(
            [NotNull] IBuildContext buildContext,
            [NotNull] Expression newExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (newExpression == null) throw new ArgumentNullException(nameof(newExpression));

            var instanceExpression = buildContext.AppendVariable(newExpression);
            foreach (var statement in _statements)
            {
                yield return buildContext.Prepare(statement, instanceExpression);
            }

            yield return instanceExpression;
        }
    }
}
