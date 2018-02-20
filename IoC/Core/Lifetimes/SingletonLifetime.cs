namespace IoC.Core.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;
    using Core;
    using Extensibility;
    using TypeExtensions = TypeExtensions;

    internal sealed class SingletonLifetime : ILifetime, IDisposable, IExpressionBuilder
    {
        [NotNull] private object _lockObject = new object();
        private volatile object _instance;

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            if (_instance != null)
            {
                return (T) _instance;
            }

            return CreateInstance(container, args, resolver);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public T CreateInstance<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = resolver(container, args);
                }
            }

            return (T)_instance;
        }

        public void Dispose()
        {
            if (_instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public ILifetime Clone()
        {
            return new SingletonLifetime();
        }

        public override string ToString()
        {
            return Lifetime.Singleton.ToString();
        }

        private static readonly MethodInfo CreateInstanceMethodInfo = TypeExtensions.Info<SingletonLifetime>().DeclaredMethods.Single(i => i.Name == nameof(CreateInstance));
        private static readonly Expression NullConst = Expression.Constant(null);
        private static readonly ITypeInfo ResolverTypeInfo = typeof(Resolver<>).Info();

        public Expression Build(Expression expression)
        {
            if (expression == null) throw new NotSupportedException($"The argument {nameof(expression)} should not be null for lifetime.");
            var instanceField = Expression.Field(Expression.Constant(this), nameof(_instance));
            var typedInstance = ExpressionBuilder.Shared.Convert(instanceField, expression.Type);
            var methodInfo = CreateInstanceMethodInfo.MakeGenericMethod(expression.Type);
            var resolverType = ResolverTypeInfo.MakeGenericType(expression.Type);
            var resolverExpression = Expression.Lambda(resolverType, expression, true, ResolverExpressionBuilder.Parameters);
            var resolver = resolverExpression.Compile();

            var lifetimeBody = Expression.Condition(
                Expression.NotEqual(instanceField, NullConst),
                typedInstance,
                Expression.Call(Expression.Constant(this), methodInfo, ResolverExpressionBuilder.ContainerParameter, ResolverExpressionBuilder.ArgsParameter, Expression.Constant(resolver)));

            return lifetimeBody;
        }
    }
}
