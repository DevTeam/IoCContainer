namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using Issues;

    /// <summary>
    /// Represents the autowiring dependency.
    /// </summary>
    public sealed class AutowiringDependency : IDependency
    {
        [NotNull] private readonly Type _implementationType;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly List<GenericParamsWithConstraints> _genericParamsWithConstraints;
        private readonly Type[] _genericTypeParameters;
        private readonly TypeDescriptor _typeDescriptor;
        [NotNull] [ItemNotNull] private readonly LambdaExpression[] _initializeInstanceExpressions;
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="implementationType">The autowiring implementation type.</param>
        /// <param name="initializeInstanceLambdaStatements">The statements to initialize an instance.</param>
        public AutowiringDependency(
            [NotNull] Type implementationType,
            [NotNull] [ItemNotNull] params LambdaExpression[] initializeInstanceLambdaStatements)
            :this(implementationType, null, initializeInstanceLambdaStatements)
        {
        }

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="implementationType">The autowiring implementation type.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        /// <param name="initializeInstanceLambdaStatements">The statements to initialize an instance.</param>
        public AutowiringDependency(
            [NotNull] Type implementationType,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = null,
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceLambdaStatements)
        {
            _implementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            _autowiringStrategy = autowiringStrategy;
            _initializeInstanceExpressions = initializeInstanceLambdaStatements ?? throw new ArgumentNullException(nameof(initializeInstanceLambdaStatements));
            _typeDescriptor = implementationType.Descriptor();

            if (_typeDescriptor.IsInterface())
            {
                throw new ArgumentException($"Type \"{implementationType}\" should not be an interface.", nameof(implementationType));
            }

            if (_typeDescriptor.IsAbstract())
            {
                throw new ArgumentException($"Type \"{implementationType}\" should not be an abstract class.", nameof(implementationType));
            }

            if (!_typeDescriptor.IsGenericTypeDefinition())
            {
                return;
            }

            _genericTypeParameters = _typeDescriptor.GetGenericTypeParameters();
            if (_genericTypeParameters.Length > GenericTypeArguments.Arguments.Length)
            {
                throw new ArgumentException($"Too many generic type parameters in the type \"{implementationType}\".", nameof(implementationType));
            }

            _genericParamsWithConstraints = new List<GenericParamsWithConstraints>(_genericTypeParameters.Length);
            var genericTypePos = 0;
            for (var position = 0; position < _genericTypeParameters.Length; position++)
            {
                var genericType = _genericTypeParameters[position];
                if (!genericType.IsGenericParameter)
                {
                    continue;
                }

                var descriptor = genericType.Descriptor();
                if (descriptor.GetGenericParameterAttributes() == GenericParameterAttributes.None && !descriptor.GetGenericParameterConstraints().Any())
                {
                    if (!_typesMap.TryGetValue(genericType, out var curType))
                    {
                        try
                        {
                            curType = GenericTypeArguments.Arguments[genericTypePos++];
                            _typesMap[genericType] = curType;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            throw new BuildExpressionException("Too many generic arguments.", ex);
                        }
                    }

                    _genericTypeParameters[position] = curType;
                }
                else
                {
                    _genericParamsWithConstraints.Add(new GenericParamsWithConstraints(descriptor, position));
                }
            }

            if (_genericParamsWithConstraints.Count == 0)
            {
                _implementationType = _typeDescriptor.MakeGenericType(_genericTypeParameters);
            }
            else
            {
                _hasGenericParamsWithConstraints = true;
            }
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            var typesMap = new Dictionary<Type, Type>(_typesMap);
            NewExpression newExpression;
            try
            {
                var autoWiringStrategy = _autowiringStrategy ?? buildContext.AutowiringStrategy;
                var instanceType = ResolveInstanceType(buildContext, autoWiringStrategy);
                var typeDescriptor = CreateTypeDescriptor(buildContext, instanceType, typesMap);
                var ctor = SelectConstructor(buildContext, typeDescriptor, autoWiringStrategy);
                newExpression = Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext));
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }

            return
                new BaseDependency(
                    newExpression,
                    _initializeInstanceExpressions.Select(i => i.Body),
                    typesMap,
                    _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        private Type ResolveInstanceType(IBuildContext buildContext, IAutowiringStrategy autoWiringStrategy)
        {
            if (autoWiringStrategy.TryResolveType(_implementationType, buildContext.Key.Type, out var instanceType))
            {
                return instanceType;
            }

            if (_hasGenericParamsWithConstraints)
            {
                return GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<ICannotResolveType>().Resolve(buildContext, _implementationType, buildContext.Key.Type);
            }

            return _implementationType;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IMethod<ConstructorInfo> SelectConstructor(IBuildContext buildContext, TypeDescriptor typeDescriptor, IAutowiringStrategy autoWiringStrategy)
        {
            var constructors = (IEnumerable<IMethod<ConstructorInfo>>)typeDescriptor
                .GetDeclaredConstructors()
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<ConstructorInfo>(info));

            if (autoWiringStrategy.TryResolveConstructor(constructors, out var ctor))
            {
                return ctor;
            }

            if (DefaultAutowiringStrategy.Shared != autoWiringStrategy && DefaultAutowiringStrategy.Shared.TryResolveConstructor(constructors, out ctor))
            {
                return ctor;
            }

            return buildContext.Container.Resolve<ICannotResolveConstructor>().Resolve(buildContext, constructors);
        }

        private TypeDescriptor CreateTypeDescriptor(IBuildContext buildContext, Type type, Dictionary<Type, Type> typesMap)
        {
            var typeDescriptor = type.Descriptor();
            if (!typeDescriptor.IsConstructedGenericType())
            {
                return typeDescriptor;
            }

            TypeMapper.Shared.Map(type, buildContext.Key.Type, typesMap);
            foreach (var mapping in typesMap)
            {
                buildContext.MapType(mapping.Key, mapping.Value);
            }

            var genericTypeArgs = typeDescriptor.GetGenericTypeArguments();
            var isReplaced = false;
            for (var position = 0; position < genericTypeArgs.Length; position++)
            {
                var genericTypeArg = genericTypeArgs[position];
                var genericTypeArgDescriptor = genericTypeArg.Descriptor();
                if (genericTypeArgDescriptor.IsGenericTypeDefinition() || genericTypeArgDescriptor.IsGenericTypeArgument())
                {
                    if (typesMap.TryGetValue(genericTypeArg, out var genericArgType))
                    {
                        genericTypeArgs[position] = genericArgType;
                        isReplaced = true;
                    }
                    else
                    {
                        genericTypeArgs[position] = buildContext.Container.Resolve<ICannotResolveGenericTypeArgument>().Resolve(buildContext, _typeDescriptor.Type, position, genericTypeArg);
                        isReplaced = true;
                    }
                }
            }

            if (isReplaced)
            {
                typeDescriptor = typeDescriptor.GetGenericTypeDefinition().MakeGenericType(genericTypeArgs).Descriptor();
            }

            return typeDescriptor;
        }

        [CanBeNull]
        internal Type GetInstanceTypeBasedOnTargetGenericConstrains(Type type)
        {
            var registeredGenericTypeParameters = new Type[_genericTypeParameters.Length];
            Array.Copy(_genericTypeParameters, registeredGenericTypeParameters, _genericTypeParameters.Length);
            var typeDescriptor = type.Descriptor();
            var typeDefinitionDescriptor = typeDescriptor.GetGenericTypeDefinition().Descriptor();
            var typeDefinitionGenericTypeParameters = typeDefinitionDescriptor.GetGenericTypeParameters();
            var constraintsMap = typeDescriptor
                .GetGenericTypeArguments()
                .Zip(typeDefinitionGenericTypeParameters, (genericType, typeDefinition) => Tuple.Create(genericType, typeDefinition.Descriptor().GetGenericParameterConstraints()))
                .ToArray();

            var canBeResolved = true;
            foreach (var item in _genericParamsWithConstraints)
            {
                var constraints = item.TypeDescriptor.GetGenericParameterConstraints();
                var isDefined = false;
                foreach (var constraint in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraint.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[item.Position] = constraint.Item1;
                    isDefined = true;
                    break;
                }

                if (!isDefined)
                {
                    canBeResolved = false;
                    break;
                }
            }

            return canBeResolved ? _typeDescriptor.MakeGenericType(registeredGenericTypeParameters) : null;
        }

        /// <inheritdoc />
        public override string ToString() => $"new {_implementationType.Descriptor()}(...)";

        private struct GenericParamsWithConstraints
        {
            public readonly TypeDescriptor TypeDescriptor;
            public readonly int Position;

            public GenericParamsWithConstraints(TypeDescriptor typeDescriptor, int position)
            {
                TypeDescriptor = typeDescriptor;
                Position = position;
            }
        }
    }
}
