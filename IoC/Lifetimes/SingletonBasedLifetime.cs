namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using Core.Collections;
    using Extensibility;
    using static Core.TypeExtensions;
    using static Extensibility.WellknownExpressions;

    /// <summary>
    /// Represents the abstaction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    [PublicAPI]
    public abstract class SingletonBasedLifetime<TKey>: ILifetime, IDisposable
    {
        private static readonly MethodInfo CreateKeyMethodInfo = Info<SingletonBasedLifetime<TKey>>().DeclaredMethods.Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = Info<Table<TKey, object>>().DeclaredMethods.Single(i => i.Name == nameof(Table<TKey, object>.Get));
        private static readonly MethodInfo SetMethodInfo = Info<Table<TKey, object>>().DeclaredMethods.Single(i => i.Name == nameof(Table<TKey, object>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Info<SingletonBasedLifetime<TKey>>().DeclaredMethods.Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(typeof(TKey), "key");

        [NotNull] internal object LockObject = new object();
        internal volatile Table<TKey, object> Instances = Table<TKey, object>.Empty;

        /// <inheritdoc />
        public Expression Build(Expression bodyExpression, IBuildContext buildContext, object state)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var returnType = buildContext.Key.Type;
            var thisVar = buildContext.DefineValue(this);
            var instanceVar = Expression.Variable(returnType, "val");
            var instancesField = Expression.Field(thisVar, nameof(Instances));
            var lockObjectField = Expression.Field(thisVar, nameof(LockObject));
            var onNewInstanceCreatedMethodInfo = OnNewInstanceCreatedMethodInfo.MakeGenericMethod(returnType);
            var assignInstanceExpression = Expression.Assign(instanceVar, Expression.Call(instancesField, GetMethodInfo, SingletonBasedLifetimeVars.HashCodeVar, KeyVar).Convert(returnType));
            return Expression.Block(
                // Key key;
                // int hashCode;
                // T instance;
                new[] { KeyVar, SingletonBasedLifetimeVars.HashCodeVar, instanceVar },
                // var key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(thisVar, CreateKeyMethodInfo, ContainerParameter, ArgsParameter)),
                // var hashCode = key.GetHashCode();
                Expression.Assign(SingletonBasedLifetimeVars.HashCodeVar, Expression.Call(KeyVar, ExpressionExtensions.GetHashCodeMethodInfo)),
                // var instance = (T)Instances.Get(hashCode, key);
                assignInstanceExpression,
                // if(instance != null)
                Expression.Condition(
                    Expression.NotEqual(instanceVar, ExpressionExtensions.NullConst),
                    // return instance;
                    instanceVar,
                    // else {
                    Expression.Block(
                        // lock (this.LockObject)
                        Expression.Block(
                            // var instance = (T)Instances.Get(hashCode, key);
                            assignInstanceExpression,
                            // if(instance == null)
                            Expression.IfThen(
                                Expression.Equal(instanceVar, ExpressionExtensions.NullConst),
                                Expression.Block(
                                    // instance = new T();
                                    Expression.Assign(instanceVar, bodyExpression),
                                    // Instances = Instances.Set(hashCode, key, instance);
                                    Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, SingletonBasedLifetimeVars.HashCodeVar, KeyVar, instanceVar))
                                )
                            )
                        ).Lock(lockObjectField),
                        // OnNewInstanceCreated(instance, key, container, args);
                        Expression.Call(thisVar, onNewInstanceCreatedMethodInfo, instanceVar, KeyVar, ContainerParameter, ArgsParameter),
                        // return instance;
                        instanceVar))
                    // }
            );
        }

        /// <inheritdoc />
        public void Dispose()
        {
            System.Collections.Generic.List<IDisposable> items;
            lock (LockObject)
            { 
                items = Instances.Select(i => i.Value).OfType<IDisposable>().ToList();
                Instances = Table<TKey, object>.Empty;
            }

            Disposable.Create(items).Dispose();
        }

        /// <inheritdoc />
        public abstract ILifetime Clone();

        /// <summary>
        /// Creates key for singleton.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">The arfuments.</param>
        /// <returns>The created key.</returns>
        protected abstract TKey CreateKey(IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the new instance creation.
        /// </summary>
        /// <param name="newInstance">The new instance.</param>
        /// <param name="key">The instance key.</param>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        protected abstract void OnNewInstanceCreated<T>(T newInstance, TKey key, IContainer container, object[] args);
    }

    internal static class SingletonBasedLifetimeVars
    {
        internal static readonly ParameterExpression HashCodeVar = Expression.Variable(typeof(int), "hashCode");
    }
}
