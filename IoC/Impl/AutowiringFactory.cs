namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class AutowiringFactory: IFactory
    {
        [NotNull] private readonly IIssueResolver _issueResolver;
        [NotNull] private readonly Type _instanceType;
        [NotNull] private readonly Has[] _dependencies;
        private readonly IFactory _instanceFactory;
        private readonly Dictionary<Type, IFactory> _factories;

        public AutowiringFactory(
            [NotNull] IIssueResolver issueResolver,
            [NotNull] Type instanceType,
            [NotNull] params Has[] dependencies)
        {
            _issueResolver = issueResolver ?? throw new ArgumentNullException(nameof(issueResolver));
            _instanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
            _dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            var typeInfo = _instanceType.GetTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                _instanceFactory = new InstanceFactory(issueResolver, _instanceType.GetTypeInfo(), dependencies);
            }
            else
            {
                _factories = new Dictionary<Type, IFactory>();
            }
        }

        public object Create(Context context)
        {
            if (_instanceFactory != null)
            {
                return _instanceFactory.Create(context);
            }

            if (!_factories.TryGetValue(context.ContractType, out var factory))
            {
                Type[] genericTypeArguments;
                if (context.ContractType.IsConstructedGenericType)
                {
                    genericTypeArguments = context.ContractType.GenericTypeArguments;
                }
                else
                {
                    genericTypeArguments = _issueResolver.CannotGetGenericTypeArguments(context.ContractType);
                }

                var genericInstanceType = _instanceType.MakeGenericType(genericTypeArguments);
                factory = new InstanceFactory(_issueResolver, genericInstanceType.GetTypeInfo(), _dependencies);
                _factories.Add(context.ContractType, factory);
            }

            return factory.Create(context);
        }
    }
}