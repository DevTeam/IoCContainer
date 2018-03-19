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
        public static Resolver<T> Compile<T>(this ILifetime lifetime, Expression<T> expression, Resolver<T> resolver)
        {
            var buildContext = new BuildContext(ExpressionCompilerDefault.Shared, new Key(typeof(T)), Mock.Of<IContainer>(), new List<IDisposable>());
            var resolverVar = buildContext.DefineValue(resolver, typeof(T));
            var lifetimeExpression = lifetime.Build(expression, buildContext, resolverVar);
            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), lifetimeExpression, false, ResolverParameters);
            return (Resolver<T>)buildContext.Compiler.Compile(resolverExpression);
        }
    }
}
