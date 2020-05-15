namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal sealed class Method<TMethodInfo> : IMethod<TMethodInfo> where TMethodInfo : MethodBase
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
                var param = _parameters[parameterPosition];
                if (param.IsOut)
                {
                    if (param.ParameterType.HasElementType)
                    {
                        var type = param.ParameterType.GetElementType();
                        if (type != null)
                        {
                            var outParam = Expression.Parameter(type);
                            buildContext.AddParameter(outParam);
                            yield return outParam;
                            continue;
                        }
                    }
                }

                var expression = _parametersExpressions[parameterPosition];
                if (expression != null)
                {
                    yield return expression;
                }
                else
                {
                    var key = new Key(param.ParameterType);
                    Expression defaultExpression;
                    if (param.IsOptional)
                    {
                        if (param.DefaultValue == null)
                        {
                            defaultExpression = Expression.Default(param.ParameterType);
                        }
                        else
                        {
                            defaultExpression = Expression.Constant(param.DefaultValue);
                        }
                    }
                    else
                    {
                        defaultExpression = null;
                    }

                    yield return buildContext.Create(key, buildContext.Container).GetDependencyExpression(defaultExpression);
                }
            }
        }

        public void SetExpression(int parameterPosition, Expression parameterExpression)
        {
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));

            _parametersExpressions[parameterPosition] = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }

        public void SetDependency(int parameterPosition, Type dependencyType, object dependencyTag = null, bool isOptional = false, params object[] args)
        {
            if (dependencyType == null) throw new ArgumentNullException(nameof(dependencyType));
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));

            var injectMethod = isOptional ? Injections.TryInjectWithTagMethodInfo : Injections.InjectWithTagMethodInfo;
            var parameterExpression = Expression.Call(
                    injectMethod,
                    ExpressionBuilderExtensions.ContainerExpression,
                    Expression.Constant(dependencyType),
                    Expression.Constant(dependencyTag),
                    Expression.NewArrayInit(typeof(object), args.Select(Expression.Constant)))
                .Convert(dependencyType);

            SetExpression(parameterPosition, parameterExpression.Convert(dependencyType));
        }
    }
}