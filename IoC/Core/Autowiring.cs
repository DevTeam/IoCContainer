namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class Autowiring
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static Expression ApplyInitializers(
            IBuildContext buildContext,
            IAutowiringStrategy autoWiringStrategy,
            TypeDescriptor typeDescriptor,
            bool isComplexType,
            Expression baseExpression,
            Expression[] statements)
        {
            var isDefaultAutoWiringStrategy = DefaultAutowiringStrategy.Shared == autoWiringStrategy;
            var defaultMethods = GetMethods(typeDescriptor.GetDeclaredMethods());
            if (!autoWiringStrategy.TryResolveInitializers(defaultMethods, out var initializers))
            {
                if (isDefaultAutoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(defaultMethods, out initializers))
                {
                    initializers = Enumerable.Empty<IMethod<MethodInfo>>();
                }
            }

            var curInitializers = initializers.ToArray();
            if (curInitializers.Length > 0)
            {
                var thisVar = Expression.Variable(baseExpression.Type, "this");
                baseExpression = Expression.Block(
                    new[] { thisVar },
                    Expression.Assign(thisVar, baseExpression),
                    Expression.Block(
                        from initializer in initializers
                        select Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext))
                    ),
                    thisVar
                );
            }

            if (!isDefaultAutoWiringStrategy)
            {
                baseExpression = buildContext.InjectDependencies(baseExpression);
            }

            if (statements.Length == 0)
            {
                return baseExpression;
            }

            var contextItVar = ReplaceTypes(buildContext, isComplexType, Expression.Variable(baseExpression.Type, "ctx.It"));
            baseExpression = Expression.Block(
                new[] { contextItVar },
                Expression.Assign(contextItVar, baseExpression),
                ReplaceTypes(buildContext, isComplexType, Expression.Block(statements)),
                contextItVar
            );

            return buildContext.InjectDependencies(baseExpression, contextItVar);
        }

        public static bool IsComplexType(TypeDescriptor typeDescriptor) => 
            typeDescriptor.IsConstructedGenericType() || typeDescriptor.IsGenericTypeDefinition() || typeDescriptor.IsArray();

        public static T ReplaceTypes<T>(IBuildContext buildContext, bool isComplexType, T expression)
            where T : Expression =>
            isComplexType ? (T)buildContext.ReplaceTypes(expression) : expression;

        [NotNull]
        [MethodImpl((MethodImplOptions)256)]
        public static IEnumerable<IMethod<TMethodInfo>> GetMethods<TMethodInfo>([NotNull] IEnumerable<TMethodInfo> methodInfos)
            where TMethodInfo : MethodBase
            => methodInfos
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<TMethodInfo>(info));
    }
}
