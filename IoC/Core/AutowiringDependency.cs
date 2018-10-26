namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    internal class AutowiringDependency: IDependency
    {
        private readonly Expression _expression;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public AutowiringDependency([NotNull] LambdaExpression constructor, [NotNull][ItemNotNull] params LambdaExpression[] statements)
            :this(
                constructor?.Body ?? throw new ArgumentNullException(nameof(constructor)),
                statements?.Select(i => i.Body)?.ToArray() ?? throw new ArgumentNullException(nameof(statements)))
        {
        }

        public AutowiringDependency([NotNull] Expression constructorExpression, [NotNull][ItemNotNull] params Expression[] statementExpressions)
        {
            _expression = constructorExpression ?? throw new ArgumentNullException(nameof(constructorExpression));
            _isComplexType = IsComplexType(_expression.Type);
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public Expression Expression { get; }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                baseExpression = _expression;
                if (_statements.Length > 0)
                {
                    var thisVar = Expression.Variable(baseExpression.Type, "this");
                    baseExpression = Expression.Block(
                        new[] {thisVar},
                        Expression.Assign(thisVar, baseExpression),
                        Expression.Block(_statements),
                        thisVar
                    );

                    if (_isComplexType)
                    {
                        baseExpression = buildContext.ReplaceTypes(baseExpression);
                    }

                    baseExpression = buildContext.InjectDependencies(baseExpression, thisVar);
                }
                else
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

        private bool IsComplexType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return typeDescriptor.IsConstructedGenericType() || typeDescriptor.IsGenericTypeDefinition() || typeDescriptor.IsArray();
        }
    }
}
