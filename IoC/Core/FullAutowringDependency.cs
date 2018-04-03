namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;

    internal class FullAutowringDependency: IDependency
    {
        [NotNull] private static readonly ITypeInfo GenericContextTypeInfo = typeof(Context<>).Info();
        [NotNull] private static readonly MethodInfo InjectMethodInfo;
        [NotNull] private readonly AutowringDependency _autowringDependency;

        static FullAutowringDependency()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
        }

        public FullAutowringDependency([NotNull] IContainer container, [NotNull] Type type, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (autowiringStrategy == null)
            {
                autowiringStrategy = container.TryGetResolver<IAutowiringStrategy>(typeof(IAutowiringStrategy), out var contextResolver)
                    ? contextResolver(container)
                    : new DefaultAutowiringStrategy(container);
            }

            var typeInfo = type.Info().ToDefinedGenericType().Info();
            var ctor = autowiringStrategy.SelectConstructor(GetDefaultConstructors(typeInfo).Select(i => new Method<ConstructorInfo>(i)));
            var methods = autowiringStrategy.GetMethods(GetDefaultMethods(typeInfo).Select(i => new Method<MethodInfo>(i)));
            var newExpression = Expression.New(ctor.Info, GetParameters(ctor));

            var contextType = GenericContextTypeInfo.MakeGenericType(typeInfo.Type);
            var itFieldInfo = contextType.Info().DeclaredFields.Single(i => i.Name == nameof(Context<object>.It));
            var thisExpression = Expression.Field(Expression.Parameter(contextType, "context"), itFieldInfo);
            var methodCallExpressions = (
                from method in methods
                select (Expression)Expression.Call(thisExpression, method.Info, GetParameters(method))).ToArray();

            _autowringDependency = new AutowringDependency(newExpression, methodCallExpressions);
        }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            return _autowringDependency.TryBuildExpression(buildContext, lifetime, out baseExpression);
        }

        [NotNull]
        private static IEnumerable<ConstructorInfo> GetDefaultConstructors([NotNull] ITypeInfo typeInfo)
        {
            return typeInfo.DeclaredConstructors
                .Where(MethodFilter)
                .OrderBy(ctor => ctor.GetParameters().Length);
        }

        [NotNull]
        private static IEnumerable<MethodInfo> GetDefaultMethods([NotNull] ITypeInfo typeInfo)
        {
            return typeInfo.DeclaredMethods
                .Where(MethodFilter)
                .OrderBy(methodInfo => methodInfo.GetParameters().Length);
        }

        private static bool MethodFilter<T>([NotNull] T method)
            where T: MethodBase
        {
            return !method.IsStatic && (method.IsAssembly || method.IsPublic);
        }

        [NotNull]
        private static IEnumerable<Expression> GetParameters<TMethodInfo>(IMethod<TMethodInfo> method)
            where TMethodInfo: MethodBase
        {
            for (var position = 0; position < method.Info.GetParameters().Length; position++)
            {
                yield return method[position];
            }
        }

        private class Method<TMethodInfo>: IMethod<TMethodInfo>
            where TMethodInfo: MethodBase
        {
            private readonly Expression[] _parameters;

            public Method([NotNull] TMethodInfo info)
            {
                Info = info ?? throw new ArgumentNullException(nameof(info));
                var parameters = info.GetParameters();
                _parameters = new Expression[parameters.Length];
                for (var i = 0; i < _parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var paramType = parameter.ParameterType;
                    var methodInfo = InjectMethodInfo.MakeGenericMethod(paramType);
                    var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                    _parameters[i] = Expression.Call(methodInfo, containerExpression);
                }
            }

            public TMethodInfo Info { get; }

            public Expression this[int position]
            {
                get => _parameters[position];
                set => _parameters[position] = value ?? throw new ArgumentNullException();
            }
        }
    }
}
