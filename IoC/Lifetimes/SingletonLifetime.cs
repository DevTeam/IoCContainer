namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime
    {
        private static readonly FieldInfo InstanceFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(_instance));

        [NotNull] private readonly object _lockObject = new object();
#pragma warning disable CS0649, IDE0044
        private volatile object _instance;
#pragma warning restore CS0649, IDE0044

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            var thisConst = Expression.Constant(this);
            var lockObjectConst = Expression.Constant(_lockObject);
            var instanceField = Expression.Field(thisConst, InstanceFieldInfo);
            var typedInstance = instanceField.Convert(expression.Type);
            var isNullExpression = Expression.ReferenceEqual(instanceField, ExpressionBuilderExtensions.NullConst);

            return Expression.Block(Expression.IfThen(
                isNullExpression,
                // if (this._instance == null)
                // lock (this._lockObject)
                Expression.IfThen(
                    // if (this._instance == null)
                    isNullExpression,
                    // this._instance = new T();
                    Expression.Assign(instanceField, expression)).Lock(lockObjectConst)),
                // return this._instance
                typedInstance);
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            registrationContainer;

        /// <inheritdoc />
        public void Dispose()
        {
            IDisposable disposable;
            lock (_lockObject)
            {
                disposable = _instance as IDisposable;
            }

            disposable?.Dispose();
        }

        /// <inheritdoc />
        public ILifetime Create() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}
