namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core;
    using Dependencies;

    [PublicAPI]
    public static class Has
    {
        public static DependencyPosition At([NotNull] this IDependency dependency, int parameterPosition)
        {
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            if (parameterPosition < 0) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            return new DependencyPosition(dependency, parameterPosition);
        }

        public static DependencyPosition For([NotNull] this IDependency dependency, string parameterName)
        {
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            if (string.IsNullOrWhiteSpace(parameterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));
            return new DependencyPosition(dependency, parameterName);
        }

        [NotNull]
        public static Argument Argument([NotNull] Type type, int argIndex)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Argument(type.Info(), argIndex);
        }

        [NotNull]
        public static Argument Argument<T>(int argIndex)
        {
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            return new Argument(Type<T>.Info, argIndex);
        }

        [NotNull]
        public static Value Value<T>([CanBeNull] T value)
        {
            return new Value(Type<T>.Info, value);
        }

        [NotNull]
        public static Value Value(
            [NotNull] Type type,
            [CanBeNull] object value)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return new Value(type.Info(), value);
        }

        [NotNull]
        public static Dependency Dependency([NotNull] Key key, Scope scope = Scope.Current)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return new Dependency(key, scope);
        }

        [NotNull]
        public static Dependency Dependency<T>(object tag = null, Scope scope = Scope.Current)
        {
            return new Dependency(new Key(typeof(T), tag), scope);
        }

        [NotNull]
        public static StaticMethod StaticMethod(
            [NotNull] Type type,
            [NotNull] MethodInfo methodInfo,
            [NotNull] params DependencyPosition[] dependenciesPosition)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (dependenciesPosition == null) throw new ArgumentNullException(nameof(dependenciesPosition));
            return new StaticMethod(type.Info(), methodInfo, Fill(methodInfo, dependenciesPosition));
        }

        [NotNull]
        public static StaticMethod StaticMethod(
            [NotNull] Type type,
            [NotNull] Type methodType,
            [NotNull] string methodName,
            [NotNull] params DependencyPosition[] dependenciesPosition)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (methodType == null) throw new ArgumentNullException(nameof(methodType));
            if (dependenciesPosition == null) throw new ArgumentNullException(nameof(dependenciesPosition));
            if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
            var typeInfo = type.Info();
            var methodInfo = FindMethod(typeInfo, methodType.Info(), methodName);
            return new StaticMethod(typeInfo, methodInfo, Fill(methodInfo, dependenciesPosition));
        }

        [NotNull]
        public static StaticMethod StaticMethod<T>(
            [NotNull] MethodInfo methodInfo,
            [NotNull] params DependencyPosition[] dependencyPositions)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            return new StaticMethod(Type<T>.Info, methodInfo, Fill(methodInfo, dependencyPositions));
        }

        [NotNull]
        public static StaticMethod StaticMethod<T, TM>(
            [NotNull] string methodName,
            [NotNull] params DependencyPosition[] dependencyPositions)
        {
            if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            var typeInfo = Type<T>.Info;
            var methodInfo = FindMethod(typeInfo, Type<TM>.Info, methodName);
            return new StaticMethod(typeInfo, methodInfo, Fill(methodInfo, dependencyPositions));
        }

        [NotNull]
        public static FactoryMethod Factory([NotNull] Type type, [NotNull] Factory<object> factory)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new FactoryMethod(type.Info(), factory);
        }

        [NotNull]
        public static FactoryMethod Factory<T>([NotNull] Factory<T> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new FactoryMethod(Type<T>.Info, factory);
        }

        [NotNull]
        public static FactoryMethod Func<T>([NotNull] Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            return Factory((key, container, args) => func());
        }

        [NotNull]
        public static FactoryMethod Func<T>([NotNull] Func<Context, T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            return Factory((key, container, args) => func(new Context(key, container, args)));
        }

        [NotNull]
        public static Autowiring Autowiring<T>([NotNull] params MethodData[] methods)
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            return Autowiring<T>(Constructor(), methods);
        }

        [NotNull]
        public static Autowiring Autowiring<T>(ConstructorData constructor, [NotNull] params MethodData[] methods)
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            var typeInfo = Type<T>.Info;
            return new Autowiring(
                typeInfo,
                types =>
                {
                    var constructorInfo = typeInfo.DeclaredConstructors.FilterMethods(constructor.DependencyPositions).Single(i => constructor.ConstructorSelector(i));
                    return new Constructor(constructorInfo, Fill(constructorInfo, constructor.DependencyPositions));
                },
                types =>
                {
                    return (
                        from method in methods
                        let methodInfo = typeInfo.DeclaredMethods.Where(i => i.Name == method.MethodName).FilterMethods(method.DependencyPositions).Single(i => method.MethodSelector(i))
                        select new Method(methodInfo, Fill(methodInfo, method.DependencyPositions))).ToArray();
                });
        }

        [NotNull]
        public static Autowiring Autowiring([NotNull] Type type, [NotNull] params MethodData[] methods)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            return Autowiring(type, Constructor(), methods);
        }

        [NotNull]
        public static Autowiring Autowiring([NotNull] Type type, ConstructorData constructor, [NotNull] params MethodData[] methods)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            var typeInfo = type.Info();
            return Autowiring(
                type,
                constructor,
                types =>
                {
                    if (typeInfo.IsGenericTypeDefinition)
                    {
                        return typeInfo.MakeGenericType(types);
                    }

                    return type;
                },
                methods);
        }

        [NotNull]
        public static Autowiring Autowiring(
            [NotNull] Type type,
            ConstructorData constructor,
            [NotNull] Func<Type[], Type> typeSelector,
            [NotNull] params MethodData[] methods)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeSelector == null) throw new ArgumentNullException(nameof(typeSelector));
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            var typeInfo = type.Info();
            return new Autowiring(
                typeInfo,
                types =>
                {
                    typeInfo = typeSelector(types).Info();
                    var constructorInfo = typeInfo.DeclaredConstructors.FilterMethods(constructor.DependencyPositions).Single(i => constructor.ConstructorSelector(i));
                    return new Constructor(constructorInfo, Fill(constructorInfo, constructor.DependencyPositions));
                },
                types =>
                {
                    typeInfo = typeSelector(types).Info();
                    return (
                        from method in methods
                        let methodInfo = typeInfo.DeclaredMethods.Where(i => i.Name == method.MethodName).FilterMethods(method.DependencyPositions).Single(i => method.MethodSelector(i))
                        select new Method(methodInfo, Fill(methodInfo, method.DependencyPositions))).ToArray();
                });
        }

        public static ConstructorData Constructor([NotNull] params DependencyPosition[] dependencyPositions)
        {
            return new ConstructorData(dependencyPositions);
        }

        public static ConstructorData Select(this ConstructorData constructorData, [NotNull] Predicate<ConstructorInfo> constructorSelector)
        {
            if (constructorSelector == null) throw new ArgumentNullException(nameof(constructorSelector));
            return new ConstructorData(constructorData, constructorSelector);
        }

        public static MethodData Method([NotNull] string methodName, [NotNull] params DependencyPosition[] dependencyPositions)
        {
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
            return new MethodData(methodName, dependencyPositions);
        }

        public static MethodData Property(string propertyName, [NotNull] params DependencyPosition[] dependencyPositions)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyName));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            return new MethodData($"set_{propertyName}", dependencyPositions);
        }

        public static MethodData Select(this MethodData methodData, [NotNull] Predicate<MethodInfo> methodSelector)
        {
            if (methodSelector == null) throw new ArgumentNullException(nameof(methodSelector));
            return new MethodData(methodData, methodSelector);
        }

        public struct ConstructorData
        {
            [NotNull] public readonly DependencyPosition[] DependencyPositions;
            [NotNull] public readonly Predicate<ConstructorInfo> ConstructorSelector;

            internal ConstructorData([NotNull] params DependencyPosition[] dependencyPositions)
            {
                DependencyPositions = dependencyPositions ?? throw new ArgumentNullException(nameof(dependencyPositions));
                ConstructorSelector = ctor => true;
            }

            public ConstructorData(ConstructorData dependencyPositions, [NotNull] Predicate<ConstructorInfo> constructorSelector)
            {
                ConstructorSelector = constructorSelector ?? throw new ArgumentNullException(nameof(constructorSelector));
                DependencyPositions = dependencyPositions.DependencyPositions;
            }
        }

        public struct MethodData
        {
            [NotNull] public readonly string MethodName;
            [NotNull] public readonly DependencyPosition[] DependencyPositions;
            [NotNull] public readonly Predicate<MethodInfo> MethodSelector;

            internal MethodData([NotNull] string methodName, [NotNull] params DependencyPosition[] dependencyPositions)
            {
                if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
                DependencyPositions = dependencyPositions ?? throw new ArgumentNullException(nameof(dependencyPositions));
                MethodName = methodName;
                MethodSelector = info => true;
            }

            public MethodData(MethodData methodData, [NotNull] Predicate<MethodInfo> methodSelector) : this()
            {
                MethodName = methodData.MethodName;
                DependencyPositions = methodData.DependencyPositions;
                MethodSelector = methodSelector ?? throw new ArgumentNullException(nameof(methodSelector));
            }
        }

        public struct DependencyPosition
        {
            [NotNull] public readonly IDependency DependencyItem;
            [NotNull] public readonly string ParameterName;
            public readonly int ParameterPosition;

            internal DependencyPosition([NotNull] IDependency dependency, [NotNull] string parameterName)
            {
                if (string.IsNullOrWhiteSpace(parameterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));
                DependencyItem = dependency ?? throw new ArgumentNullException(nameof(dependency));
                ParameterName = parameterName;
                ParameterPosition = -1;
            }

            public DependencyPosition([NotNull] IDependency dependency, int parameterPosition)
            {
                if (parameterPosition < 0) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
                DependencyItem = dependency ?? throw new ArgumentNullException(nameof(dependency));
                ParameterPosition = parameterPosition;
                ParameterName = string.Empty;
            }
        }

        [NotNull]
        private static IEnumerable<T> FilterMethods<T>(
            [NotNull][ItemNotNull] this IEnumerable<T> methods,
            [NotNull] DependencyPosition[] dependencyPositions)
            where T : MethodBase
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));

            var maxPosition = dependencyPositions.Any() ? dependencyPositions.Max(i => i.ParameterPosition) : -1;
            var parameterNames = new HashSet<string>(dependencyPositions.Select(i => i.ParameterName).Where(i => !string.IsNullOrWhiteSpace(i)));

            return (
                from method in methods
                where !method.IsPrivate
                let parameters = method.GetParameters()
                where parameters.Length > maxPosition
                where parameterNames.IsSubsetOf(parameters.Select(i => i.Name))
                let weight = GetMethodWeight(parameters, dependencyPositions) * (parameters.Length + 1) - parameters.Length
                group method by weight)
                .OrderByDescending(i => i.Key)
                .First();
        }

        [NotNull]
        private static MethodInfo FindMethod([NotNull] ITypeInfo returnType, [NotNull] ITypeInfo methodType, string methodName)
        {
            if (returnType == null) throw new ArgumentNullException(nameof(returnType));
            if (methodType == null) throw new ArgumentNullException(nameof(methodType));
            if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
            return methodType.DeclaredMethods.Single(i => i.IsStatic && i.Name == methodName && returnType.IsAssignableFrom(i.ReturnType.Info()));
        }

        [NotNull]
        private static IDependency[] Fill(
            [NotNull] MethodBase methodBaseInfo,
            [NotNull] params DependencyPosition[] dependencyPositions)
        {
            if (methodBaseInfo == null) throw new ArgumentNullException(nameof(methodBaseInfo));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            var parameters = methodBaseInfo.GetParameters();
            var dependenceArr = new IDependency[parameters.Length];
            for (var position = 0; position < parameters.Length; position++)
            {
                var parameter = parameters[position];
                if (TryFindDependency(parameter, out var dependency, dependencyPositions))
                {
                    dependenceArr[position] = dependency;
                }
                else
                {
                    dependenceArr[position] = new Dependency(new Key(parameter.ParameterType), Scope.Current);
                }
            }

            return dependenceArr;
        }

        private static bool TryFindDependency(
            [NotNull] ParameterInfo parameterInfo,
            out IDependency dependency,
            [NotNull] IEnumerable<DependencyPosition> dependencyPositions)
        {
            if (parameterInfo == null) throw new ArgumentNullException(nameof(parameterInfo));
            if (dependencyPositions == null) throw new ArgumentNullException(nameof(dependencyPositions));
            var paramTypeInfo = parameterInfo.ParameterType.Info();
            dependency = dependencyPositions
                .Where(i => i.ParameterPosition == parameterInfo.Position || i.ParameterName == parameterInfo.Name)
                .Where(i => paramTypeInfo.IsAssignableFrom(i.DependencyItem.Type.Info()))
                .Select(i => i.DependencyItem).SingleOrDefault();

            return dependency != default(IDependency);
        }

        private static int GetMethodWeight([NotNull][ItemNotNull] ParameterInfo[] paramaters, DependencyPosition[] dependencyPositions)
        {
            if (paramaters == null) throw new ArgumentNullException(nameof(paramaters));
            return 
                paramaters.Count(paramater => TryFindDependency(paramater, out var _, dependencyPositions)) 
                - paramaters.Count(paramater => !TryFindDependency(paramater, out var _, dependencyPositions));
        }

    }
}
