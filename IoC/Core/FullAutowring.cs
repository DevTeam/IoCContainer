﻿namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class FullAutowring: IDependency
    {
        private static readonly MethodInfo InjectMethodInfo;

        static FullAutowring()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
        }

        public FullAutowring([NotNull] Type type, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            type = type.Info().ToDefinedGenericType();
            var typeInfo = type.Info();
            var constructorInfo = typeInfo.DeclaredConstructors
                .Where(ctor => !ctor.IsStatic && !ctor.IsPrivate)
                .OrderBy(ctor => ctor.GetParameters().Length)
                .First(ctor => constructorFilter == null || constructorFilter(ctor));

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