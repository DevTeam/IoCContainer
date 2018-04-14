namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents extensions for autowring.
    /// </summary>
    public static class FluentAutowiring
    {
        /// <summary>
        /// Injects dependency to parameter.
        /// </summary>
        /// <typeparam name="TMethodInfo"></typeparam>
        /// <param name="method">The target method or constructor.</param>
        /// <param name="parameterPosition">The parameter's position.</param>
        /// <param name="dependencyType">The dependency's type.</param>
        /// <param name="dependencyTag">The optional dependency's tag value.</param>
        /// <returns>True if success.</returns>
        public static bool TryInjectDependency<TMethodInfo>([NotNull] this IMethod<TMethodInfo> method, int parameterPosition, [NotNull] Type dependencyType, [CanBeNull] object dependencyTag = null)
            where TMethodInfo: MethodBase
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (dependencyType == null) throw new ArgumentNullException(nameof(dependencyType));
            if (parameterPosition < 0) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            var methodInfo = Injections.InjectWithTagMethodInfo.MakeGenericMethod(dependencyType);
            var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
            var parameterExpression = Expression.Call(methodInfo, containerExpression, Expression.Constant(dependencyTag));
            method.SetParameterExpression(parameterPosition, parameterExpression);
            return true;
        }
    }
}
