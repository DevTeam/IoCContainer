namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class ExpressionBuilderExtensions
    {
        private static readonly TypeDescriptor ResolverGenericTypeDescriptor = typeof(Resolver<>).Descriptor();
        internal static readonly Expression NullConst = Expression.Constant(null);
        internal static readonly Expression ContainerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
        internal static readonly MethodInfo EqualsMethodInfo = typeof(Object).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Equals) && i.ReturnType == typeof(bool) && i.GetParameters().Length == 2 && i.GetParameters()[0].ParameterType == typeof(object) && i.GetParameters()[1].ParameterType == typeof(object));
        private static readonly MethodInfo EnterMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Enter) && i.GetParameters().Length == 1);
        private static readonly MethodInfo ExitMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Exit));

        [MethodImpl((MethodImplOptions)0x100)]
        public static Expression Convert(this Expression expression, Type type)
        {
            var targetType = expression.Type.Descriptor();
            if (type != typeof(object))
            {
                var typeDescriptor = type.Descriptor();
                if (typeDescriptor.IsAssignableFrom(targetType))
                {
                    return expression;
                }
            }

            return Expression.Convert(expression, type);
        }

        [MethodImpl((MethodImplOptions)0x100)]
        public static Type ToResolverType(this Type type) =>
            ResolverGenericTypeDescriptor.MakeGenericType(type);

        [MethodImpl((MethodImplOptions)0x100)]
        public static Expression Lock(this Expression body, Expression lockObject) =>
            Expression.TryFinally(
                Expression.Block(
                    Expression.Call(EnterMethodInfo, lockObject),
                    body), 
                Expression.Call(ExitMethodInfo, lockObject));


        public static Expression BuildGetOrCreateInstance<T>([NotNull] T owner, [NotNull] IBuildContext context, [NotNull] Expression bodyExpression, string methodName)
        {
            var resolverType = bodyExpression.Type.ToResolverType();
            var resolver = Expression.Constant(Expression.Lambda(resolverType, bodyExpression, false, context.ContainerParameter, context.ArgsParameter).Compile());
            return Expression.Call(
                Expression.Constant(owner),
                methodName,
                new[] { bodyExpression.Type },
                resolver,
                context.ContainerParameter,
                context.ArgsParameter);
        }
    }
}