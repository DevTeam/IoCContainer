// ReSharper disable UnusedParameter.Global
namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// A set of injection markers.
    /// </summary>
    [PublicAPI]
    public static class Injections
    {
        private static readonly string JustAMarkerError = $"The method `{nameof(Inject)}` is a marker method and has no implementation. It should be used to configure dependency injection via the constructor or initialization expressions. In other cases please use `{nameof(FluentResolve.Resolve)}` methods.";
        [NotNull] internal static readonly MethodInfo InjectGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject<object>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject<object>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectValueGenericMethodInfo = ((MethodCallExpression)((Expression<Func<TTS?>>)(() => TryInjectValue<TTS>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo InjectWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject<object>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject<object>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectValueWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<TTS?>>)(() => TryInjectValue<TTS>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo AssignGenericMethodInfo = ((MethodCallExpression)((Expression<Action<object, object>>) ((item1, item2) => Assign<object>(default(IContainer), null, null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo InjectMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject(default(IContainer), typeof(object)))).Body).Method;
        [NotNull] internal static readonly MethodInfo TryInjectMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject(default(IContainer), typeof(object)))).Body).Method;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject(default(IContainer), typeof(object), (object)null))).Body).Method;
        [NotNull] internal static readonly MethodInfo TryInjectWithTagMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject(default(IContainer), typeof(object), null))).Body).Method;

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a value type dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull]
        public static T? TryInjectValue<T>([NotNull] this IContainer container) where T : struct =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a value type dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull]
        public static T? TryInjectValue<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) where T : struct =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static IContainer Assign<T>([NotNull] this IContainer container, [NotNull] T destination, [CanBeNull] T source) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);
    }
}
