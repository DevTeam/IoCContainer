namespace IoC.Lifetimes
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey>: ILifetime, ILifetimeBuilder
    {
        private static readonly FieldInfo InstancesFieldInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredFields().Single(i => i.Name == nameof(_instances));
        private static readonly MethodInfo CreateKeyMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, object>.Get));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, object>.Set));
        private static readonly MethodInfo AfterCreationMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(AfterCreation));
        private static readonly ParameterExpression KeyVar = Expression.Variable(typeof(TKey), "key");

        private readonly ILockObject _lockObject = new LockObject();
        private readonly LifetimeDirector _lifetimeDirector;
        private volatile Table<TKey, object> _instances = Table<TKey, object>.Empty;
        private readonly MemberExpression _instancesField;
        private readonly ConstantExpression _thisConst;

        /// <summary>
        /// Creates an instance
        /// </summary>
        protected KeyBasedLifetime()
        {
            _lifetimeDirector = new LifetimeDirector(this, _lockObject);
            _thisConst = Expression.Constant(this);
            _instancesField = Expression.Field(_thisConst, InstancesFieldInfo);
        }

        public abstract ILifetime CreateLifetime();

        public virtual IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression) =>
            Expression.Block(
                new[] { KeyVar },
                // TKey key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(_thisConst, CreateKeyMethodInfo, context.ContainerParameter, context.ArgsParameter)),
                _lifetimeDirector.Build(context, bodyExpression)
            );

        bool ILifetimeBuilder.TryBuildRestoreInstance(IBuildContext context, out Expression getExpression)
        {
            getExpression = Expression.Call(_instancesField, GetMethodInfo, KeyVar);
            return true;
        }

        bool ILifetimeBuilder.TryBuildSaveInstance(IBuildContext context, Expression instanceExpression, out Expression setExpression)
        {
            setExpression = Expression.Assign(_instancesField, Expression.Call(_instancesField, SetMethodInfo, KeyVar, instanceExpression));
            return true;
        }

        bool ILifetimeBuilder.TryBuildBeforeCreating(IBuildContext context, out Expression beforeCreatingExpression)
        {
            beforeCreatingExpression = default(Expression);
            return false;
        }

        bool ILifetimeBuilder.TryBuildAfterCreation(IBuildContext context, Expression instanceExpression, out Expression newInstanceExpression)
        {
            newInstanceExpression = Expression.Call(_thisConst, AfterCreationMethodInfo, instanceExpression, KeyVar, context.ContainerParameter, context.ArgsParameter);
            return true;
        }

        [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
        public virtual void Dispose()
        {
            Table<TKey, object> instances;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    instances = _instances;
                    _instances = Table<TKey, object>.Empty;
                }
            }
            else
            {
                instances = _instances;
                _instances = Table<TKey, object>.Empty;
            }

            foreach (var instance in instances)
            {
                OnRelease(instance.Value, instance.Key);
            }
        }

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
        protected virtual object AfterCreation(object newInstance, TKey key, IContainer container, object[] args) 
            => newInstance;

        /// <summary>
        /// Is invoked on the instance was released.
        /// </summary>
        /// <param name="releasedInstance">The released instance.</param>
        /// <param name="key">The instance key.</param>
        protected virtual void OnRelease(object releasedInstance, TKey key) { }

        /// <summary>
        /// Forcibly remove an instance.
        /// </summary>
        /// <param name="key">The instance key.</param>
        [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
        protected internal bool Remove(TKey key)
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
