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
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();

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

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var autoWiringStrategy = _autoWiringStrategy ?? buildContext.AutowiringStrategy;
                var instanceType = ResolveInstanceType(buildContext, autoWiringStrategy);
                var typesMap = new Dictionary<Type, Type>(_typesMap);
                var typeDescriptor = CreateTypeDescriptor(buildContext, instanceType, typesMap);
                var ctor = SelectConstructor(buildContext, typeDescriptor, autoWiringStrategy);
                expression = buildContext.ApplyInitializers(
                    autoWiringStrategy,
                    typeDescriptor,
                    Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext)),
                    _statements,
                    lifetime,
                    typesMap);

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

        private Type ResolveInstanceType(IBuildContext buildContext, IAutowiringStrategy autoWiringStrategy)
        {
            if (autoWiringStrategy.TryResolveType(_type, buildContext.Key.Type, out var instanceType))
            {
                return instanceType;
            }

            if (_hasGenericParamsWithConstraints)
            {
                return GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<ICannotResolveType>().Resolve(buildContext, _type, buildContext.Key.Type);
            }

            return _type;
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
                        genericTypeArgs[position] = buildContext.Container.Resolve<ICannotResolveGenericTypeArgument>().Resolve(buildContext, _registeredTypeDescriptor.Type, position, genericTypeArg);
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
            var registeredGenericTypeParameters = new Type[_registeredGenericTypeParameters.Length];
            Array.Copy(_registeredGenericTypeParameters, registeredGenericTypeParameters, _registeredGenericTypeParameters.Length);
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
