namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
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

        [NotNull] internal object LockObject = new object();
        internal volatile Table<TKey, object> Instances = Table<TKey, object>.Empty;

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext, object state)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var returnType = buildContext.Key.Type;
            var thisVar = buildContext.DefineValue(this);
            var keyVar = Expression.Variable(typeof(TKey), "key");
            var hashCodeVar = Expression.Variable(typeof(int), "hashCode");
            var valVar = Expression.Variable(returnType, "val");
            var assignKeyStatement = Expression.Assign(keyVar, Expression.Call(thisVar, CreateKeyMethodInfo, ContainerParameter, ArgsParameter));
            var assignHashCodeStatement = Expression.Assign(hashCodeVar, Expression.Call(keyVar, ExpressionExtensions.GetHashCodeMethodInfo));
            var instancesField = Expression.Field(thisVar, nameof(Instances));
            var assignValStatement = Expression.Assign(valVar, Expression.Call(instancesField, GetMethodInfo, hashCodeVar, keyVar).Convert(returnType));
            var lockObjectField = Expression.Field(thisVar, nameof(LockObject));
            var onNewInstanceCreatedMethodInfo = OnNewInstanceCreatedMethodInfo.MakeGenericMethod(returnType);
            var conditionStatement = Expression.Condition(
                Expression.NotEqual(valVar, ExpressionExtensions.NullConst),
                valVar,
                Expression.Block(
                    Expression.Block(
                        Expression.Assign(valVar, expression),
                        Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, hashCodeVar, keyVar, valVar))
                        ).LockExpression(lockObjectField),
                    Expression.Call(thisVar, onNewInstanceCreatedMethodInfo, valVar, keyVar, ContainerParameter, ArgsParameter),
                    valVar));

            return Expression.Block(
                new[] { keyVar, hashCodeVar, valVar },
                assignKeyStatement,
                assignHashCodeStatement,
                assignValStatement,
                conditionStatement
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
}
