namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    internal sealed class AutowiringDependency : IDependency
    {
        private readonly Expression _expression;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;

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
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var typesMap = new Dictionary<Type, Type>();
                TypeMapper.Shared.Map(_expression.Type, buildContext.Key.Type, typesMap);
                foreach (var mapping in typesMap)
                {
                    buildContext.MapType(mapping.Key, mapping.Value);
                }

                expression = buildContext.ApplyInitializers(
                    _autoWiringStrategy ?? buildContext.AutowiringStrategy,
                    _expression.Type.Descriptor(),
                    _expression,
                    _statements,
                    lifetime,
                    typesMap);

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

        public override string ToString() => $"{_expression}";
    }
}
