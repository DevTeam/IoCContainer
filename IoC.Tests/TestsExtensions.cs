namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using Core;
    using Moq;

    internal static class TestsExtensions
    {
        [NotNull]
        public static Resolver<T> Compile<T>([NotNull] this ILifetime lifetime, [NotNull] Expression<Func<T>> lambdaExpression, IBuildContext context = null)
        {
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            if (lambdaExpression == null) throw new ArgumentNullException(nameof(lambdaExpression));
            var buildContext = new BuildContext(TypeDescriptor<T>.Key, Mock.Of<IContainer>(), new List<IBuilder>(), DefaultAutowiringStrategy.Shared);
            var lifetimeExpression = lifetime.Build(buildContext, lambdaExpression.Body);
            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), lifetimeExpression, false, DependencyEntry.ResolverParameters);
            DefaultCompiler.Shared.TryCompile(context ?? Mock.Of<IBuildContext>(), resolverExpression, out var resolver);
            return (Resolver<T>)resolver;
        }

        public static void Parallelize(Action action, int count = 10, int parallelism = 100)
        {
            var exceptions = new List<Exception>();
            void RunAction(object state)
            {
                try
                {
                    for (var i = 0; i < count; i++)
                    {
                        action();
                    }
                }
                catch (Exception ex)
                {
                    lock (exceptions)
                    {
                        exceptions.Add(ex);
                    }
                }
            }

            var threads = new List<Thread>();
            for (var i = 0; i < parallelism; i++)
            {
                threads.Add(new Thread(RunAction) { IsBackground = true });
            }

            threads.ForEach(i => i.Start());
            threads.ForEach(i => i.Join());

            if (exceptions.Count > 0)
            {
                throw exceptions[0];
            }
        }
    }
}
