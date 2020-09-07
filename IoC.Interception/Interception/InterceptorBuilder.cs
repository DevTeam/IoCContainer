namespace IoC.Features.Interception
{
    using System;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Castle.DynamicProxy;
// ReSharper disable once RedundantUsingDirective

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class InterceptorBuilder : IInterceptorRegistry, IBuilder
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly List<InterceptorsInfo> _interceptors = new List<InterceptorsInfo>();

        public IToken Register(IMutableContainer container, Predicate<Key> filter, params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            var info = new InterceptorsInfo(filter, interceptors);
            lock (_interceptors)
            {
                _interceptors.Add(info);
            }

            return Disposable.Create(() =>
            {
                lock (_interceptors)
                {
                    _interceptors.Remove(info);
                }
            }).AsTokenOf(container);
        }

        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            lock (_interceptors)
            {
                var proxyGeneratorExpression = Expression.Constant(ProxyGenerator);
                foreach (var interceptors in _interceptors)
                {
                    if (interceptors.Accept(context.Key))
                    {
                        bodyExpression = interceptors.Build(bodyExpression, context, proxyGeneratorExpression);
                    }
                }

                return bodyExpression;
            }
        }

        private sealed class InterceptorsInfo
        {
            private static readonly Type[] FuncTypes = {typeof(Type), typeof(Type[]), typeof(object), typeof(IInterceptor[])};
#if NET40
            private static readonly MethodInfo CreateInterfaceProxyWithTargetMethodInfo = typeof(ProxyGenerator).GetMethod(nameof(ProxyGenerator.CreateInterfaceProxyWithTarget), FuncTypes);
            private static readonly MethodInfo CreateClassProxyWithTargetMethodInfo = typeof(ProxyGenerator).GetMethod(nameof(ProxyGenerator.CreateClassProxyWithTarget), FuncTypes);
#else
            private static readonly TypeInfo ProxyGeneratorTypeInfo = typeof(ProxyGenerator).GetTypeInfo();
            private static readonly MethodInfo CreateInterfaceProxyWithTargetMethodInfo = ProxyGeneratorTypeInfo.GetDeclaredMethods(nameof(ProxyGenerator.CreateInterfaceProxyWithTarget)).First(i => i.GetParameters().Select(j => j.ParameterType).SequenceEqual(FuncTypes));
            private static readonly MethodInfo CreateClassProxyWithTargetMethodInfo = ProxyGeneratorTypeInfo.GetDeclaredMethods(nameof(ProxyGenerator.CreateClassProxyWithTarget)).First(i => i.GetParameters().Select(j => j.ParameterType).SequenceEqual(FuncTypes));
#endif

            private readonly Predicate<Key> _filter;
            private readonly IInterceptor[] _interceptors;

            public InterceptorsInfo(Predicate<Key> filter, IInterceptor[] interceptors)
            {
                _filter = filter;
                _interceptors = interceptors;
            }

            public bool Accept(Key key)
            {
#if NET40
                if (key.Type.IsSealed)
                {
                    return false;
                }
#else
                if (key.Type.GetTypeInfo().IsSealed)
                {
                    return false;
                }
#endif
                return _filter(key);
            }

            public Expression Build(Expression bodyExpression, IBuildContext buildContext, Expression proxyGeneratorExpression)
            {
                var type = buildContext.Key.Type;
#if NET40
                var isInterface = type.IsInterface;
                var interfaces = type.GetInterfaces();
#else
                var typeInfo = type.GetTypeInfo();
                var isInterface = typeInfo.IsInterface;
                var interfaces = typeInfo.ImplementedInterfaces.ToArray();
#endif

                var typeExpression = Expression.Constant(type);
                var interfacesExpression = Expression.Constant(interfaces);
                var interceptorsExpression = Expression.Constant(_interceptors);
                var args = new[] {typeExpression, interfacesExpression, bodyExpression, interceptorsExpression};
                if (isInterface)
                {
                    return Expression.Call(proxyGeneratorExpression, CreateInterfaceProxyWithTargetMethodInfo, args);
                }

                return Expression.Call(proxyGeneratorExpression, CreateClassProxyWithTargetMethodInfo, args);
            }
        }
    }
}
