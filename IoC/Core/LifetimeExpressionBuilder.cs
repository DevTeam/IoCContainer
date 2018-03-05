namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;

    internal class LifetimeExpressionBuilder : IExpressionBuilder<ILifetime>
    {
        public static readonly IExpressionBuilder<ILifetime> Shared = new LifetimeExpressionBuilder();
        private static readonly MethodInfo LifetimeGenericGetOrCreateMethodInfo = TypeExtensions.Info<ILifetime>().DeclaredMethods.Single(i => i.Name == nameof(ILifetime.GetOrCreate));

        private LifetimeExpressionBuilder()
        {
        }

        public Expression Build(Expression expression, BuildContext buildContext, ILifetime lifetime)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (lifetime == null)
            {
                return expression;
            }

            var getOrCreateMethodInfo = LifetimeGenericGetOrCreateMethodInfo.MakeGenericMethod(buildContext.Key.Type);
            var resolverType = expression.Type.ToResolverType();
            var resolverExpression = Expression.Lambda(resolverType, expression, false, BuildContext.ResolverParameters);
            var resolver = buildContext.Container.GetExpressionCompiler().Compile(resolverExpression);
            var resolverVar = buildContext.DefineValue(resolver, resolverType);
            if (lifetime is IExpressionBuilder<ParameterExpression> builder)
            {
                return builder.Build(expression, buildContext, resolverVar);
            }

            var lifetimeVar = buildContext.DefineValue(lifetime);
            return Expression.Call(lifetimeVar, getOrCreateMethodInfo, BuildContext.ContainerParameter, BuildContext.ArgsParameter, resolverVar);
        }
    }
}
