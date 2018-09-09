namespace IoC.Features
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Castle.DynamicProxy;

    /// <summary>
    /// Represents extensions to add interceptions to the container.
    /// </summary>
    public static class FluentInterception
    {
        /// <summary>
        /// Intercepts types.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types">The contract types to intercept.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IInterception<object> Intercept([NotNull] this IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Interception<object>(container, types);
        }

        /// <summary>
        /// Intercepts the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IInterception<T> Intercept<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Interception<T>(container, typeof(T));
        }

        /// <summary>
        /// Marks the binding by the tag. Is it possible to use multiple times.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="interception"></param>
        /// <param name="tagValue"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IInterception<T> Tag<T>([NotNull] this IInterception<T> interception, [CanBeNull] object tagValue = null)
        {
            if (interception == null) throw new ArgumentNullException(nameof(interception));
            return new Interception<T>(interception, tagValue);
        }

        /// <summary>
        /// Marks the interception to be used for any tags.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="interception">The interception token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IInterception<T> AnyTag<T>([NotNull] this IInterception<T> interception)
        {
            if (interception == null) throw new ArgumentNullException(nameof(interception));
            return interception.Tag(Key.AnyTag);
        }

        /// <summary>
        /// Apply the interceptor.
        /// </summary>
        /// <param name="interception">The interception token.</param>
        /// <param name="interceptors">The list of interceptors.</param>
        /// <returns>The interception token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable By([NotNull] this IInterception<object> interception, [NotNull][ItemNotNull] params IInterceptor[] interceptors)
        {
            if (interception == null) throw new ArgumentNullException(nameof(interception));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return interception.ByInternal(interceptors);
        }

        /// <summary>
        /// Apply the interceptor.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="interception">The interception token.</param>
        /// <param name="interceptors">The set of interceptors.</param>
        /// <returns>The interception token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable By<T>([NotNull] this IInterception<T> interception, [NotNull][ItemNotNull] params IInterceptor[] interceptors)
        {
            if (interception == null) throw new ArgumentNullException(nameof(interception));
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
            return interception.ByInternal(interceptors);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IDisposable ByInternal<T>([NotNull] this IInterception<T> interception, [NotNull][ItemNotNull] IInterceptor[] interceptors)
        {
            var keys =
                from type in interception.Types
                from tag in interception.Tags.DefaultIfEmpty(null)
                select new Key(type, tag);
            
            return interception.Container.Resolve<IInterceptorRegistry>().Register<T>(keys, interceptors);
        }
    }
}
