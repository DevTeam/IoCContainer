namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;
    using Lifetimes;

    /// <summary>
    /// Allows to resolve Functions.
    /// </summary>
    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new FuncFeature();

        /// The high-performance instance.
        public static readonly IConfiguration LightSet = new FuncFeature(true);

        private readonly bool _light;

        private FuncFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(new[] { typeof(Func<TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);

            if (_light) yield break;

            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), Sets.AnyTag);
        }

        private class FuncDependency : IDependency
        {
            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var genericTypeArguments = buildContext.Key.Type.Descriptor().GetGenericTypeArguments();
                var paramsCount = genericTypeArguments.Length - 1;
                var instanceType = genericTypeArguments[paramsCount];
                var key = new Key(instanceType, buildContext.Key.Tag);
                var context = buildContext.CreateChild(key, buildContext.Container);
                var instanceExpression = context.GetDependencyExpression();
                var parameters = new ParameterExpression[paramsCount];
                var parametersArgs = new Expression[paramsCount];
                for (var i = 0; i < paramsCount; i++)
                {
                    var parameterExpression = Expression.Parameter(genericTypeArguments[i]);
                    parameters[i] = parameterExpression;
                    parametersArgs[i] = parameterExpression.Convert(typeof(object));
                }

                Expression argsExpression;
                if (parameters.Length == 0)
                {
                    argsExpression = Expression.Constant(FluentNativeResolve.EmptyArgs);
                }
                else
                {
                    argsExpression = Expression.NewArrayInit(typeof(object), parametersArgs);
                }

                instanceExpression = Expression.Block(
                    new[] { buildContext.ContainerParameter, buildContext.ArgsParameter },
                    Expression.Assign(buildContext.ContainerParameter, Expression.Constant(buildContext.Container)),
                    Expression.Assign(buildContext.ArgsParameter, argsExpression),
                    instanceExpression);

                if (context.TryCompile(Expression.Lambda(instanceExpression, parameters), out var factory, out error))
                {
                    expression = Expression.Constant(factory);
                    return true;
                }

                expression = default(Expression);
                return false;
            }
        }
    }
}
