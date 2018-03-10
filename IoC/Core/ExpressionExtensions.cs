namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Extensibility;

    internal static class ExpressionExtensions
    {
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

                return ExpressionCompilerDefault.Shared;
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