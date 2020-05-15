// ReSharper disable RedundantNameQualifier
namespace IoC.Core
{
    using System;
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
            this IBuildContext buildContext,
            IAutowiringStrategy autoWiringStrategy,
            TypeDescriptor typeDescriptor,
            Expression baseExpression,
            IEnumerable<Expression> statements,
            ILifetime lifetime,
            IDictionary<Type, Type> typesMap)
        {
            var isDefaultAutoWiringStrategy = DefaultAutowiringStrategy.Shared == autoWiringStrategy;
            var defaultMethods = typeDescriptor.GetDeclaredMethods().FilterMethods();
            if (!autoWiringStrategy.TryResolveInitializers(defaultMethods, out var initializers))
            {
                if (isDefaultAutoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(defaultMethods, out initializers))
                {
                    initializers = Enumerable.Empty<IMethod<MethodInfo>>();
                }
            }

            baseExpression = TypeReplacerExpressionBuilder.Shared.Build(baseExpression, buildContext, typesMap);
            var thisVar = Expression.Variable(baseExpression.Type);

            var initializerExpressions =
                initializers.Select(initializer => (Expression)Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext)))
                    .Concat(statements.Select(expression => TypeReplacerExpressionBuilder.Shared.Build(expression, buildContext, typesMap)))
                    .ToArray();

            if (initializerExpressions.Length > 0)
            {
                baseExpression = Expression.Block(
                    new[] { thisVar },
                    Expression.Assign(thisVar, baseExpression),
                    Expression.Block(initializerExpressions),
                    thisVar
                );
            }

            baseExpression = DependencyInjectionExpressionBuilder.Shared.Build(baseExpression, buildContext, thisVar);
            return buildContext.AddLifetime(baseExpression, lifetime);
        }

        [IoC.NotNull]
        [MethodImpl((MethodImplOptions)0x100)]
        public static IEnumerable<IMethod<TMethodInfo>> FilterMethods<TMethodInfo>([IoC.NotNull] this IEnumerable<TMethodInfo> methodInfos)
            where TMethodInfo : MethodBase
            => methodInfos
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<TMethodInfo>(info));
    }
}
