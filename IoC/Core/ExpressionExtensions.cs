namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Extensibility;
    using static TypeExtensions;

    internal static class ExpressionExtensions
    {
        private static readonly ITypeInfo ResolverGenericTypeInfo = typeof(Resolver<>).Info();
        [ThreadStatic] private static int _getExpressionCompilerReentrancy;
        internal static readonly MethodInfo GetHashCodeMethodInfo = Info<object>().DeclaredMethods.Single(i => i.Name == nameof(GetHashCode));
        internal static readonly Expression NullConst = Expression.Constant(null);
        private static readonly MethodInfo EnterMethodInfo = typeof(Monitor).Info().DeclaredMethods.Single(i => i.Name == nameof(Monitor.Enter) && i.GetParameters().Length == 1);
        private static readonly MethodInfo ExitMethodInfo = typeof(Monitor).Info().DeclaredMethods.Single(i => i.Name == nameof(Monitor.Exit));

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
        public static Type ToResolverType(this Type type) => ResolverGenericTypeInfo.MakeGenericType(type);

        public static BlockExpression LockExpression(this Expression body, MemberExpression lockObject)
        {
            return Expression.Block(
                Expression.TryFinally(
                    Expression.Block(
                        Expression.Call(EnterMethodInfo, lockObject),
                        body), 
                    Expression.Call(ExitMethodInfo, lockObject)));
        }
    }
}