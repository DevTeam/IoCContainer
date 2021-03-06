﻿namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// For a singleton instance.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime
    {
        private static readonly FieldInfo InstanceFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(_instance));

        [CanBeNull] private readonly object _lockObject;
#pragma warning disable CS0649, IDE0044
        private volatile object _instance;
#pragma warning restore CS0649, IDE0044

        /// <summary>
        /// Creates an instance of lifetime.
        /// </summary>
        /// <param name="threadSafe"><c>True</c> to synchronize operations.</param>
        public SingletonLifetime(bool threadSafe = true)
        {
            if (threadSafe)
            {
                _lockObject = new LockObject();
            }
        }

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var thisConst = Expression.Constant(this);
            var instanceField = Expression.Field(thisConst, InstanceFieldInfo);
            var typedInstance = instanceField.Convert(expression.Type);
            var isNullExpression = Expression.ReferenceEqual(instanceField, ExpressionBuilderExtensions.NullConst);

            Expression createExpression = Expression.IfThen(
                // if (this._instance == null)
                isNullExpression,
                // this._instance = new T();
                Expression.Assign(instanceField, expression.Convert(typeof(object))));

            if (_lockObject != null)
            {
                // double check
                createExpression = Expression.IfThen(isNullExpression, createExpression.Lock(Expression.Constant(_lockObject)));
            }

            return Expression.Block(
                createExpression,
                // return this._instance
                typedInstance);
        }

        /// <inheritdoc />
        public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            registrationContainer;

        /// <inheritdoc />
        public void Dispose()
        {
            IDisposable disposable;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    disposable = _instance.AsDisposable();
                }
            }
            else
            {
                disposable = _instance.AsDisposable();
            }

            disposable?.Dispose();
        }

        /// <inheritdoc />
        public ILifetime CreateLifetime() => new SingletonLifetime(_lockObject != null);

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}
