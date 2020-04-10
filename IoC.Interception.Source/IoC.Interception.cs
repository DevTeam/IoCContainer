
/* 
IoC Container Interception feature

https://github.com/DevTeam/IoCContainer

Important note: do not use any internal classes, structures, enums, interfaces, methods, fields or properties
because it may be changed even in minor updates of package.

MIT License

Copyright (c) 2018-2020 Nikolay Pianikov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning disable nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper disable All

#region Interception

#region Disposable

namespace IoC.Features.Interception
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class Disposable
    {
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG   
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            private int _counter;

            public DisposableAction([NotNull] Action action) => _action = action;

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                _action();
            }
        }
    }
}


#endregion
#region IInterceptorRegistry

namespace IoC.Features.Interception
{
    using System;
    using Castle.DynamicProxy;

    internal interface IInterceptorRegistry
    {
        [NotNull]
        IToken Register([NotNull] IContainer container, [NotNull] Predicate<Key> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors);
    }
}

#endregion
#region InterceptorBuilder

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

        public IToken Register([NotNull] IContainer container, Predicate<Key> filter, params IInterceptor[] interceptors)
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

            public bool Accept(Key key) => _filter(key);

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


#endregion

#endregion

#region IoC.Interception

#region FluentInterception

// ReSharper disable MemberCanBePrivate.Global
namespace IoC.Features
{
    using System;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Castle.Core.Internal;
    using Castle.DynamicProxy;
    using Interception;

    /// <summary>
    /// Represents extensions to add interceptions to the container.
    /// </summary>
    public static class FluentInterception
    {
        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="filter">The filter to intercept appropriate instances.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept([NotNull] this IContainer container, [NotNull] Predicate<Key> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return container.GetRegistry().Register(container, filter, interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="token">The binding token.</param>
        /// <param name="filter">The filter to intercept appropriate instances.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept([NotNull] this IToken token, [NotNull] Predicate<Key> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return token.Container.Intercept(filter, interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return container.Intercept(new Key(typeof(T)), interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="token">The binding token.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept<T>([NotNull] this IToken token, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return token.Intercept(new Key(typeof(T)), interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="token">The binding token.</param>
        /// <param name="key">The key to intercept appropriate instance.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept([NotNull] this IToken token, Key key, [NotNull] [ItemNotNull] params IInterceptor[] interceptors) =>
            Intercept(token.Container, key, interceptors);

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="key">The key to intercept appropriate instance.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Intercept([NotNull] this IContainer container, Key key, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));

            return container.GetRegistry().Register(container, (targetKey) => Filter(targetKey, key), interceptors);
        }

        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        private static IInterceptorRegistry GetRegistry([NotNull] this IContainer container)
        {
            if (container.TryGetResolver<IInterceptorRegistry>(typeof(IInterceptorRegistry), out var resolver))
            {
                return resolver(container);
            }

            throw new InvalidOperationException($"Apply the configuration '{typeof(InterceptionFeature).Name}' before an interception. For details please see this sample: https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Interception.cs");
        }

        private static bool Filter(Key targetKey, Key key)
        {
            if (targetKey.Equals(key))
            {
                return true;
            }

            var targetType = targetKey.Type;
            var interceptedType = key.Type;
#if NET40
            var isGenericTargetType = targetType.IsGenericType;
            if (!isGenericTargetType)
            {
                return false;
            }

            var interceptedIsGenericType = interceptedType.IsGenericTypeDefinition || interceptedType.GetGenericArguments().Any(i => i.GetAttribute<GenericTypeArgumentAttribute>() != null);
#else
            var targetTypeInfo = targetType.GetTypeInfo();
            var isGenericTargetType = targetTypeInfo.IsGenericType;
            if (!isGenericTargetType) {
                return false;
            }

            var interceptedTypeInfo = interceptedType.GetTypeInfo();
            var interceptedIsGenericType = interceptedTypeInfo.IsGenericTypeDefinition || interceptedTypeInfo.GenericTypeArguments.Any(i => i.GetAttribute<GenericTypeArgumentAttribute>() != null);
#endif

            if (!interceptedIsGenericType)
            {
                return false;
            }

#if NET40
            var genericTypeDefinition = targetType.GetGenericTypeDefinition();
            var curGenericTypeDefinition = interceptedType.GetGenericTypeDefinition();
#else
            var genericTypeDefinition = targetTypeInfo.GetGenericTypeDefinition();
            var curGenericTypeDefinition = interceptedTypeInfo.GetGenericTypeDefinition();
#endif
            return Equals(new Key(genericTypeDefinition, targetKey.Tag), new Key(curGenericTypeDefinition, key.Tag));
        }
    }
}


#endregion
#region InterceptionFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Interception;

    /// <inheritdoc cref="IConfiguration" />
    [PublicAPI]
    public sealed class InterceptionFeature : IConfiguration
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Bind<InterceptorBuilder, IInterceptorRegistry, IBuilder>().As(Lifetime.Singleton).To();
        }
    }
}


#endregion

#endregion

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning restore nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper restore All