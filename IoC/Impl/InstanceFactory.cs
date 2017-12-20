namespace IoC.Impl
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class InstanceFactory: IFactory
    {
        private readonly Constructor _ctor;
        private readonly IFactory[] _factories;
        private readonly object[] _args;
        private int _paramLength;

        public InstanceFactory([NotNull] TypeInfo typeInfo, [NotNull] params Dependency[] dependencies)
        {
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            var maxParamsIndex = dependencies.Any() ? dependencies.Where(i => i.Type == DependencyType.Arg).Max(i => i.ArgIndex) : -1;
            var ctorInfo = (
                from ctor in typeInfo.DeclaredConstructors
                where ctor.IsPublic
                let parameters = ctor.GetParameters()
                where parameters.Length > maxParamsIndex && IsCtorMatched(parameters, dependencies)
                select new { ctor, parameters })
            .FirstOrDefault() ?? throw new InvalidOperationException();

            var param = ctorInfo.parameters;
            _paramLength = param.Length;
            _factories = new IFactory[_paramLength];
            _args = new object[_paramLength];
            foreach (var dependency in dependencies)
            {
                var position = dependency.Parameter.Position;
                switch (dependency.Type)
                {
                    case DependencyType.Arg:
                        _factories[position] = new ArgFactory(dependency.ArgIndex);
                        break;

                    case DependencyType.Ref:
                        _factories[position] = new RefFactory(param[position].ParameterType, dependency.Tag.Value);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"{dependency.Type}");
                }
            }

            for (var position = 0; position < _paramLength; position++)
            {
                if (_factories[position] != null)
                {
                    continue;
                }

                var parameter = param[position];
                _factories[position] = new RefFactory(parameter.ParameterType, null);
            }

            _ctor = CreateConstructor(ctorInfo.ctor);
        }

        public object Create(Context context)
        {
            for (var position = 0; position < _paramLength; position++)
            {
                _args[position] = _factories[position].Create(context);
            }

            return _ctor(_args);
        }

        private static Constructor CreateConstructor(ConstructorInfo constructor)
        {
            var argsParameter = Expression.Parameter(typeof(object[]), "args");
            var paramsExpression = CreateParameterExpressions(constructor, argsParameter);
            var create = Expression.New(constructor, paramsExpression);
            var lambda = Expression.Lambda<Constructor>(
                create,
                argsParameter);
            return lambda.Compile();
        }

        private static bool IsCtorMatched(ParameterInfo[] parameters, Dependency[] dependencies)
        {
            return dependencies.Where(i => i.Type == DependencyType.Arg).All(i =>
            {
                var ctorParam = i.Parameter;
                var param = parameters[ctorParam.Position];

                return
                    ctorParam.Position == param.Position
                    && (ctorParam.Type == null || ctorParam.Type == param.ParameterType)
                    && (string.IsNullOrWhiteSpace(ctorParam.Name) || ctorParam.Name == param.Name);
            });
        }

        private static Expression[] CreateParameterExpressions(MethodBase method, Expression argumentsParameter)
        {
            return method.GetParameters().Select(
                (parameter, index) => (Expression)Expression.Convert(
                    Expression.ArrayIndex(
                        argumentsParameter,
                        Expression.Constant(index)),
                    parameter.ParameterType)).ToArray();
        }

        private delegate object Constructor([NotNull][ItemCanBeNull] params object[] args);

        private struct ArgFactory: IFactory
        {
            private readonly int _argIndex;

            public ArgFactory(int argIndex)
            {
                _argIndex = argIndex;
            }

            public object Create(Context context)
            {
                return context.Args[_argIndex];
            }
        }

        private struct RefFactory : IFactory
        {
            private readonly Type _contractType;
            private readonly Key _key;
            [CanBeNull] private IContainer _lastContainer;
            private IResolver _lastResolver;

            public RefFactory(Type contractType, object tagValue)
            {
                _contractType = contractType;
                _key = new Key(new Contract(contractType), new Tag(tagValue));
                _lastContainer = null;
                _lastResolver = null;
            }

            public object Create(Context context)
            {
                if (_lastContainer != context.ResolvingContainer)
                {
                    if (!context.ResolvingContainer.TryGetResolver(_key, out _lastResolver))
                    {
                        throw new InvalidOperationException();
                    }

                    _lastContainer = context.ResolvingContainer;
                }

                return _lastResolver.Resolve(_lastContainer, _contractType);
            }
        }
    }
}
