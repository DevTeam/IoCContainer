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

        public bool TryCreateResolver<T>(Type type, [CanBeNull] object tag, [NotNull] IContainer container, out Resolver<T> resolver)
        {
            var typeInfo = type.Info();
            var resolverKey = typeInfo.IsConstructedGenericType ? new ResolverKey(type, typeInfo.GenericTypeArguments) : new ResolverKey(type, new Type[0]);
            lock (_lockObject)
            {
                var resolverObject = _resolvers.Get(resolverKey);
                if (resolverObject == null)
                {
                    if (!_resolverExpressionBuilder.TryBuild<T>(new Key(type, tag), container, Dependency, GetLifetime(typeInfo), out var resolverExpression))
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
                if (_lifetimes.Count == 0)
                {
                    return;
                }

                lifetimesToDispose = _lifetimes.Enumerate().Select(i => i.Value).OfType<IDisposable>().ToArray();
                _lifetimes = HashTable<LifetimeKey, ILifetime>.Empty;
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
            private readonly Type _type;
            private readonly Type[] _genericTypes;

            public ResolverKey(Type type, Type [] genericTypes)
            {
                _type = type;
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
                    return (_type.GetHashCode() * 397) ^ (_genericTypes != null ? _genericTypes.GetHash() : 0);
                }
            }

            private bool Equals(ResolverKey other)
            {
                return _type == other._type && ArrayExtensions.SequenceEqual(_genericTypes, other._genericTypes);
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
                return ArrayExtensions.SequenceEqual(_genericTypes, other._genericTypes);
            }
        }
    }
}
