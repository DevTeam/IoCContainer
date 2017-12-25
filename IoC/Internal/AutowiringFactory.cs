﻿namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Reflection;

    internal sealed class AutowiringFactory : IFactory
    {
        [NotNull] private readonly IIssueResolver _issueResolver;
        [NotNull] private readonly Type _instanceType;
        [NotNull] private readonly Has[] _dependencies;
        private readonly object _lockObject = new object();
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
            var typeInfo = _instanceType.AsTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                _instanceFactory = new InstanceFactory(issueResolver, _instanceType.AsTypeInfo(), dependencies);
            }
            else
            {
                _factories = new Dictionary<Type, IFactory>();
            }
        }

        public object Create(Context context)
        {
            return GetOrCreateFactory(context).Create(context);
        }

        private IFactory GetOrCreateFactory(Context context)
        {
            lock (_lockObject)
            {
                if (_instanceFactory != null)
                {
                    return _instanceFactory;
                }

                if (!_factories.TryGetValue(context.TargetContractType, out var factory))
                {
                    Type[] genericTypeArguments;
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (context.TargetContractType.IsConstructedGenericType())
                    {
                        genericTypeArguments = context.TargetContractType.GenericTypeArguments();
                    }
                    else
                    {
                        genericTypeArguments = _issueResolver.CannotGetGenericTypeArguments(context.TargetContractType);
                    }

                    var genericInstanceType = _instanceType.MakeGenericType(genericTypeArguments);
                    factory = new InstanceFactory(_issueResolver, genericInstanceType.AsTypeInfo(), _dependencies);
                    _factories.Add(context.TargetContractType, factory);
                }

                return factory;
            }
        }
    }
}