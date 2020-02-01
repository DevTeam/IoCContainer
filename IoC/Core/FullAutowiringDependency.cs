namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Issues;

    internal class FullAutowiringDependency: IDependency
    {
        [NotNull] private readonly Type _type;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly Dictionary<int, TypeDescriptor> _genericParamsWithConstraints = new Dictionary<int, TypeDescriptor>();
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
                if (!descriptor.GetGenericParameterConstraints().Any())
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
                    _genericParamsWithConstraints[position] = descriptor;
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
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
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
                    var genericArgs = typeDescriptor.GetGenericTypeArguments();
                    var isReplaced = false;
                    for (var position = 0; position < genericArgs.Length; position++)
                    {
                        if (buildContext.TryReplaceType(genericArgs[position], out var type))
                        {
                            genericArgs[position] = type;
                            isReplaced = true;
                        }
                    }

                    if (isReplaced)
                    {
                        typeDescriptor = typeDescriptor.GetGenericTypeDefinition().MakeGenericType(genericArgs).Descriptor();
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

                baseExpression = Autowiring.ApplyInitializers(
                    buildContext,
                    autoWiringStrategy,
                    typeDescriptor,
                    _isComplexType,
                    Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext)),
                    _statements);

                baseExpression = buildContext.AddLifetime(baseExpression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                baseExpression = default(Expression);
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
                var position = item.Key;
                var descriptor = item.Value;
                var constraints = descriptor.GetGenericParameterConstraints();

                var isDefined = false;
                foreach (var constraintsEntry in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraintsEntry.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[position] = constraintsEntry.Item1;
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
    }
}
