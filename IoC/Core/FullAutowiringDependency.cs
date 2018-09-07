namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class FullAutowiringDependency: IDependency
    {
        [NotNull] private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static Cache<ConstructorInfo, NewExpression> _constructors = new Cache<ConstructorInfo, NewExpression>();
        [NotNull] private static Cache<Type, Expression> _this = new Cache<Type, Expression>();
        [NotNull] private readonly Type _type;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;

        public FullAutowiringDependency([NotNull] Type type, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _autowiringStrategy = autowiringStrategy;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var autoWiringStrategy = _autowiringStrategy;
            if (
                autoWiringStrategy == null
                && buildContext.Key.Type != TypeDescriptor<IAutowiringStrategy>.Type
                && buildContext.Container.TryGetResolver<IAutowiringStrategy>(TypeDescriptor<IAutowiringStrategy>.Type, null, out var autoWiringStrategyResolver, out error))
            {
                autoWiringStrategy = autoWiringStrategyResolver(buildContext.Container);
            }

            Type instanceType = null;
            if (!(autoWiringStrategy?.TryResolveType(_type, buildContext.Key.Type, out instanceType) ?? false))
            {
                if (!DefaultAutowiringStrategy.Shared.TryResolveType(_type, buildContext.Key.Type, out instanceType))
                {
                    instanceType = null;
                }
            }

            var typeDescriptor = (instanceType ?? buildContext.Container.Resolve<IIssueResolver>().CannotResolveType(_type, buildContext.Key.Type)).Descriptor();
            var defaultConstructors = CreateMethods(buildContext.Container, typeDescriptor.GetDeclaredConstructors());
            IMethod<ConstructorInfo> ctor = null;
            if (!(autoWiringStrategy?.TryResolveConstructor(defaultConstructors, out ctor) ?? false))
            {
                if (!DefaultAutowiringStrategy.Shared.TryResolveConstructor(defaultConstructors, out ctor))
                {
                    ctor = null;
                }
            }

            ctor = ctor ?? buildContext.Container.Resolve<IIssueResolver>().CannotResolveConstructor(defaultConstructors);
            var defaultMethods = CreateMethods(buildContext.Container, typeDescriptor.GetDeclaredMethods());
            IEnumerable<IMethod<MethodInfo>> initializers = null;
            if (!(autoWiringStrategy?.TryResolveInitializers(defaultMethods, out initializers) ?? false))
            {
                if (!DefaultAutowiringStrategy.Shared.TryResolveInitializers(defaultMethods, out initializers))
                {
                    initializers = null;
                }
            }

            initializers = initializers ?? Enumerable.Empty<IMethod<MethodInfo>>();

            var newExpression = _constructors.GetOrCreate(ctor.Info, () => Expression.New(ctor.Info, ctor.GetParametersExpressions()));
            var thisExpression = _this.GetOrCreate(typeDescriptor.AsType(), () =>
            {
                var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.AsType());
                var itFieldInfo = contextType.Descriptor().GetDeclaredFields().Single(i => i.Name == nameof(Context<object>.It));
                return Expression.Field(Expression.Parameter(contextType, "context"), itFieldInfo);
            });

            var methodCallExpressions = (
                from initializer in initializers
                select (Expression) Expression.Call(thisExpression, initializer.Info, initializer.GetParametersExpressions())).ToArray();

            var autowiringDependency = new AutowiringDependency(newExpression, methodCallExpressions);
            return autowiringDependency.TryBuildExpression(buildContext, lifetime, out baseExpression, out error);
        }

        [NotNull]
        [MethodImpl((MethodImplOptions) 256)]
        private static IEnumerable<IMethod<TMethodInfo>> CreateMethods<TMethodInfo>(IContainer container, [NotNull] IEnumerable<TMethodInfo> methodInfos)
            where TMethodInfo: MethodBase
            => methodInfos
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<TMethodInfo>(info));
    }
}
