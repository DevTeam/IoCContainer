namespace IoC.Lifetimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;

    internal struct LifetimeDirector
    {
        [NotNull] private readonly ILifetimeBuilder _builder;
        [CanBeNull] private readonly ILockObject _lockObject;

        public LifetimeDirector(
            [NotNull] ILifetimeBuilder builder,
            [CanBeNull] ILockObject lockObject = null)
        {
            _builder = builder;
            _lockObject = lockObject;
        }
        
        [Pure]
        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var instanceType = context.Key.Type;
            var instanceVar = Expression.Variable(typeof(object));
            var creationBlock = new List<Expression>();
            if (_builder.TryBuildBeforeCreating(context, out var creatingExpression))
            {
                creationBlock.Add(creatingExpression);
            }

            creationBlock.Add(Expression.Assign(instanceVar,  bodyExpression.Convert(typeof(object))));
            if (_builder.TryBuildAfterCreation(context, instanceVar, out var newInstanceVar))
            {
                creationBlock.Add(Expression.Assign(instanceVar, newInstanceVar));
            }

            if (_builder.TryBuildSaveInstance(context, instanceVar, out var setExpression))
            {
                creationBlock.Add(setExpression);
            }

            creationBlock.Add(instanceVar);

            if (!_builder.TryBuildRestoreInstance(context, out var getExpression))
            {
                return Expression.Block(new[] { instanceVar }, creationBlock);
            }

            getExpression = Expression.Assign(instanceVar, getExpression);
            Expression isEmptyExpression;
            if (instanceType.Descriptor().IsValueType())
            {
                isEmptyExpression = Expression.Call(null, ExpressionBuilderExtensions.EqualsMethodInfo, instanceVar, Expression.Default(typeof(object)));
            }
            else
            {
                isEmptyExpression = Expression.ReferenceEqual(instanceVar, Expression.Default(instanceType));
            }

            var createExpression = Expression.Block(
                new[] { instanceVar },
                getExpression,
                Expression.Condition(
                    isEmptyExpression,
                    Expression.Block(creationBlock),
                    instanceVar));

            var lockObject = _lockObject;
            if (lockObject != null)
            {
                // double check
                createExpression = Expression.Block(
                    new [] {instanceVar},
                    getExpression,
                    Expression.Condition(isEmptyExpression, createExpression.Lock(Expression.Constant(lockObject)), instanceVar)
                );
            }

            return createExpression.Convert(instanceType);
        }
    }
}
