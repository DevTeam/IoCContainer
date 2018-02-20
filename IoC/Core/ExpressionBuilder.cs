namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;

    internal class ExpressionBuilder
    {
        public static  readonly ExpressionBuilder Shared = new ExpressionBuilder();
        private static readonly ITypeInfo ResolverGenericTypeInfo = typeof(Resolver<>).Info();
        private static readonly MethodInfo LifetimeGenericGetOrCreateMethodInfo = TypeExtensions.Info<ILifetime>().DeclaredMethods.Single(i => i.Name == nameof(ILifetime.GetOrCreate));

        public Expression PrepareExpression(Expression expression, Type type, IDictionary<Type, Type> typesMap)
        {
            var typeMapingExpressionVisitor = new TypeMapingExpressionVisitor(type, typesMap);
            typeMapingExpressionVisitor.Visit(expression);
            if (typesMap.Count > 0)
            {
                var typeReplacingExpressionVisitor = new TypeReplacingExpressionVisitor(typesMap);
                expression = typeReplacingExpressionVisitor.Visit(expression);
            }

            return expression;
        }

        public Expression Convert(Expression expression, Type type)
        {
            var baseTypeInfo = expression.Type.Info();
            var typeInfo = type.Info();
            if (typeInfo.IsAssignableFrom(baseTypeInfo))
            {
                return expression;
            }

            return Expression.Convert(expression, type);
        }

        public Expression AddLifetime([NotNull] Expression expression, [CanBeNull] ILifetime lifetime, [NotNull] Type type, ExpressionVisitor injectingExpressionVisitor)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (lifetime == null)
            {
                return expression;
            }

            if (lifetime is IExpressionBuilder builder)
            {
                return builder.Build(injectingExpressionVisitor.Visit(expression));
            }

            var getOrCreateMethodInfo = LifetimeGenericGetOrCreateMethodInfo.MakeGenericMethod(type);
            var resolverType = ResolverGenericTypeInfo.MakeGenericType(type);
            expression = injectingExpressionVisitor.Visit(expression) ?? throw new BuildExpressionException(new InvalidOperationException("Null expression"));
            var resolverExpression = Expression.Lambda(resolverType, expression, true, ResolverExpressionBuilder.Parameters);
            var resolver = resolverExpression.Compile();
            var lifetimeCall = Expression.Call(Expression.Constant(lifetime), getOrCreateMethodInfo, ResolverExpressionBuilder.ContainerParameter, ResolverExpressionBuilder.ArgsParameter, Expression.Constant(resolver));
            return lifetimeCall;
        }

        [NotNull]
        public Type ConvertToDefinedGenericType([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeInfo = type.Info();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                return type;
            }

            var genericTypeParameters = typeInfo.GenericTypeParameters;
            var typesMap = genericTypeParameters.Distinct().Zip(GenericTypeArguments.Types, Tuple.Create).ToDictionary(i => i.Item1, i => i.Item2);
            var genericTypeArguments = new Type[genericTypeParameters.Length];
            for (var position = 0; position < genericTypeParameters.Length; position++)
            {
                genericTypeArguments[position] = typesMap[genericTypeParameters[position]];
            }

            return typeInfo.MakeGenericType(genericTypeArguments);
        }
    }
}
