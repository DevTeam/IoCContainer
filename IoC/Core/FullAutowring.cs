namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class FullAutowring: IDependency
    {
        private static readonly MethodInfo InjectMethodInfo;
        private static readonly Dictionary<Type, NewExpression> NewExpressions = new Dictionary<Type, NewExpression>();
        private static readonly Dictionary<Type, Expression> InjectionExpressions = new Dictionary<Type, Expression>();

        static FullAutowring()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
        }

        public FullAutowring([NotNull] Type type, [CanBeNull] Predicate<ConstructorInfo> constructorFilter = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (constructorFilter == null)
            {
                lock (NewExpressions)
                {
                    if (!NewExpressions.TryGetValue(type, out var newExpression))
                    {
                        var constructorInfo = EnumerateConstructors(type).First();
                        newExpression = CreateNewExpression(constructorInfo);
                        NewExpressions.Add(type, newExpression);
                    }

                    Expression = newExpression;
                }
            }
            else
            {
                var constructorInfo = EnumerateConstructors(type).First(ctor => constructorFilter(ctor));
                Expression = CreateNewExpression(constructorInfo);
            }
        }

        public Expression Expression { get; }

        [MethodImpl((MethodImplOptions)256)]
        private static NewExpression CreateNewExpression(ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters();
            var parameterExpressions = new Expression[parameters.Length];
            for (var position = 0; position < parameters.Length; position++)
            {
                var paramType = parameters[position].ParameterType;
                Expression injectionExpression;
                lock (InjectionExpressions)
                {
                    if (!InjectionExpressions.TryGetValue(paramType, out injectionExpression))
                    {
                        var methodInfo = InjectMethodInfo.MakeGenericMethod(paramType);
                        var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                        injectionExpression = Expression.Call(methodInfo, containerExpression);
                        InjectionExpressions.Add(paramType, injectionExpression);
                    }
                }

                parameterExpressions[position] = injectionExpression;
            }

            return Expression.New(constructorInfo, parameterExpressions);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IOrderedEnumerable<ConstructorInfo> EnumerateConstructors([NotNull] Type type)
        {
            var typeInfo = type.Info().ToDefinedGenericType().Info();
            return typeInfo.DeclaredConstructors
                .Where(ctor => !ctor.IsStatic && !ctor.IsPrivate)
                .OrderBy(ctor => ctor.GetParameters().Length);
        }
    }
}
