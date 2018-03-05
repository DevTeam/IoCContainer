namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Extensibility;

    internal class ResolverExpressionBuilder: IResolverExpressionBuilder
    {
        public static readonly IResolverExpressionBuilder Shared = new ResolverExpressionBuilder();

        public bool TryBuild(BuildContext buildContext, IDependency dependency, ILifetime lifetime, out LambdaExpression resolverExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var typesMap = new Dictionary<Type, Type>();
                var expression = TypeReplacerExpressionBuilder.Shared.Build(dependency.Expression, buildContext, typesMap);
                expression = DependencyInjectionExpressionBuilder.Shared.Build(expression, buildContext);
                switch (dependency)
                {
                    case Autowring autowring:
                        if (autowring.Statements.Length == 0)
                        {
                            break;
                        }

                        expression = Expression.Block(CreateAutowringStatements(buildContext, autowring, expression, typesMap));
                        break;
                }

                expression = expression.Convert(buildContext.Key.Type);
                expression = buildContext.CloseBlock(expression);
                expression = LifetimeExpressionBuilder.Shared.Build(expression, buildContext, lifetime);
                expression = buildContext.CloseBlock(expression);

                resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, BuildContext.ResolverParameters);
                return true;
            }
            catch (BuildExpressionException)
            {
                resolverExpression = default(LambdaExpression);
                return false;
            }
        }

        private IEnumerable<Expression> CreateAutowringStatements(
            [NotNull] BuildContext buildContext,
            [NotNull] Autowring autowring,
            [NotNull] Expression newExpression,
            IDictionary<Type, Type> typesMap)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (autowring == null) throw new ArgumentNullException(nameof(autowring));
            if (newExpression == null) throw new ArgumentNullException(nameof(newExpression));
            if (autowring.Statements.Length == 0)
            {
                throw new ArgumentException($"{nameof(autowring.Statements)} should not be empty.");
            }

            var instanceExpression = buildContext.DefineValue(newExpression);
            foreach (var statement in autowring.Statements)
            {
                var statementExpression = TypeReplacerExpressionBuilder.Shared.Build(statement, buildContext, typesMap);
                statementExpression = DependencyInjectionExpressionBuilder.Shared.Build(statementExpression, buildContext, instanceExpression);
                yield return statementExpression;
            }

            yield return instanceExpression;
        }
    }
}
