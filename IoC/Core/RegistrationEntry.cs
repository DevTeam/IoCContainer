namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private readonly IResolverExpressionBuilder _resolverExpressionBuilder;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] private readonly ILifetime _lifetime;
        [NotNull] private readonly IDisposable _resource;
        private readonly object _lockObject = new object();
        private readonly Dictionary<ResolverKey, object> _resolvers = new Dictionary<ResolverKey, object>();
        private readonly Dictionary<LifetimeKey, ILifetime> _lifetimes = new Dictionary<LifetimeKey, ILifetime>();

        public RegistrationEntry(
            [NotNull] IResolverExpressionBuilder resolverExpressionBuilder,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource)
        {
            _resolverExpressionBuilder = resolverExpressionBuilder ?? throw new ArgumentNullException(nameof(resolverExpressionBuilder));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            _lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public bool TryCreateResolver<T>([NotNull] Key key, [NotNull] IContainer container, out Resolver<T> resolver)
        {
            var typeInfo = key.Type.Info();
            var resolverKey = typeInfo.IsConstructedGenericType ? new ResolverKey(key, typeInfo.GenericTypeArguments) : new ResolverKey(key, new Type[0]);
            lock (_lockObject)
            {
                if (!_resolvers.TryGetValue(resolverKey, out var resolverObject))
                {
                    if (!_resolverExpressionBuilder.TryBuild<T>(key, container, Dependency, GetLifetime(typeInfo), out var resolverExpression))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }

                    resolver = resolverExpression.Compile();
                    _resolvers.Add(resolverKey, resolver);
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
            var lifetimeKey = typeInfo.IsConstructedGenericType ? new LifetimeKey(typeInfo.GenericTypeArguments) : new LifetimeKey(new Type[0]);
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
            lock (_lockObject)
            {
                if (_lifetimes.Any())
                {
                    Disposable.Create(_lifetimes.Values.OfType<IDisposable>().Concat(_resolvers.Values.OfType<IDisposable>())).Dispose();
                    _lifetimes.Clear();
                }

                if (_resolvers.Any())
                {
                    Disposable.Create(_resolvers.Values.OfType<IDisposable>()).Dispose();
                    _resolvers.Clear();
                }
            }
        }

        public void Dispose()
        {
            Reset();
            _resource.Dispose();
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
                return obj is ResolverKey && Equals((ResolverKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((_key != null ? _key.GetHashCode() : 0) * 397) ^ (_genericTypes != null ? _genericTypes.GetHash() : 0);
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
