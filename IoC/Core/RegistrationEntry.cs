namespace IoC.Core
{
    using System;
    using System.Linq;
    using Collections;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private readonly IResolverExpressionBuilder _resolverExpressionBuilder;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] private readonly ILifetime _lifetime;
        [NotNull] private readonly IDisposable _resource;
        [NotNull] public readonly System.Collections.Generic.List<Key> Keys;
        private readonly object _lockObject = new object();
        private HashTable<ResolverKey, object> _resolvers = HashTable<ResolverKey, object>.Empty;
        private HashTable<LifetimeKey, ILifetime> _lifetimes = HashTable<LifetimeKey, ILifetime>.Empty;

        public RegistrationEntry(
            [NotNull] IResolverExpressionBuilder resolverExpressionBuilder,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] System.Collections.Generic.List<Key> keys)
        {
            _resolverExpressionBuilder = resolverExpressionBuilder ?? throw new ArgumentNullException(nameof(resolverExpressionBuilder));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            _lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        public bool TryCreateResolver<T>(Key key, [NotNull] IContainer container, out Resolver<T> resolver)
        {
            var typeInfo = key.Type.Info();
            var resolverKey = typeInfo.IsConstructedGenericType ? new ResolverKey(key, typeInfo.GenericTypeArguments) : new ResolverKey(key, new Type[0]);
            lock (_lockObject)
            {
                var resolverObject = _resolvers.Get(resolverKey);
                if (resolverObject == null)
                {
                    if (!_resolverExpressionBuilder.TryBuild<T>(key, container, Dependency, GetLifetime(typeInfo), out var resolverExpression))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }

                    resolver = resolverExpression.Compile();
                    _resolvers = _resolvers.Add(resolverKey, resolver);
                }
                else
                {
                    resolver = (Resolver<T>)resolverObject;
                }
            }

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
                lifetime = _lifetimes.Get(lifetimeKey);
                if (lifetime == null)
                {
                    lifetime = _lifetime?.Clone();
                    _lifetimes = _lifetimes.Add(lifetimeKey, lifetime);
                }
            }

            return lifetime;
        }

        public void Reset()
        {
            IDisposable[] lifetimesToDispose;
            lock (_lockObject)
            {
                _resolvers = HashTable<ResolverKey, object>.Empty;
                if (_lifetimes.Count > 0)
                {
                    lifetimesToDispose =  _lifetimes.Enumerate().Select(i => i.Value).OfType<IDisposable>().ToArray();
                    _lifetimes = HashTable<LifetimeKey, ILifetime>.Empty;
                }
                else
                {
                    return;
                }
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

        private struct ResolverKey
        {
            private readonly Key _key;
            private readonly Type[] _genericTypes;

            public ResolverKey(Key key, Type [] genericTypes)
            {
                _key = key;
                _genericTypes = genericTypes;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is ResolverKey key && Equals(key);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (_key.GetHashCode() * 397) ^ (_genericTypes != null ? _genericTypes.GetHash() : 0);
                }
            }

            private bool Equals(ResolverKey other)
            {
                return Equals(_key, other._key) && Arrays.SequenceEqual(_genericTypes, other._genericTypes);
            }
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
                return Arrays.SequenceEqual(_genericTypes, other._genericTypes);
            }
        }
    }
}
