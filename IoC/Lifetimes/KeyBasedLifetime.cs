namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;
    using static WellknownExpressions;

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey>: ILifetime
    {
        private static readonly FieldInfo InstancesFieldInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredFields().Single(i => i.Name == nameof(_instances));
        private static readonly MethodInfo CreateKeyMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = typeof(CoreExtensions).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(CoreExtensions.GetByRef)).MakeGenericMethod(typeof(TKey), typeof(object));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, object>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(TypeDescriptor<TKey>.Type, "key");

        [NotNull] private object _lockObject = new object();
        private volatile Table<TKey, object> _instances = Table<TKey, object>.Empty;

        /// <inheritdoc />
        public Expression Build(Expression bodyExpression, IBuildContext buildContext)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var returnType = buildContext.Key.Type;
            var thisConst = buildContext.AppendValue(this);
            var instanceVar = Expression.Variable(returnType, "val");
            var instancesField = Expression.Field(thisConst, InstancesFieldInfo);
            var lockObjectConst = buildContext.AppendValue(_lockObject);
            var onNewInstanceCreatedMethodInfo = OnNewInstanceCreatedMethodInfo.MakeGenericMethod(returnType);
            var assignInstanceExpression = Expression.Assign(instanceVar, Expression.Call(null, GetMethodInfo, instancesField, SingletonBasedLifetimeShared.HashCodeVar, KeyVar).Convert(returnType));
            var isNullExpression = Expression.ReferenceEqual(instanceVar, ExpressionBuilderExtensions.NullConst);

            return Expression.Block(
                // Key key;
                // int hashCode;
                // T instance;
                new[] { KeyVar, SingletonBasedLifetimeShared.HashCodeVar, instanceVar },
                // var key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(thisConst, CreateKeyMethodInfo, ContainerParameter, ArgsParameter)),
                // var hashCode = key.GetHashCode();
                Expression.Assign(SingletonBasedLifetimeShared.HashCodeVar, Expression.Call(KeyVar, ExpressionBuilderExtensions.GetHashCodeMethodInfo)),
                // var instance = (T)_instances.Get(hashCode, key);
                assignInstanceExpression,
                // if (instance == null)
                Expression.Condition(
                    isNullExpression,
                    Expression.Block(
                        // lock (this._lockObject)
                        Expression.Block(
                            // var instance = (T)_instances.Get(hashCode, key);
                            assignInstanceExpression,
                            // if (instance == null)
                            Expression.IfThen(
                                Expression.Equal(instanceVar, ExpressionBuilderExtensions.NullConst),
                                Expression.Block(
                                    // instance = new T();
                                    Expression.Assign(instanceVar, bodyExpression),
                                    // Instances = _instances.Set(hashCode, key, instance);
                                    Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, SingletonBasedLifetimeShared.HashCodeVar, KeyVar, instanceVar))
                                )
                            )
                        ).Lock(lockObjectConst),
                        // OnNewInstanceCreated(instance, key, container, args);
                        Expression.Call(thisConst, onNewInstanceCreatedMethodInfo, instanceVar, KeyVar, ContainerParameter, ArgsParameter)),
                        // else {
                        // return instance;
                        instanceVar
                    )
                    // }
            );
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Table<TKey, object> instances;
            lock (_lockObject)
            {
                instances = _instances;
                _instances = Table<TKey, object>.Empty;
            }

            foreach (var instance in instances)
            {
                OnInstanceReleased(instance.Value, instance.Key);
            }
        }

        /// <inheritdoc />
        public abstract ILifetime Create();

        /// <summary>
        /// Creates key for singleton.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The created key.</returns>
        protected abstract TKey CreateKey(IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the new instance creation.
        /// </summary>
        /// <param name="newInstance">The new instance.</param>
        /// <param name="key">The instance key.</param>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected abstract T OnNewInstanceCreated<T>(T newInstance, TKey key, IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the instance was released.
        /// </summary>
        /// <param name="releasedInstance">The released instance.</param>
        /// <param name="key">The instance key.</param>
        protected abstract void OnInstanceReleased(object releasedInstance, TKey key);
    }

    internal static class SingletonBasedLifetimeShared
    {
        internal static readonly ParameterExpression HashCodeVar = Expression.Variable(TypeDescriptor<int>.Type, "hashCode");
    }
}
