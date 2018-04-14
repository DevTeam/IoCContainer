namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using static Injections;

    internal class Method<TMethodInfo>: IMethod<TMethodInfo> where TMethodInfo: MethodBase
    {
        // ReSharper disable once StaticMemberInGenericType
        [NotNull] private static Cache<Type, MethodCallExpression> _injections = new Cache<Type, MethodCallExpression>();
        private Lazy<Expression[]> _parametersExpressions;

        public Method([NotNull] TMethodInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            _parametersExpressions = new Lazy<Expression[]>(() => CreateParametersExpressions(info.GetParameters()));
        }

        public TMethodInfo Info { get; }

        public IEnumerable<Expression> GetParametersExpressions() => _parametersExpressions.Value;

        public void SetParameterExpression(int parameterPosition, Expression parameterExpression)
        {
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Value.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            _parametersExpressions.Value[parameterPosition] = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }

        private static Expression[] CreateParametersExpressions(ParameterInfo[] parameters)
        {
            var parametersExpressions = new Expression[parameters.Length];
            for (var i = 0; i < parametersExpressions.Length; i++)
            {
                var parameter = parameters[i];
                var paramType = parameter.ParameterType;
                parametersExpressions[i] = _injections.GetOrCreate(paramType, () =>
                {
                    var methodInfo = InjectMethodInfo.MakeGenericMethod(paramType);
                    var containerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
                    return Expression.Call(methodInfo, containerExpression);
                });
            }

            return parametersExpressions;
        }
    }
}