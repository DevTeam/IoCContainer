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
        [NotNull] private static Cache<Type, Expression> _injections = new Cache<Type, Expression>();
        private readonly Expression[] _parametersExpressions;
        private readonly ParameterInfo[] _parameters;

        public Method([NotNull] TMethodInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            _parameters = info.GetParameters();
            _parametersExpressions = new Expression[_parameters.Length];
        }

        public TMethodInfo Info { get; }

        public IEnumerable<Expression> GetParametersExpressions()
        {
            for (var parameterPosition = 0; parameterPosition < _parametersExpressions.Length; parameterPosition++)
            {
                var expression = _parametersExpressions[parameterPosition];
                if (expression != null)
                {
                    yield return expression;
                }
                else
                {
                    var paramType = _parameters[parameterPosition].ParameterType;
                    yield return _injections.GetOrCreate(paramType, () =>
                    {
                        var containerExpression = Expression.Field(Expression.Constant(null, TypeDescriptor<Context>.Type), nameof(Context.Container));
                        return Expression.Call(InjectMethodInfo, containerExpression, Expression.Constant(paramType)).Convert(paramType);
                    });
                }
            }
        }

        public void SetParameterExpression(int parameterPosition, Expression parameterExpression)
        {
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            _parametersExpressions[parameterPosition] = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }
    }
}