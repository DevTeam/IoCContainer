namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Issues;

    internal sealed class FullAutowiringDependency : IDependency
    {
        [NotNull] private readonly Type _type;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly List<GenericParamsWithConstraints> _genericParamsWithConstraints;
        private readonly Type[] _registeredGenericTypeParameters;
        private readonly TypeDescriptor _registeredTypeDescriptor;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        public FullAutowiringDependency(
            [NotNull] Type type,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = null,
            [NotNull][ItemNotNull] params LambdaExpression[] statements)
        {
            if (statements == null) throw new ArgumentNullException(nameof(statements));
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _autoWiringStrategy = autoWiringStrategy;
            _statements = statements.Select(i => i.Body).ToArray();
            _registeredTypeDescriptor = type.Descriptor();
            _isComplexType = Autowiring.IsComplexType(_registeredTypeDescriptor);

            if (_registeredTypeDescriptor.IsInterface())
            {
                throw new ArgumentException($"Type \"{type}\" should not be an interface.", nameof(type));
            }

            if (_registeredTypeDescriptor.IsAbstract())
            {
                throw new ArgumentException($"Type \"{type}\" should not be an abstract class.", nameof(type));
            }

            if (!_registeredTypeDescriptor.IsGenericTypeDefinition())
            {
                return;
            }

            _registeredGenericTypeParameters = _registeredTypeDescriptor.GetGenericTypeParameters();
            if (_registeredGenericTypeParameters.Length > GenericTypeArguments.Arguments.Length)
            {
                throw new ArgumentException($"Too many generic type parameters in the type \"{type}\".", nameof(type));
            }

            _genericParamsWithConstraints = new List<GenericParamsWithConstraints>(_registeredGenericTypeParameters.Length);
            var genericTypePos = 0;
            var typesMap = new Dictionary<Type, Type>();
            for (var position = 0; position < _registeredGenericTypeParameters.Length; position++)
            {
                var genericType = _registeredGenericTypeParameters[position];
                if (!genericType.IsGenericParameter)
                {
                    continue;
                }

                var descriptor = genericType.Descriptor();
                if (descriptor.GetGenericParameterAttributes() == GenericParameterAttributes.None && !descriptor.GetGenericParameterConstraints().Any())
                {
                    if (!typesMap.TryGetValue(genericType, out var curType))
                    {
                        try
                        {
                            curType = GenericTypeArguments.Arguments[genericTypePos++];
                            typesMap[genericType] = curType;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            throw new BuildExpressionException("Too many generic arguments.", ex);
                        }
                    }

                    _registeredGenericTypeParameters[position] = curType;
                }
                else
                {
                    _genericParamsWithConstraints.Add(new GenericParamsWithConstraints(descriptor, position));
                }
            }

            if (_genericParamsWithConstraints.Count == 0)
            {
                _type = _registeredTypeDescriptor.MakeGenericType(_registeredGenericTypeParameters);
            }
            else
            {
                _hasGenericParamsWithConstraints = true;
            }
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var autoWiringStrategy = _autoWiringStrategy ?? buildContext.AutowiringStrategy;
                var isDefaultAutoWiringStrategy = DefaultAutowiringStrategy.Shared == autoWiringStrategy;
                if (!autoWiringStrategy.TryResolveType(_type, buildContext.Key.Type, out var instanceType))
                {
                    instanceType = _hasGenericParamsWithConstraints
                        ? GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<ICannotResolveType>().Resolve(buildContext, _type, buildContext.Key.Type)
                        : _type;
                }

                var typeDescriptor = instanceType.Descriptor();
                if (typeDescriptor.IsConstructedGenericType())
                {
                    buildContext.BindTypes(instanceType, buildContext.Key.Type);
                    var genericTypeArgs = typeDescriptor.GetGenericTypeArguments();
                    var isReplaced = false;
                    for (var position = 0; position < genericTypeArgs.Length; position++)
                    {
                        var genericTypeArg = genericTypeArgs[position];
                        var genericTypeArgDescriptor = genericTypeArg.Descriptor();
                        if (genericTypeArgDescriptor.IsGenericTypeDefinition() || genericTypeArgDescriptor.IsGenericTypeArgument())
                        {
                            if (buildContext.TryReplaceType(genericTypeArg, out var type))
                            {
                                genericTypeArgs[position] = type;
                                isReplaced = true;
                            }
                            else
                            {
                                genericTypeArgs[position] = buildContext.Container.Resolve<ICannotResolveGenericTypeArgument>().Resolve(buildContext, _registeredTypeDescriptor.Type, position, genericTypeArg);
                                isReplaced = true;
                            }
                        }
                    }

                    if (isReplaced)
                    {
                        typeDescriptor = typeDescriptor.GetGenericTypeDefinition().MakeGenericType(genericTypeArgs).Descriptor();
                    }
                }

                var defaultConstructors = Autowiring.GetMethods(typeDescriptor.GetDeclaredConstructors());
                if (!autoWiringStrategy.TryResolveConstructor(defaultConstructors, out var ctor))
                {
                    if (isDefaultAutoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveConstructor(defaultConstructors, out ctor))
                    {
                        ctor = buildContext.Container.Resolve<ICannotResolveConstructor>().Resolve(buildContext, defaultConstructors);
                    }
                }

                expression = Autowiring.ApplyInitializers(
                    buildContext,
                    autoWiringStrategy,
                    typeDescriptor,
                    _isComplexType,
                    Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext)),
                    _statements);

                expression = buildContext.AddLifetime(expression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }
        }

        [CanBeNull]
        internal Type GetInstanceTypeBasedOnTargetGenericConstrains(Type targetType)
        {
            var registeredGenericTypeParameters = new Type[_registeredGenericTypeParameters.Length];
            Array.Copy(_registeredGenericTypeParameters, registeredGenericTypeParameters, _registeredGenericTypeParameters.Length);
            var resolvingTypeDescriptor = targetType.Descriptor();
            var resolvingTypeDefinitionDescriptor = resolvingTypeDescriptor.GetGenericTypeDefinition().Descriptor();
            var resolvingTypeDefinitionGenericTypeParameters = resolvingTypeDefinitionDescriptor.GetGenericTypeParameters();
            var constraintsMap = resolvingTypeDescriptor
                .GetGenericTypeArguments()
                .Zip(resolvingTypeDefinitionGenericTypeParameters, (type, typeDefinition) => Tuple.Create(type, typeDefinition.Descriptor().GetGenericParameterConstraints()))
                .ToArray();

            var canBeResolved = true;
            foreach (var item in _genericParamsWithConstraints)
            {
                var constraints = item.TypeDescriptor.GetGenericParameterConstraints();

                var isDefined = false;
                foreach (var constraintsEntry in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraintsEntry.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[item.Position] = constraintsEntry.Item1;
                    isDefined = true;
                    break;
                }

                if (!isDefined)
                {
                    canBeResolved = false;
                    break;
                }
            }

            return canBeResolved ? _registeredTypeDescriptor.MakeGenericType(registeredGenericTypeParameters) : null;
        }

        public override string ToString() => $"new {_type.Descriptor()}(...)";

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
