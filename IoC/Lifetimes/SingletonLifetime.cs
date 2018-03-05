namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Core;
    using Extensibility;
    using TypeExtensions = Core.TypeExtensions;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime, IDisposable, IExpressionBuilder<ParameterExpression>
    {
        [NotNull] private object _lockObject = new object();
        internal volatile object Instance;

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            if (Instance != null)
            {
                return (T) Instance;
            }

            return CreateInstance(container, args, resolver);
        }

        [MethodImpl((MethodImplOptions)256)]
        internal T CreateInstance<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            lock (_lockObject)
            {
                if (Instance == null)
                {
                    Instance = resolver(container, args);
                }
            }

            return (T)Instance;
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
        public ILifetime Clone()
        {
            return new SingletonLifetime();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Lifetime.Singleton.ToString();
        }

        private static readonly MethodInfo CreateInstanceMethodInfo = TypeExtensions.Info<SingletonLifetime>().DeclaredMethods.Single(i => i.Name == nameof(CreateInstance));
        private static readonly Expression NullConst = Expression.Constant(null);
        private static readonly ITypeInfo ResolverTypeInfo = typeof(Resolver<>).Info();

        /// <inheritdoc />
        public Expression Build(Expression expression, BuildContext buildContext, ParameterExpression resolver)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            var thisVar = buildContext.DefineValue(this);
            var instanceField = Expression.Field(thisVar, nameof(Instance));
            var typedInstance = instanceField.Convert(expression.Type);
            var methodInfo = CreateInstanceMethodInfo.MakeGenericMethod(expression.Type);
            return Expression.Condition(
                Expression.NotEqual(instanceField, NullConst),
                typedInstance,
                buildContext.PartiallyCloseBlock(Expression.Call(thisVar, methodInfo, BuildContext.ContainerParameter, BuildContext.ArgsParameter, resolver), resolver));
        }
    }
}
