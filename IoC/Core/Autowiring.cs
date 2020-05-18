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
            Expression createInstanceExpression,
            IEnumerable<Expression> initializeInstanceStatements,
            ILifetime lifetime,
            IDictionary<Type, Type> typesMap)
        {
            createInstanceExpression = ReplaceTypes(buildContext, createInstanceExpression, typesMap);
            var thisVar = Expression.Variable(createInstanceExpression.Type);

            var initializeExpressions = 
                GetInitializers(autoWiringStrategy, typeDescriptor)
                    .Select(initializer => (Expression)Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext)))
                    .Concat(initializeInstanceStatements.Select(statementExpression => ReplaceTypes(buildContext, statementExpression, typesMap)))
                    .ToArray();

            if (initializeExpressions.Length > 0)
            {
                createInstanceExpression = Expression.Block(
                    new[] { thisVar },
                    Expression.Assign(thisVar, createInstanceExpression),
                    Expression.Block(initializeExpressions),
                    thisVar
                );
            }

            createInstanceExpression = DependencyInjectionExpressionBuilder.Shared.Build(createInstanceExpression, buildContext, thisVar);
            return buildContext.FinalizeExpression(createInstanceExpression, lifetime);
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
