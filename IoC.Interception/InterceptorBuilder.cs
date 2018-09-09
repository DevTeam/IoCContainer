namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    // ReSharper disable once RedundantUsingDirective
    using System.Reflection;
    using Castle.DynamicProxy;

    internal class InterceptorBuilder : IInterceptorRegistry, IBuilder
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly Dictionary<Key, List<ProxyInfo>> _infos = new Dictionary<Key, List<ProxyInfo>>();

        public IDisposable Register<T>(IEnumerable<Key> keys, params IInterceptor[] interceptors)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));

            Type targetType = null;
            var tokens = new List<IDisposable>();
            lock (_infos)
            {
                var info = new ProxyInfo(interceptors, _infos);
                foreach (var key in keys)
                {
                    if (!_infos.TryGetValue(key, out var infos))
                    {
                        infos = new List<ProxyInfo>();
                        _infos.Add(key, infos);
                    }

                    infos.Add(info);
                    tokens.Add(Disposable.Create(() =>
                    {
                        lock (_infos)
                        {
                            if (infos.Remove(info) && infos.Count == 0)
                            {
                                _infos.Remove(key);
                            }
                        }
                    }));

                    var type = key.Type;
                    var isAssignableFrom = targetType == null || targetType.IsAssignableFrom(type);
                    if (!isAssignableFrom)
                    {
                        continue;
                    }

                    targetType = type;
                }

                info.Type = targetType ?? throw new NotSupportedException();

#if NET40 || UAP10
                info.IsInterface = targetType.IsInterface;
                info.Interfaces = targetType.GetInterfaces();
#else
                var typeInfo = targetType.GetTypeInfo();
                info.IsInterface = typeInfo.IsInterface;
                info.Interfaces = typeInfo.ImplementedInterfaces.ToArray();
#endif
            }

            return Disposable.Create(tokens);
        }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext)
        {
            lock (_infos)
            {
                if (_infos.TryGetValue(buildContext.Key, out var infos))
                {
                    var proxyGeneratorExpression = buildContext.AppendValue(ProxyGenerator);
                    foreach (var info in infos)
                    {
                        bodyExpression = info.Build(bodyExpression, buildContext, proxyGeneratorExpression);
                    }
                }

                return bodyExpression;
            }
        }

        private class ProxyInfo
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

            public bool IsInterface;
            public Type Type;
            public Type[] Interfaces;
            private readonly IInterceptor[] _interceptors;
            private readonly object _lockObject;

            public ProxyInfo(IInterceptor[] interceptors, object lockObject)
            {
                _interceptors = interceptors;
                _lockObject = lockObject;
            }

            public Expression Build(Expression bodyExpression, IBuildContext buildContext, Expression proxyGeneratorExpression)
            {
                lock (_lockObject)
                {
                    var typeExpression = buildContext.AppendValue(Type);
                    var interfacesExpression = buildContext.AppendValue(Interfaces);
                    var interceptorsExpression = buildContext.AppendValue(_interceptors);
                    var args = new[] {typeExpression, interfacesExpression, bodyExpression, interceptorsExpression};
                    if (IsInterface)
                    {
                        return Expression.Call(proxyGeneratorExpression, CreateInterfaceProxyWithTargetMethodInfo, args);
                    }

                    return Expression.Call(proxyGeneratorExpression, CreateClassProxyWithTargetMethodInfo, args);
                }
            }
        }
    }
}
