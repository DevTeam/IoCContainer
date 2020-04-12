// ReSharper disable UnusedParameter.Global
namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Injection extensions.
    /// </summary>
    [PublicAPI]
    public static class Injections
    {
        internal static readonly string JustAMarkerError = $"The method `{nameof(Inject)}` is a marker method and has no implementation. It should be used to configure dependency injection via the constructor or initialization expressions only. In other cases please use `{nameof(FluentResolve.Resolve)}` method.";
        [NotNull] internal static readonly MethodInfo InjectGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo TryInjectGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo TryInjectWithTagGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectingAssignmentGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectMethodInfo;
        [NotNull] internal static readonly MethodInfo TryInjectMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo;
        [NotNull] internal static readonly MethodInfo TryInjectWithTagMethodInfo;

        static Injections()
        {
            Expression<Func<object>> injectGenExpression = () => Inject<object>(default(IContainer));
            InjectGenericMethodInfo = ((MethodCallExpression)injectGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> tryInjectGenExpression = () => TryInject<object>(default(IContainer));
            TryInjectGenericMethodInfo = ((MethodCallExpression)tryInjectGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectWithTagGenExpression = () => Inject<object>(default(IContainer), null);
            InjectWithTagGenericMethodInfo = ((MethodCallExpression)injectWithTagGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> tryInjectWithTagGenExpression = () => Inject<object>(default(IContainer), null);
            TryInjectWithTagGenericMethodInfo = ((MethodCallExpression)tryInjectWithTagGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Action<object, object>> assignmentCallGenExpression = (item1, item2) => Inject<object>(default(IContainer), null, null);
            InjectingAssignmentGenericMethodInfo = ((MethodCallExpression)assignmentCallGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectExpression = () => Inject(default(IContainer), typeof(object));
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method;

            Expression<Func<object>> tryInjectExpression = () => Inject(default(IContainer), typeof(object));
            TryInjectMethodInfo = ((MethodCallExpression)tryInjectExpression.Body).Method;

            Expression<Func<object>> injectWithTagExpression = () => Inject(default(IContainer), typeof(object), (object)null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method;

            Expression<Func<object>> tryInjectWithTagExpression = () => Inject(default(IContainer), typeof(object), (object)null);
            TryInjectWithTagMethodInfo = ((MethodCallExpression)tryInjectWithTagExpression.Body).Method;
        }

        /// <summary>
        /// Injects a dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);


        /// <summary>
        /// Tries to inject a dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container)
            where T : class
            => throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Tries to inject a dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);


        /// <summary>
        /// Injects a dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static void Inject<T>([NotNull] this IContainer container, [NotNull] T destination, [CanBeNull] T source) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Tries to inject a dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Tries to inject a dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);
    }
}
