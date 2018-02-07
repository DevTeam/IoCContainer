namespace IoC.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private readonly IResolverGenerator _resolverGenerator;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] private readonly IDisposable _resource;
        private readonly ConcurrentDictionary<ResolverKey, object> _resolvers = new ConcurrentDictionary<ResolverKey, object>();
        private readonly ConcurrentDictionary<LifetimeKey, ILifetime> _lifetimes = new ConcurrentDictionary<LifetimeKey, ILifetime>();

        public RegistrationEntry(
            [NotNull] IResolverGenerator resolverGenerator,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource)
        {
            _resolverGenerator = resolverGenerator ?? throw new ArgumentNullException(nameof(resolverGenerator));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            Lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public Resolver<T> CreateResolver<T>(Key key, [NotNull] IContainer container)
        {
            var typeInfo = key.Type.Info();
            var resolverKey = typeInfo.IsConstructedGenericType ? new ResolverKey(typeof(T), typeInfo.GenericTypeArguments) : new ResolverKey(typeof(T), new Type[0]);
            var resolverHolder = (IResolverHolder<T>)_resolvers.GetOrAdd(resolverKey, i => _resolverGenerator.Generate<T>(key, container, Dependency, GetLifetime(typeInfo)));
            return resolverHolder.Resolve;
        }

        private ILifetime GetLifetime(ITypeInfo typeInfo)
        {
            var lifetimeKey = typeInfo.IsConstructedGenericType ? new LifetimeKey(typeInfo.GenericTypeArguments) : new LifetimeKey(new Type[0]);
            return _lifetimes.GetOrAdd(lifetimeKey, i => Lifetime?.Clone());
        }

        public void Reset()
        {
            Disposable.Create(_lifetimes.Values.OfType<IDisposable>()).Dispose();
            _lifetimes.Clear();
            Disposable.Create(_resolvers.Values.OfType<IDisposable>()).Dispose();
            _resolvers.Clear();
        }

        public void Dispose()
        {
            Reset();
            _resource.Dispose();
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
                return obj is ResolverKey && Equals((ResolverKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((_type != null ? _type.GetHashCode() : 0) * 397) ^ (_genericTypes != null ? _genericTypes.GetHash() : 0);
                }
            }

            private bool Equals(ResolverKey other)
            {
                return _type == other._type && Arrays.SequenceEqual(_genericTypes, other._genericTypes);
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
