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
        internal const string JustAMarkerError = "Just a marker. Should be used to configure dependency injection.";
        [NotNull] internal static readonly MethodInfo InjectGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectingAssignmentGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo;

        static Injections()
        {
            Expression<Func<object>> injectGenExpression = () => Inject<object>(default(IContainer));
            InjectGenericMethodInfo = ((MethodCallExpression)injectGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectWithTagGenExpression = () => Inject<object>(default(IContainer), null);
            InjectWithTagGenericMethodInfo = ((MethodCallExpression)injectWithTagGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Action<object, object>> assignmentCallGenExpression = (item1, item2) => Inject<object>(default(IContainer), null, null);
            InjectingAssignmentGenericMethodInfo = ((MethodCallExpression)assignmentCallGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectExpression = () => Inject(default(IContainer), typeof(object));
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method;

            Expression<Func<object>> injectWithTagExpression = () => Inject(default(IContainer), typeof(object), (object)null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method;
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container, [CanBeNull] object tag)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static void Inject<T>(this IContainer container, [NotNull] T destination, [CanBeNull] T source)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject(this IContainer container, Type type)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject(this IContainer container, Type type, [CanBeNull] object tag)
        {
            throw new NotImplementedException(JustAMarkerError);
        }
    }
}
