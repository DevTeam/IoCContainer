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
