namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;

    internal class FullAutowringDependency: IDependency
    {
        [NotNull] private readonly IContainer _container;
        [NotNull] private readonly Type _type;
        [NotNull] private readonly IAutowiringStrategy _defaultAutowiringStrategy;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        
        [NotNull] private static Cache<ConstructorInfo, NewExpression> _constructors = new Cache<ConstructorInfo, NewExpression>();
        [NotNull] private static Cache<Type, Expression> _this = new Cache<Type, Expression>();
        [NotNull] private static Cache<Type, MethodCallExpression> _injections = new Cache<Type, MethodCallExpression>();

        public FullAutowringDependency([NotNull] IContainer container, [NotNull] Type type, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _defaultAutowiringStrategy = new DefaultAutowiringStrategy(container);
            if (autowiringStrategy == null)
            {
                if (container.TryGetResolver<IAutowiringStrategy>(typeof(IAutowiringStrategy), out var contextResolver))
                {
                    _autowiringStrategy = contextResolver(container);
                }
            }
            else
            {
                _autowiringStrategy = autowiringStrategy;
            }
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            Type instanceType = null;
            if (!(_autowiringStrategy?.TryResolveType(_type, buildContext.Key.Type, out instanceType) ?? false))
            {
                if (!_defaultAutowiringStrategy.TryResolveType(_type, buildContext.Key.Type, out instanceType))
                {
                    instanceType = null;
                }
            }

            var typeDescriptor = (instanceType ?? _container.Resolve<IIssueResolver>().CannotResolveType(_type, buildContext.Key.Type)).Descriptor();
            var defaultConstructors = GetDefaultConstructors(typeDescriptor);
            IMethod<ConstructorInfo> ctor = null;
            if (!(_autowiringStrategy?.TryResolveConstructor(defaultConstructors, out ctor) ?? false))
            {
                if (!_defaultAutowiringStrategy.TryResolveConstructor(defaultConstructors, out ctor))
                {
                    ctor = null;
                }
            }

            ctor = ctor ?? _container.Resolve<IIssueResolver>().CannotResolveConstructor(defaultConstructors);

            var defaultMehods = GetDefaultMethods(typeDescriptor);
            IEnumerable<IMethod<MethodInfo>> initializers = null;
            if (!(_autowiringStrategy?.TryResolveInitializers(defaultMehods, out initializers) ?? false))
            {
                if (!_defaultAutowiringStrategy.TryResolveInitializers(defaultMehods, out initializers))
                {
                    initializers = null;
                }
            }

            initializers = initializers ?? Enumerable.Empty<IMethod<MethodInfo>>();

            var newExpression = _constructors.GetOrCreate(ctor.Info, () => Expression.New(ctor.Info, GetParameters(ctor)));
            var thisExpression = _this.GetOrCreate(typeDescriptor.AsType(), () =>
            {
                var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.AsType());
                var itFieldInfo = contextType.Descriptor().GetDeclaredFields().Single(i => i.Name == nameof(Context<object>.It));
                return Expression.Field(Expression.Parameter(contextType, "context"), itFieldInfo);
            });

            var methodCallExpressions = (
                from initializer in initializers
                select (Expression)Expression.Call(thisExpression, initializer.Info, GetParameters(initializer))).ToArray();

            var autowringDependency = new AutowringDependency(newExpression, methodCallExpressions);
            return autowringDependency.TryBuildExpression(buildContext, lifetime, out baseExpression);
        }

        [NotNull]
        private static IEnumerable<Method<ConstructorInfo>> GetDefaultConstructors([NotNull] TypeDescriptor typeDescriptor)
        {
            return typeDescriptor.GetDeclaredConstructors()
                .Where(MethodFilter)
                .OrderBy(ctor => ctor.GetParameters().Length)
                .Select(i => new Method<ConstructorInfo>(i));
        }

        [NotNull]
        private static IEnumerable<Method<MethodInfo>> GetDefaultMethods([NotNull] TypeDescriptor typeDescriptor)
        {
            return typeDescriptor.GetDeclaredMethods()
                .Where(MethodFilter)
                .OrderBy(methodInfo => methodInfo.GetParameters().Length)
                .Select(i => new Method<MethodInfo>(i));
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
                    _parameters[i] = _injections.GetOrCreate(paramType, () =>
                    {
                        var methodInfo = Injections.InjectMethodInfo.MakeGenericMethod(paramType);
                        var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                        return Expression.Call(methodInfo, containerExpression);
                    });
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
