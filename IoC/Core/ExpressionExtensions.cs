namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal static class ExpressionExtensions
    {
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));
        public static readonly ParameterExpression[] Parameters = { ContainerParameter, ArgsParameter };

        public static Expression Convert(this Expression expression, Type type)
        {
            var baseTypeInfo = expression.Type.Info();
            var typeInfo = type.Info();
            if (typeInfo.IsAssignableFrom(baseTypeInfo))
            {
                return expression;
            }

            return Expression.Convert(expression, type);
        }
    }
}
