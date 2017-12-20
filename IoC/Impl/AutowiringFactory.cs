namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class AutowiringFactory: IFactory
    {
        private readonly Type _instanceType;
        private readonly Dependency[] _dependencies;
        private readonly IFactory _instanceFactory;
        private readonly Dictionary<Type, IFactory> _factories;

        public AutowiringFactory([NotNull] Type instanceType, [NotNull] params Dependency[] dependencies)
        {
            _instanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
            _dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            var typeInfo = _instanceType.GetTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                _instanceFactory = new InstanceFactory(_instanceType.GetTypeInfo(), dependencies);
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
                if (!context.ContractType.IsConstructedGenericType)
                {
                    throw new InvalidOperationException();
                }

                var typeInfo = context.ContractType.GetTypeInfo();
                var genericInstanceType = _instanceType.MakeGenericType(typeInfo.GenericTypeArguments);
                factory = new InstanceFactory(genericInstanceType.GetTypeInfo(), _dependencies);
                _factories.Add(context.ContractType, factory);
            }

            return factory.Create(context);
        }
    }
}