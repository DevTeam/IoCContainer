namespace IoC.Lifetimes
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey>: ILifetime
    {
        private readonly Dictionary<TKey, object> _instances = new Dictionary<TKey, object>();

        public abstract ILifetime CreateLifetime();

        public virtual IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer)
            => resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression) =>
            ExpressionBuilderExtensions.BuildGetOrCreateInstance(this, context, bodyExpression, nameof(GetOrCreateInstance));

        protected T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            var key = CreateKey(container, args);
            object value;
            var created = false;
            lock (_instances)
            {
                if (!_instances.TryGetValue(key, out value))
                {
                    value = resolver(container, args);
                    _instances.Add(key, value);
                    created = true;
                }
            }

            if (created)
            {
                AfterCreation(value, key, container, args);
            }

            return (T)value;
        }

        public virtual void Dispose()
        {
            lock (_instances)
            {
                foreach (var instance in _instances)
                {
                    OnRelease(instance.Value, instance.Key);
                }
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
            lock (_instances)
            {
                return _instances.Remove(key);
            }
        }
    }
}
