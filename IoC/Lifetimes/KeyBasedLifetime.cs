namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey, TValue> : ILifetime
        where TValue: class
    {
        private readonly bool _supportOnNewInstanceCreated;
        private readonly bool _supportOnInstanceReleased;
        private static readonly FieldInfo InstancesFieldInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredFields().Single(i => i.Name == nameof(_instances));
        private static readonly MethodInfo CreateKeyMethodInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredMethods().Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, TValue>.Get));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, TValue>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredMethods().Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(typeof(TKey), "key");

        [CanBeNull] private readonly object _lockObject;
        private volatile Table<TKey, TValue> _instances = Table<TKey, TValue>.Empty;

        /// <summary>
        /// Creates an instance
        /// </summary>
        /// <param name="supportOnNewInstanceCreated">True to invoke OnNewInstanceCreated</param>
        /// <param name="supportOnInstanceReleased">True to invoke OnInstanceReleased</param>
        /// <param name="threadSafe"><c>True</c> to synchronize operations.</param>
        protected KeyBasedLifetime(
            bool supportOnNewInstanceCreated = true,
            bool supportOnInstanceReleased = true,
            bool threadSafe = true)
        {
            _supportOnNewInstanceCreated = supportOnNewInstanceCreated;
            _supportOnInstanceReleased = supportOnInstanceReleased;
            if (threadSafe)
            {
                _lockObject = new LockObject();
            }
        }

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (context == null) throw new ArgumentNullException(nameof(context));
            var returnType = context.Key.Type;
            var thisConst = Expression.Constant(this);
            var instanceVar = Expression.Variable(typeof(TValue));
            var instancesField = Expression.Field(thisConst, InstancesFieldInfo);
            var getExpression = Expression.Assign(instanceVar, Expression.Call(instancesField, GetMethodInfo, KeyVar));
            Expression isEmptyExpression;
            if (returnType.Descriptor().IsValueType())
            {
                isEmptyExpression = Expression.Call(null, ExpressionBuilderExtensions.EqualsMethodInfo, instanceVar.Convert(typeof(TValue)), Expression.Default(typeof(TValue)));
            }
            else
            {
                isEmptyExpression = Expression.ReferenceEqual(instanceVar, Expression.Default(returnType));
            }

            var initNewInstanceExpression = _supportOnNewInstanceCreated 
                ? Expression.Call(thisConst, OnNewInstanceCreatedMethodInfo, instanceVar.Convert(typeof(TValue)), KeyVar, context.ContainerParameter, context.ArgsParameter).Convert(returnType)
                : instanceVar;

            Expression createExpression = Expression.Block(
                // instance = _instances.Get(hashCode, key);
                getExpression,
                // if (instance == default(TValue))
                Expression.Condition(
                    isEmptyExpression,
                    Expression.Block(
                        // instance = new T();
                        Expression.Assign(instanceVar, bodyExpression.Convert(typeof(TValue))),
                        // Instances = _instances.Set(hashCode, key, instance);
                        Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, KeyVar, instanceVar.Convert(typeof(object)))),
                        // instance or OnNewInstanceCreated(instance, key, container, args);
                        initNewInstanceExpression,
                        instanceVar), 
                    instanceVar));

            if (_lockObject != null)
            {
                // double check
                createExpression = Expression.Block(
                    // instance = _instances.Get(hashCode, key);
                    getExpression,
                    Expression.Condition(isEmptyExpression, createExpression.Lock(Expression.Constant(_lockObject)), instanceVar)
                );
            }

            return Expression.Block(
                // Key key;
                // int hashCode;
                // T instance;
                new[] { KeyVar, instanceVar },
                // TKey key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(thisConst, CreateKeyMethodInfo, context.ContainerParameter, context.ArgsParameter)),
                createExpression.Convert(returnType)
            // }
            );
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Table<TKey, TValue> instances;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    instances = _instances;
                    _instances = Table<TKey, TValue>.Empty;
                }
            }
            else
            {
                instances = _instances;
                _instances = Table<TKey, TValue>.Empty;
            }

            if (_supportOnInstanceReleased)
            {
                foreach (var instance in instances)
                {
                    OnInstanceReleased(instance.Value, instance.Key);
                }
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
        protected abstract TValue OnNewInstanceCreated(TValue newInstance, TKey key, IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the instance was released.
        /// </summary>
        /// <param name="releasedInstance">The released instance.</param>
        /// <param name="key">The instance key.</param>
        protected abstract void OnInstanceReleased(TValue releasedInstance, TKey key);

        /// <summary>
        /// Forcibly remove an instance.
        /// </summary>
        /// <param name="key">The instance key.</param>
        protected bool Remove(TKey key)
        {
            bool removed;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    _instances = _instances.Remove(key, out removed);
                }
            }
            else
            {
                _instances = _instances.Remove(key, out removed);
            }

            return removed;
        }
    }
}
