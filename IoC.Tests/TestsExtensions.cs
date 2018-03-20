namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;
    using Moq;
    using static Extensibility.WellknownExpressions;

    internal static class TestsExtensions
    {
        [NotNull]
        public static Resolver<T> Compile<T>([NotNull] this ILifetime lifetime, [NotNull] Expression<Func<T>> lambdaExpression)
        {
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            if (lambdaExpression == null) throw new ArgumentNullException(nameof(lambdaExpression));
            var buildContext = new BuildContext(ExpressionCompilerDefault.Shared, new Key(typeof(T)), Mock.Of<IContainer>(), new List<IDisposable>());
            var lifetimeExpression = lifetime.Build(lambdaExpression.Body, buildContext);
            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), lifetimeExpression, false, ResolverParameters);
            return (Resolver<T>)buildContext.Compiler.Compile(resolverExpression);
        }
    }
}
