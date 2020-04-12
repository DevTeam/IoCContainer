namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal sealed class Method<TMethodInfo>: IMethod<TMethodInfo> where TMethodInfo: MethodBase
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
                    var param = _parameters[parameterPosition];
                    Expression defaultExpression = null;
#if !NET40
                    if (param.HasDefaultValue)
                    {
                        defaultExpression = Expression.Constant(param.DefaultValue);
                    }

                    if (defaultExpression == null && param.CustomAttributes.Any(i => i.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute"))
                    {
                        defaultExpression = Expression.Default(param.ParameterType);
                    }
#else
                    if (param.IsOptional)
                    {
                        defaultExpression = Expression.Constant(param.DefaultValue);
                    }
#endif
                    var key = new Key(param.ParameterType);
                    yield return buildContext.CreateChild(key, buildContext.Container).GetDependencyExpression(defaultExpression);
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