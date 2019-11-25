namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal sealed class DependencyEntry : IToken
    {
        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull] [ItemNotNull] internal static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression> { WellknownExpressions.ContainerParameter, WellknownExpressions.ArgsParameter };

        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] internal readonly IEnumerable<Key> Keys;
        [NotNull] internal readonly IDependency Dependency;

        [NotNull] private readonly IEnumerable<IDisposable> _resources;
        private readonly object _lockObject = new object();
        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public DependencyEntry(
            [NotNull] IContainer container,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] IEnumerable<Key> keys)
        {
            Container = container;
            Dependency = dependency;
            Lifetime = lifetime;
            Keys = keys;
            if (lifetime is IDisposable disposableLifetime)
            {
                _resources = new[] { resource, disposableLifetime };
            }
            else
            {
                _resources = new[] { resource };
            }
        }

        public IContainer Container { get; }

        public bool TryCreateResolver(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            out Delegate resolver,
            out Exception error)
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(DependencyEntry));
                }
            }

            var typeDescriptor = key.Type.Descriptor();
            var buildContext = new BuildContext(key, resolvingContainer, _resources, builders, defaultAutowiringStrategy);
            if (!Dependency.TryBuildExpression(buildContext, GetLifetime(typeDescriptor), out var expression, out error))
            {
                resolver = default(Delegate);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            resolver = ExpressionCompiler.Shared.Compile(resolverExpression);
            error = default(Exception);
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type) => GetLifetime(type.Descriptor());

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
            var lifetimeHashCode = lifetimeKey.GetHashCode();
            lock (_lockObject)
            {
                var lifetime = _lifetimes.Get(lifetimeHashCode, lifetimeKey);
                if (lifetime == null)
                {

                    lifetime = Lifetime.Create();
                    _lifetimes = _lifetimes.Set(lifetimeHashCode, lifetimeKey, lifetime);
                }

                return lifetime;
            }
        }

        public void Dispose()
        {
            lock (_lockObject)
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
            
            foreach (var lifetime in _lifetimes)
            {
                lifetime.Value.Dispose();
            }
        }

        public override string ToString() => $"{String.Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

        private struct LifetimeKey
        {
            private readonly Type[] _genericTypes;
            private readonly int _hashCode;

            public LifetimeKey(Type[] genericTypes)
            {
                _genericTypes = genericTypes;
                _hashCode = genericTypes.GetHash();
            }

            // ReSharper disable once PossibleNullReferenceException
            public override bool Equals(object obj) => Equals((LifetimeKey)obj);

            public override int GetHashCode() => _hashCode;

            private bool Equals(LifetimeKey other) => CoreExtensions.SequenceEqual(_genericTypes, other._genericTypes);
        }
    }
}
