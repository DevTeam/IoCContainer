namespace IoC.Features.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Castle.DynamicProxy;
// ReSharper disable once RedundantUsingDirective

    internal class InterceptorBuilder : IInterceptorRegistry, IBuilder
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly List<InterceptorsInfo> _interceptors = new List<InterceptorsInfo>();

        public IDisposable Register(Predicate<IBuildContext> filter, params IInterceptor[] interceptors)
        {
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
            });
        }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext)
        {
            lock (_interceptors)
            {
                var proxyGeneratorExpression = buildContext.AppendValue(ProxyGenerator);
                foreach (var interceptors in _interceptors)
                {
                    if (interceptors.Accept(buildContext))
                    {
                        bodyExpression = interceptors.Build(bodyExpression, buildContext, proxyGeneratorExpression);
                    }
                }

                return bodyExpression;
            }
        }

        private class InterceptorsInfo
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

            private readonly Predicate<IBuildContext> _filter;
            private readonly IInterceptor[] _interceptors;

            public InterceptorsInfo(Predicate<IBuildContext> filter, IInterceptor[] interceptors)
            {
                _filter = filter;
                _interceptors = interceptors;
            }

            public bool Accept(IBuildContext buildContext) => _filter(buildContext);

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

                var typeExpression = buildContext.AppendValue(type);
                var interfacesExpression = buildContext.AppendValue(interfaces);
                var interceptorsExpression = buildContext.AppendValue(_interceptors);
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
