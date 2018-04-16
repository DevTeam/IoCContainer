namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Extensibility;
    using static TypeDescriptorExtensions;

    internal static class ExpressionExtensions
    {
        private static readonly TypeDescriptor ResolverGenericTypeDescriptor = typeof(Resolver<>).Descriptor();
        [ThreadStatic] private static int _getExpressionCompilerReentrancy;
        internal static readonly MethodInfo GetHashCodeMethodInfo = Descriptor<object>().GetDeclaredMethods().Single(i => i.Name == nameof(GetHashCode));
        internal static readonly Expression NullConst = Expression.Constant(null);
        private static readonly MethodInfo EnterMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Enter) && i.GetParameters().Length == 1);
        private static readonly MethodInfo ExitMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Exit));

        public static IExpressionCompiler GetExpressionCompiler(this IContainer container)
        {
            _getExpressionCompilerReentrancy++;
            try
            {
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
            var baseTypeDescriptor = expression.Type.Descriptor();
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsAssignableFrom(baseTypeDescriptor))
            {
                return expression;
            }

            return Expression.Convert(expression, type);
        }

        [MethodImpl((MethodImplOptions)256)]
        public static Type ToResolverType(this Type type) => ResolverGenericTypeDescriptor.MakeGenericType(type);

        [MethodImpl((MethodImplOptions)256)]
        public static Expression Lock(this Expression body, MemberExpression lockObject)
        {
            return Expression.TryFinally(
                Expression.Block(
                    Expression.Call(EnterMethodInfo, lockObject),
                    body), 
                Expression.Call(ExitMethodInfo, lockObject));
        }
    }
}