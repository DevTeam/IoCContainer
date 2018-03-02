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

        public Expression Build(Expression expression, Key key, IContainer container, ILifetime lifetime)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (lifetime == null)
            {
                return expression;
            }

            if (lifetime is IExpressionBuilder<object> builder)
            {
                return builder.Build(expression, key, container);
            }

            var getOrCreateMethodInfo = LifetimeGenericGetOrCreateMethodInfo.MakeGenericMethod(key.Type);
            var resolverExpression = Expression.Lambda(key.Type.ToResolverType(), expression, false, ExpressionExtensions.Parameters);
            var resolver = resolverExpression.Compile();
            var lifetimeCall = Expression.Call(Expression.Constant(lifetime), getOrCreateMethodInfo, ExpressionExtensions.ContainerParameter, ExpressionExtensions.ArgsParameter, Expression.Constant(resolver));
            return lifetimeCall;
        }
    }
}
