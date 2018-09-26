namespace IoC.Features
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Castle.Core.Internal;
    using Castle.DynamicProxy;

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
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IDisposable Intercept([NotNull] this IContainer container, [NotNull] Predicate<IBuildContext> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return container.Resolve<IInterceptorRegistry>().Register(filter, interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="key">The key to intercept appropriate instance.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IDisposable Intercept([NotNull] this IContainer container, Key key, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return container.Resolve<IInterceptorRegistry>().Register(ctx =>
                {
                    if (ctx.Key.Equals(key))
                    {
                        return true;
                    }

                    var type = ctx.Key.Type;
                    var curType = key.Type;
#if NET40
                    var isGenericType = type.IsGenericType;
                    if (!isGenericType) {
                        return false;
                    }

                    var curIsGenericType = curType.IsGenericTypeDefinition || curType.GetGenericArguments().Any(i => i.GetAttribute<GenericTypeArgumentAttribute>() != null);;
#else
                    var typeInfo = type.GetTypeInfo();
                    var isGenericType = typeInfo.IsGenericType;
                    if (!isGenericType) {
                        return false;
                    }

                    var curTypeInfo = curType.GetTypeInfo();
                    var curIsGenericType = curTypeInfo.IsGenericTypeDefinition || curTypeInfo.GetGenericArguments().Any(i => i.GetAttribute<GenericTypeArgumentAttribute>() != null);
#endif

                    if (curIsGenericType)
                    {
#if NET40
                        var genericTypeDefinition = type.GetGenericTypeDefinition();
                        var curGenericTypeDefinition = curType.GetGenericTypeDefinition();
#else
                        var genericTypeDefinition = typeInfo.GetGenericTypeDefinition();
                        var curGenericTypeDefinition = curTypeInfo.GetGenericTypeDefinition();
#endif
                        return new Key(genericTypeDefinition, ctx.Key.Tag).Equals(new Key(curGenericTypeDefinition, key.Tag));
                    }

                    return false;
                },
                interceptors);
        }

        /// <summary>
        /// Registers interceptors.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IDisposable Intercept<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IInterceptor[] interceptors)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return container.Intercept(new Key(typeof(T)), interceptors);
        }
    }
}
