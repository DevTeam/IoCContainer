namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class FullAutowring: IDependency
    {
        private static readonly Type[] GenericTypes = {typeof(TT), typeof(TT1), typeof(TT2), typeof(TT3), typeof(TT4), typeof(TT5), typeof(TT6), typeof(TT7), typeof(TT8) };
        private static readonly MethodInfo InjectMethodInfo;

        static FullAutowring()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
        }

        public FullAutowring([NotNull] Type type, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeInfo = type.Info();
            if (typeInfo.IsGenericTypeDefinition)
            {
                var genericTypeArguments = typeInfo.GenericTypeParameters;
                var genericCounter = GenericTypes.Length;
                for (var position = 0; position < genericTypeArguments.Length; position++)
                {
                    if (genericTypeArguments[position].IsGenericParameter)
                    {
                        genericCounter--;
                        if (genericCounter < 0)
                        {
                            throw new InvalidOperationException();
                        }

                        genericTypeArguments[position] = GenericTypes[genericCounter];
                    }
                }

                typeInfo = typeInfo.MakeGenericType(genericTypeArguments).Info();
            }

            var constructorInfo = typeInfo.DeclaredConstructors.First(i => constructorFilter == null || constructorFilter(i));
            var parameters = constructorInfo.GetParameters();
            var parameterExpressions = new Expression[parameters.Length];
            for (var position = 0; position < parameters.Length; position++)
            {
                var itMethod = InjectMethodInfo.MakeGenericMethod(parameters[position].ParameterType);
                var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                parameterExpressions[position] = Expression.Call(itMethod, containerExpression);
            }

            Expression = Expression.New(constructorInfo, parameterExpressions);
        }

        public Expression Expression { get; }
    }
}
