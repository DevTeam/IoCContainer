namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class Method<TMethodInfo>: IMethod<TMethodInfo> where TMethodInfo: MethodBase
    {
        private readonly Expression[] _parametersExpressions;
        private readonly ParameterInfo[] _parameters;

        public Method([NotNull] TMethodInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            _parameters = info.GetParameters();
            _parametersExpressions = new Expression[_parameters.Length];
        }

        public TMethodInfo Info { get; }

        public IEnumerable<Expression> GetParametersExpressions(IBuildContext buildContext)
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
                    var key = new Key(_parameters[parameterPosition].ParameterType);
                    yield return buildContext.CreateChild(key, buildContext.Container).DependencyExpression;
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