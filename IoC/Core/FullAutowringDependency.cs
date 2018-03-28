namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Extensibility;

    internal class FullAutowringDependency: IDependency
    {
        [NotNull] private static readonly ITypeInfo GenericContextTypeInfo = typeof(Context<>).Info();
        [NotNull] private static readonly MethodCallExpression[] EmptyMethods = new MethodCallExpression[0];
        [NotNull] private static readonly MethodInfo InjectMethodInfo;
        [NotNull] private readonly AutowringDependency _autowringDependency;

        static FullAutowringDependency()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
        }

        public FullAutowringDependency([NotNull] IContainer container, [NotNull] Type type, [CanBeNull] Func<IEnumerable<MethodBase>, IEnumerable<MethodBase>> methodsProvider = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var typeInfo = type.Info().ToDefinedGenericType().Info();
            var constructorInfo = (
                methodsProvider != null
                    ? methodsProvider(GetDefaultConstructors(typeInfo).Where(Filter)).OfType<ConstructorInfo>().Where(Filter).FirstOrDefault()
                    : GetDefaultConstructors(typeInfo).Where(Filter).FirstOrDefault())
                ?? container.Resolve<IIssueResolver>().CannotFindConstructor(type);

            var methodInfos = (
                methodsProvider != null
                    ? methodsProvider(GetDefaultMethods(typeInfo).Where(Filter)).OfType<MethodInfo>().Where(Filter)
                    : Enumerable.Empty<MethodInfo>()).ToArray();

            MemberExpression thisExpression = null;
            if (methodInfos.Any())
            {
                var contextType = GenericContextTypeInfo.MakeGenericType(typeInfo.Type);
                var itFieldInfo = contextType.Info().DeclaredFields.Single(i => i.Name == nameof(Context<object>.It));
                thisExpression = Expression.Field(Expression.Parameter(contextType, "context"), itFieldInfo);
            }

            var newExpression = Expression.New(constructorInfo, CreateParameters(constructorInfo));
            var methodCallExpressions = (
                from methodInfo in methodInfos
                select (Expression)Expression.Call(thisExpression, methodInfo, CreateParameters(methodInfo))).ToArray();

            _autowringDependency = new AutowringDependency(newExpression, methodCallExpressions);
        }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression)
        {
            return _autowringDependency.TryBuildExpression(buildContext, lifetime, out baseExpression);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IEnumerable<Expression> CreateParameters(MethodBase method)
        {
            foreach (var parameter in method.GetParameters())
            {
                var paramType = parameter.ParameterType;
                var methodInfo = InjectMethodInfo.MakeGenericMethod(paramType);
                var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                yield return Expression.Call(methodInfo, containerExpression);
            }
        }

        [NotNull]
        private static IEnumerable<ConstructorInfo> GetDefaultConstructors([NotNull] ITypeInfo typeInfo)
        {
            return typeInfo.DeclaredConstructors
                .Where(Filter)
                .OrderBy(ctor => ctor.GetParameters().Length);
        }

        [NotNull]
        private static IEnumerable<MethodInfo> GetDefaultMethods([NotNull] ITypeInfo typeInfo)
        {
            return typeInfo.DeclaredMethods
                .Where(Filter)
                .OrderBy(methodInfo => methodInfo.GetParameters().Length);
        }

        private static bool Filter<T>([NotNull] T method)
            where T: MethodBase
        {
            return !method.IsStatic && method.IsPublic;
        }
    }
}
