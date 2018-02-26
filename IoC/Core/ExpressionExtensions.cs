namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class ExpressionExtensions
    {
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));
        public static readonly ParameterExpression[] Parameters = { ContainerParameter, ArgsParameter };

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

        [NotNull]
        public static Type ConvertToDefinedGenericType([NotNull] this Type type)
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
