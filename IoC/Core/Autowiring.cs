// ReSharper disable RedundantNameQualifier
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class Autowiring
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static Expression ApplyInitializers(
            this IBuildContext buildContext,
            IAutowiringStrategy autoWiringStrategy,
            TypeDescriptor typeDescriptor,
            Expression expression,
            IEnumerable<Expression> statements,
            ILifetime lifetime,
            IDictionary<Type, Type> typesMap)
        {
            expression = ReplaceTypes(buildContext, expression, typesMap);
            var thisVar = Expression.Variable(expression.Type);

            var initializerExpressions = 
                GetInitializers(autoWiringStrategy, typeDescriptor)
                    .Select(initializer => (Expression)Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext)))
                    .Concat(statements.Select(statementExpression => ReplaceTypes(buildContext, statementExpression, typesMap)))
                    .ToArray();

            if (initializerExpressions.Length > 0)
            {
                expression = Expression.Block(
                    new[] { thisVar },
                    Expression.Assign(thisVar, expression),
                    Expression.Block(initializerExpressions),
                    thisVar
                );
            }

            expression = DependencyInjectionExpressionBuilder.Shared.Build(expression, buildContext, thisVar);
            return buildContext.FinalizeExpression(expression, lifetime);
        }

        private static Expression ReplaceTypes(IBuildContext buildContext, Expression expression, IDictionary<Type, Type> typesMap) =>
            TypeReplacerExpressionBuilder.Shared.Build(expression, buildContext, typesMap);

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IEnumerable<IMethod<MethodInfo>> GetInitializers(IAutowiringStrategy autoWiringStrategy, TypeDescriptor typeDescriptor)
        {
            var methods = typeDescriptor.GetDeclaredMethods().Select(info => new Method<MethodInfo>(info));
            if (autoWiringStrategy.TryResolveInitializers(methods, out var initializers))
            {
                return initializers;
            }

            if (DefaultAutowiringStrategy.Shared == autoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(methods, out initializers))
            {
                initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            }

            return initializers;
        }
    }
}
