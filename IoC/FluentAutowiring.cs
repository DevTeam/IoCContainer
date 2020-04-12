namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;

    /// <summary>
    /// Represents extensions for autowiring.
    /// </summary>
    public static class FluentAutowiring
    {
        private static readonly Expression ContainerExpression = Expression.Field(Expression.Constant(null, TypeDescriptor<Context>.Type), nameof(Context.Container));

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
            var parameterExpression = Expression.Call(Injections.InjectWithTagMethodInfo, ContainerExpression, Expression.Constant(dependencyType), Expression.Constant(dependencyTag)).Convert(dependencyType);
            method.SetParameterExpression(parameterPosition, parameterExpression);
            return true;
        }
    }
}
