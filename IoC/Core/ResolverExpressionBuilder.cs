namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;

    internal class ResolverExpressionBuilder: IResolverExpressionBuilder
    {
        public static readonly IResolverExpressionBuilder Shared = new ResolverExpressionBuilder();

        public bool TryBuild(Key key, IContainer container, IDependency dependency, ILifetime lifetime, out LambdaExpression resolverExpression)
        {
            try
            {
                var typesMap = new Dictionary<Type, Type>();
                var expression = TypeReplacerExpressionBuilder.Shared.Build(dependency.Expression, key, container, typesMap);
                expression = DependencyInjectionExpressionBuilder.Shared.Build(expression, key, container);
                switch (dependency)
                {
                    case Autowring autowring:
                        if (autowring.Statements.Length == 0)
                        {
                            break;
                        }

                        var vars = new Collection<ParameterExpression>();
                        var statements = CreateAutowringStatements(key, container, autowring, expression, vars, typesMap).ToList();
                        expression = Expression.Block(vars, statements);
                        break;
                }

                expression = expression.Convert(key.Type);
                expression = LifetimeExpressionBuilder.Shared.Build(expression, key, container, lifetime);
                resolverExpression = Expression.Lambda(key.Type.ToResolverType(), expression, false, ExpressionExtensions.Parameters);
                return true;
            }
            catch (BuildExpressionException)
            {
                resolverExpression = default(LambdaExpression);
                return false;
            }
        }

        private IEnumerable<Expression> CreateAutowringStatements(
            Key key,
            IContainer container,
            Autowring autowring,
            Expression newExpression,
            ICollection<ParameterExpression> vars,
            IDictionary<Type, Type> typesMap)
        {
            if (autowring.Statements.Length == 0)
            {
                throw new ArgumentException($"{nameof(autowring.Statements)} should not be empty.");
            }

            var instanceExpression = Expression.Variable(newExpression.Type);
            vars.Add(instanceExpression);
            yield return instanceExpression;
            yield return Expression.Assign(instanceExpression, newExpression);
            foreach (var statement in autowring.Statements)
            {
                var statementExpression = TypeReplacerExpressionBuilder.Shared.Build(statement, key, container, typesMap);
                statementExpression = DependencyInjectionExpressionBuilder.Shared.Build(statementExpression, key, container, instanceExpression);
                yield return statementExpression;
            }

            yield return instanceExpression;
        }
    }
}
