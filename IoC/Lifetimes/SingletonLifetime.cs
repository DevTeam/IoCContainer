namespace IoC.Lifetimes
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Extensibility;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime, IDisposable
    {
        [NotNull] internal object LockObject = new object();
        internal volatile object Instance;

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext, object state)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var type = expression.Type;
            var thisVar = buildContext.DefineValue(this);
            var lockObjectField = Expression.Field(thisVar, nameof(LockObject));
            var instanceField = Expression.Field(thisVar, nameof(Instance));
            var typedInstance = instanceField.Convert(type);
            return Expression.Condition(
                Expression.NotEqual(instanceField, ExpressionExtensions.NullConst),
                typedInstance,
                Expression.Block(
                    Expression.Assign(instanceField, expression).LockExpression(lockObjectField),
                    typedInstance));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (Instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <inheritdoc />
        public ILifetime Clone() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}
