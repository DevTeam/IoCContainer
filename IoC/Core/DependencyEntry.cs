namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal sealed class DependencyEntry : IDisposable
    {
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] public readonly ILifetime Lifetime;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] public readonly IEnumerable<Key> Keys;
        private readonly object _lockObject = new object();
        private readonly Dictionary<LifetimeKey, ILifetime> _lifetimes = new Dictionary<LifetimeKey, ILifetime>();
        private bool _disposed;

        public DependencyEntry(
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] IEnumerable<Key> keys)
        {
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            Lifetime = lifetime;
            _resources.Add(resource ?? throw new ArgumentNullException(nameof(resource)));
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
            if (lifetime is IDisposable disposableLifetime)
            {
                _resources.Add(disposableLifetime);
            }
        }

        public bool TryCreateResolver(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] ICollection<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            out Delegate resolver,
            out Exception error)
        {
            var typeDescriptor = key.Type.Descriptor();
            var buildContext = new BuildContext(key, resolvingContainer, _resources, builders, defaultAutowiringStrategy);
            if (!Dependency.TryBuildExpression(buildContext, GetLifetime(typeDescriptor), out var expression, out error))
            {
                resolver = default(Delegate);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, WellknownExpressions.ResolverParameters);
            resolver = ExpressionCompiler.Shared.Compile(resolverExpression);
            error = default(Exception);
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type)
        {
            return GetLifetime(type.Descriptor());
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        private ILifetime GetLifetime(TypeDescriptor typeDescriptor)
        {
            if (Lifetime == null)
            {
                return null;
            }
            
            if (!typeDescriptor.IsConstructedGenericType())
            {
                return Lifetime;
            }

            var lifetimeKey = new LifetimeKey(typeDescriptor.GetGenericTypeArguments());
            ILifetime lifetime;
            lock (_lockObject)
            {
                if (!_lifetimes.TryGetValue(lifetimeKey, out lifetime))
                {
                    lifetime = Lifetime.Create();
                    _lifetimes.Add(lifetimeKey, lifetime);
                }
            }

            return lifetime;
        }

        public void Dispose()
        {
            lock(_lockObject)
            {
                if (_disposed)
                {
                    return;
                }
                
                _disposed = true;
            }
            
            foreach (var resource in _resources)
            {
                resource.Dispose();
            }
            
            foreach (var lifetime in _lifetimes.Values)
            {
                lifetime.Dispose();
            }
        }

        public override string ToString()
            => $"{string.Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

        private struct LifetimeKey
        {
            private readonly Type[] _genericTypes;

            public LifetimeKey(Type[] genericTypes) => _genericTypes = genericTypes;

            public override bool Equals(object obj) => obj is LifetimeKey key && Equals(key);

            public override int GetHashCode() => _genericTypes != null ? _genericTypes.GetHash() : 0;

            private bool Equals(LifetimeKey other) => CoreExtensions.SequenceEqual(_genericTypes, other._genericTypes);
        }
    }
}
