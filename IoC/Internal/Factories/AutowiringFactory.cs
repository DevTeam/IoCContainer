namespace IoC.Internal.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class AutowiringFactory : IFactory
    {
        [NotNull] private readonly IIssueResolver _issueResolver;
        [NotNull] private readonly Type _instanceType;
        [NotNull] private readonly Has[] _dependencies;
        private readonly InstanceFactory _instanceFactory;
        private readonly Dictionary<Type, InstanceFactory> _factories;
        [CanBeNull] private Func<Type[], Type[]> _typeSelector;

        public AutowiringFactory(
            [NotNull] IIssueResolver issueResolver,
            [NotNull] Type instanceType,
            [NotNull] params Has[] dependencies)
        {
#if DEBUG
            if (issueResolver == null) throw new ArgumentNullException(nameof(issueResolver));
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
#endif
            _issueResolver = issueResolver;
            _instanceType = instanceType;
            _dependencies = dependencies;
            var typeInfo = _instanceType.AsTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                _instanceFactory = new InstanceFactory(issueResolver, _instanceType.AsTypeInfo(), dependencies);
            }
            else
            {
                _factories = new Dictionary<Type, InstanceFactory>();
            }

            _typeSelector = dependencies.Where(i => i.Type == DependencyType.Generics).Select(i => i.TypeSelector).FirstOrDefault();
        }

        public object Create(ResolvingContext context)
        {
            InstanceFactory factory;
            if (_instanceFactory != null)
            {
                factory = _instanceFactory;
            }
            else
            {
                if (!_factories.TryGetValue(context.ResolvingKey.ContractType, out factory))
                {
                    Type[] genericTypeArguments;
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (context.IsGenericResolvingType)
                    {
                        genericTypeArguments = context.ResolvingKey.ContractType.GenericTypeArguments();
                    }
                    else
                    {
                        genericTypeArguments = _issueResolver.CannotGetGenericTypeArguments(context.ResolvingKey.ContractType);
                    }

                    var genericInstanceType = _instanceType.MakeGenericType(_typeSelector?.Invoke(genericTypeArguments) ?? genericTypeArguments);
                    factory = new InstanceFactory(_issueResolver, genericInstanceType.AsTypeInfo(), _dependencies);
                    _factories.Add(context.ResolvingKey.ContractType, factory);
                }
            }

            return factory.Create(context);
        }

        public override string ToString()
        {
            return $"AutowiringFactory of \"{_instanceType.Name}\"";
        }
    }
}