namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal static class ExpressionExtensions
    {
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));
        public static readonly ParameterExpression[] Parameters = { ContainerParameter, ArgsParameter };
        public static readonly Type[] ParameterTypes = Parameters.Select(i => i.Type).ToArray();
        private static readonly ITypeInfo ResolverGenericTypeInfo = typeof(Resolver<>).Info();
        [ThreadStatic] private static int _getExpressionCompilerReentrancy;

        public static IExpressionCompiler GetExpressionCompiler(this IContainer container)
        {
            try
            {
                _getExpressionCompilerReentrancy++;
                if (_getExpressionCompilerReentrancy == 1)
                {
                    if (container.TryGetResolver<IExpressionCompiler>(typeof(IExpressionCompiler), out var resolver))
                    {
                        return resolver(container);
                    }
                }

                return ExpressionCompilerSkippingSecurityCheck.Shared;
            }
            finally
            {
                _getExpressionCompilerReentrancy--;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
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

        [MethodImpl((MethodImplOptions)256)]
        public static Type ToResolverType(this Type type)
        {
            return ResolverGenericTypeInfo.MakeGenericType(type);
        }
    }
}