namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Core;
    using Extensibility;
    using static Extensibility.WellknownExpressions;
    using TypeExtensions = Core.TypeExtensions;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime, IDisposable
    {
        [NotNull] private object _lockObject = new object();
        internal volatile object Instance;

        private static readonly MethodInfo CreateInstanceMethodInfo = TypeExtensions.Info<SingletonLifetime>().DeclaredMethods.Single(i => i.Name == nameof(CreateInstance));

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext, Expression resolver)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            var thisVar = buildContext.DefineValue(this);
            var instanceField = Expression.Field(thisVar, nameof(Instance));
            var typedInstance = instanceField.Convert(expression.Type);
            var methodInfo = CreateInstanceMethodInfo.MakeGenericMethod(expression.Type);
            return Expression.Condition(
                Expression.NotEqual(instanceField, ExpressionExtensions.NullConst),
                typedInstance,
                buildContext.PartiallyCloseBlock(Expression.Call(thisVar, methodInfo, ContainerParameter, ArgsParameter, resolver), resolver));
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
        public ILifetime Clone() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();

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
    }
}
