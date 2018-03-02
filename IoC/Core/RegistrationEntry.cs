﻿namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Collections;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private readonly IResolverExpressionBuilder _resolverExpressionBuilder;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] private readonly ILifetime _lifetime;
        [NotNull] private readonly IDisposable _resource;
        [NotNull] public readonly List<Key> Keys;
        private readonly object _lockObject = new object();
        private readonly Dictionary<LifetimeKey, ILifetime> _lifetimes = new Dictionary<LifetimeKey, ILifetime>();

        public RegistrationEntry(
            [NotNull] IResolverExpressionBuilder resolverExpressionBuilder,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] List<Key> keys)
        {
            _resolverExpressionBuilder = resolverExpressionBuilder ?? throw new ArgumentNullException(nameof(resolverExpressionBuilder));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            _lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        public bool TryCreateResolver(Key key, [NotNull] IContainer container, out Delegate resolver)
        {
            var typeInfo = key.Type.Info();
            if (!_resolverExpressionBuilder.TryBuild(key, container, Dependency, GetLifetime(typeInfo), out var resolverExpression))
            {
                resolver = default(Delegate);
                return false;
            }

            resolver = ExpressionCompiler.Shared.Compile(resolverExpression);
            return true;
        }

        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type)
        {
            return GetLifetime(type.Info());
        }

        [CanBeNull]
        private ILifetime GetLifetime(ITypeInfo typeInfo)
        {
            if (!typeInfo.IsConstructedGenericType)
            {
                return _lifetime;
            }

            var lifetimeKey = new LifetimeKey(typeInfo.GenericTypeArguments);
            ILifetime lifetime;
            lock (_lockObject)
            {
                if (!_lifetimes.TryGetValue(lifetimeKey, out lifetime))
                {
                    lifetime = _lifetime?.Clone();
                    _lifetimes.Add(lifetimeKey, lifetime);
                }
            }

            return lifetime;
        }

        public void Reset()
        {
            IDisposable[] lifetimesToDispose;
            lock (_lockObject)
            {
                lifetimesToDispose = _lifetimes.Select(i => i.Value).OfType<IDisposable>().ToArray();
                _lifetimes.Clear();
            }

            foreach (var lifetime in lifetimesToDispose)
            {
                lifetime.Dispose();
            }
        }

        public void Dispose()
        {
            Reset();

            if (_lifetime is IDisposable disposableLifetime)
            {
                disposableLifetime.Dispose();
            }

            _resource.Dispose();
        }

        public override string ToString()
        {
            return $"{string.Join(", ", Keys.Select(i => i.ToString()))} as {_lifetime?.ToString() ?? Lifetime.Transient.ToString()}";
        }

        private struct LifetimeKey
        {
            private readonly Type[] _genericTypes;

            public LifetimeKey(Type[] genericTypes)
            {
                _genericTypes = genericTypes;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is LifetimeKey key && Equals(key);
            }

            public override int GetHashCode()
            {
                return _genericTypes != null ? _genericTypes.GetHash() : 0;
            }

            private bool Equals(LifetimeKey other)
            {
                return Extensions.SequenceEqual(_genericTypes, other._genericTypes);
            }
        }
    }
}
