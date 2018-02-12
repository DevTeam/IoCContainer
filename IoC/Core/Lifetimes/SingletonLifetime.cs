namespace IoC.Core.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Core;

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

        private static readonly MethodInfo CreateInstanceMethodInfo = Type<SingletonLifetime>.Info.DeclaredMethods.Single(i => i.Name == nameof(CreateInstance));
        private static readonly Expression NullConst = Expression.Constant(null);
        private static readonly ITypeInfo ResolverTypeInfo = typeof(Resolver<>).Info();

        public Expression Build(Expression baseExpression)
        {
            if (baseExpression == null) throw new NotSupportedException($"The argument {nameof(baseExpression)} should not be null for lifetime.");
            var instanceField = Expression.Field(Expression.Constant(this), nameof(_instance));
            var typedInstance = Expression.Convert(instanceField, baseExpression.Type);
            var methodInfo = CreateInstanceMethodInfo.MakeGenericMethod(baseExpression.Type);
            var resolverType = ResolverTypeInfo.MakeGenericType(baseExpression.Type);
            var resolverExpression = Expression.Lambda(resolverType, baseExpression, true, ResolverGenerator.Parameters);
            var resolver = resolverExpression.Compile();

            var lifetimeBody = Expression.Condition(
                Expression.NotEqual(instanceField, NullConst),
                typedInstance,
                Expression.Call(Expression.Constant(this), methodInfo, ResolverGenerator.ContainerParameter, ResolverGenerator.ArgsParameter, Expression.Constant(resolver)));

            return lifetimeBody;
        }
    }
}
