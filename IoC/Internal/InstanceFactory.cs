namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal sealed class InstanceFactory : IFactory
    {
        private delegate object ConstructorFunc([NotNull][ItemCanBeNull] params object[] args);
        private delegate void MethodFunc([NotNull] object instance, [NotNull][ItemCanBeNull] params object[] args);

        [NotNull] private readonly IIssueResolver _issueResolver;
        [NotNull] private readonly ITypeInfo _typeInfo;
        private readonly MethodData _ctorData;
        private readonly ConstructorFunc _ctor;
        private readonly int _methodsCount;
        private readonly Tuple<MethodFunc, MethodData>[] _methods;
        private int _reentrancy;

        public InstanceFactory(
            [NotNull] IIssueResolver issueResolver,
            [NotNull] ITypeInfo typeInfo,
            [NotNull] Has[] dependencies)
        {
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            _issueResolver = issueResolver ?? throw new ArgumentNullException(nameof(issueResolver));
            _typeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));

            var ctorInfo = (
                from ctor in typeInfo.DeclaredConstructors
                where ctor.IsPublic
                let ctorItem = CreateCtorItem(ctor, dependencies)
                let dependenciesWithPosition = ConverToDependenciesWithPosition(ctorItem.Item2, dependencies)
                where IsCtorMatched(ctorItem.Item2, dependenciesWithPosition)
                select ctorItem).FirstOrDefault()
                ?? CreateCtorItem(issueResolver.CannotFindConsructor(typeInfo, dependencies), dependencies);

            _ctorData = CreateMethodData(typeInfo, ctorInfo.Item2, ctorInfo.Item3);
            _ctor = CreateConstructor(ctorInfo.Item1);
            _methods = _ctorData.Methods;
            _methodsCount = _methods.Length;
        }

        public object Create(Context context)
        {
            _reentrancy++;
            if (_reentrancy >= 32)
            {
                _issueResolver.CyclicDependenceDetected(context, _typeInfo, _reentrancy);
            }

            try
            {
                var args = _ctorData.Args;
                var factories = _ctorData.Factories;
                for (var position = 0; position < _ctorData.Count; position++)
                {
                    args[position] = factories[position].Create(context);
                }

                var instance = _ctor(args);
                if (_methodsCount == 0)
                {
                    return instance;
                }

                for (var i = 0; i < _methodsCount; i++)
                {
                    var method = _methods[i];
                    var parameters = method.Item2;
                    var methodArgs = parameters.Args;
                    var methodFactories = parameters.Factories;
                    for (var position = 0; position < parameters.Count; position++)
                    {
                        methodArgs[position] = methodFactories[position].Create(context);
                    }

                    method.Item1(instance, methodArgs);
                }

                return instance;
            }
            finally
            {
                _reentrancy--;
            }
        }

        private Tuple<ConstructorInfo, ParameterInfo[], DepWithPosition[]> CreateCtorItem(ConstructorInfo ctor, Has[] dependencies)
        {
            var parameters = ctor.GetParameters();
            var dependenciesWithPosition = ConverToDependenciesWithPosition(parameters, dependencies);
            return Tuple.Create(ctor, ctor.GetParameters(), dependenciesWithPosition);
        }

        private static ConstructorFunc CreateConstructor(ConstructorInfo constructor)
        {
            var argsParameter = Expression.Parameter(typeof(object[]), "args");
            var paramsExpression = CreateParameterExpressions(constructor, argsParameter);
            var create = Expression.New(constructor, paramsExpression);
            var lambda = Expression.Lambda<ConstructorFunc>(
                create,
                argsParameter);
            return lambda.Compile();
        }

        private MethodFunc CreateMethod(Type instanceType, MethodInfo method)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var argsParameter = Expression.Parameter(typeof(object[]), "args");
            var paramsExpression = CreateParameterExpressions(method, argsParameter);
            var call = Expression.Call(Expression.Convert(instanceParameter, instanceType), method, paramsExpression);
            var lambda = Expression.Lambda<MethodFunc>(
                call,
                instanceParameter,
                argsParameter);
            return lambda.Compile();
        }

        private static bool IsCtorMatched(ParameterInfo[] parameters, DepWithPosition[] dependencies)
        {
            return dependencies.Where(i => i.Dependency.Type == DependencyType.Arg).All(i =>
            {
                var ctorParam = i.Dependency.Parameter;
                var param = parameters[i.Position];

                return
                    i.Position == param.Position
                    && (ctorParam.Type == null || ctorParam.Type == param.ParameterType)
                    && ctorParam.Name == param.Name;
            });
        }

        private static Expression[] CreateParameterExpressions(MethodBase method, Expression argumentsParameter)
        {
            return method.GetParameters().Select(
                (parameter, index) => (Expression)Expression.Convert(
                    Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)),
                    parameter.ParameterType)).ToArray();
        }

        private DepWithPosition[] ConverToDependenciesWithPosition(ParameterInfo[] parameters, Has[] dependencies)
        {
            var result = new DepWithPosition[dependencies.Length];
            for (var index = 0; index < result.Length; index++)
            {
                var dependency = dependencies[index];
                if (dependency.Type != DependencyType.Arg && dependency.Type != DependencyType.Ref)
                {
                    result[index] = new DepWithPosition(dependency, 0);
                    continue;
                }

                var name = dependency.Parameter.Name;
                var type = dependency.Parameter.Type;
                var hasParameter = false;
                for (var position = 0; position < parameters.Length; position++)
                {
                    var parameter = parameters[position];
                    if ((type == null || type == parameter.ParameterType) && name == parameter.Name)
                    {
                        result[index] = new DepWithPosition(dependency, position);
                        hasParameter = true;
                        break;
                    }
                }

                if (!hasParameter)
                {
                    var position = _issueResolver.CannotFindParameter(parameters, dependency.Parameter);
                    result[index] = new DepWithPosition(dependency, position);
                }
            }

            return result;
        }

        private MethodData CreateMethodData(ITypeInfo typeInfo, ParameterInfo[] parameters, DepWithPosition[] dependencies)
        {
            var paramsCount = parameters.Length;
            var factories = new IFactory[paramsCount];
            var args = new object[paramsCount];
            var methods = new List<Tuple<MethodFunc, MethodData>>();
            foreach (var dependencyWithPosition in dependencies)
            {
                var dependency = dependencyWithPosition.Dependency;
                var position = dependencyWithPosition.Position;
                switch (dependency.Type)
                {
                    case DependencyType.Arg:
                        factories[position] = new ArgFactory(dependency.ArgIndex);
                        break;

                    case DependencyType.Ref:
                        factories[position] = new RefFactory(parameters[position].ParameterType, dependency.Tag, dependency.Scope);
                        break;

                    case DependencyType.Method:
                        var methodInfo = 
                            typeInfo.DeclaredMethods.FirstOrDefault(i => i.Name == dependency.HasMethod.Name)
                            ?? typeInfo.DeclaredProperties.Where(i => i.Name == dependency.HasMethod.Name).Select(i => i.SetMethod()).FirstOrDefault()
                            ?? throw new InvalidOperationException();

                        var methodParameters = methodInfo.GetParameters();
                        var methodDependenciesWithPosition = ConverToDependenciesWithPosition(methodParameters, dependency.HasMethod.Dependencies);
                        var methodData = CreateMethodData(typeInfo, methodInfo.GetParameters(), methodDependenciesWithPosition);
                        methods.Add(Tuple.Create(CreateMethod(typeInfo.Type, methodInfo), methodData));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"{dependency.Type}");
                }
            }

            for (var position = 0; position < paramsCount; position++)
            {
                if (factories[position] != null)
                {
                    continue;
                }

                var parameter = parameters[position];
                factories[position] = new RefFactory(parameter.ParameterType, null, Scope.Current);
            }

            return new MethodData(paramsCount, factories, args, methods.ToArray());
        }

        private struct DepWithPosition
        {
            internal readonly Has Dependency;
            internal readonly int Position;

            public DepWithPosition(Has dependency, int position)
            {
                Dependency = dependency;
                Position = position;
            }
        }

        private struct MethodData
        {
            internal readonly int Count;
            internal readonly IFactory[] Factories;
            internal readonly object[] Args;
            internal readonly Tuple<MethodFunc, MethodData>[] Methods;

            public MethodData(int count, IFactory[] factories, object[] args, Tuple<MethodFunc, MethodData>[] methods)
            {
                Count = count;
                Factories = factories;
                Args = args;
                Methods = methods;
            }
        }
    }
}
