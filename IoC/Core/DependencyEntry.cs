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
        [NotNull] private readonly IDisposable _resource;
        [NotNull] internal readonly ICollection<Key> Keys;
        [NotNull] internal readonly IDependency Dependency;

        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public DependencyEntry(
            [NotNull] IContainer container,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] ICollection<Key> keys)
        {
            Container = container;
            Dependency = dependency;
            Lifetime = lifetime;
            _resource = resource;
            Keys = keys;
        }

        public IContainer Container { get; }

        public bool TryCreateResolver(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IRegistrationTracker registrationTracker,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            out Delegate resolver,
            out Exception error)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DependencyEntry));
            }

            var buildContext = new BuildContext(key, resolvingContainer, registrationTracker.Builders, registrationTracker.AutowiringStrategy);
            var lifetime = GetLifetime(key.Type);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Delegate);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            resolver = default(Delegate);
            try
            {
                foreach (var compiler in registrationTracker.Compilers)
                {
                    if (compiler.TryCompile(buildContext, resolverExpression, out resolver))
                    {
                        eventObserver.OnNext(new ContainerEvent(Container, EventType.ResolverCompilation) {Keys = Enumerable.Repeat(key, 1), Dependency = Dependency, Lifetime = lifetime, ResolverExpression = resolverExpression });
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                eventObserver.OnNext(new ContainerEvent(Container, EventType.ResolverCompilation) { Keys = Enumerable.Repeat(key, 1), Dependency = Dependency, Lifetime = lifetime, ResolverExpression = resolverExpression, IsSuccess = false, Error = ex });
                error = ex;
                return false;
            }

            error = default(Exception);
            return resolver != default(Delegate);
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type)
        {
            if (Lifetime == default(ILifetime))
            {
                return default(ILifetime);
            }

            var hasLifetimes = _lifetimes.Count != 0;
            if (!hasLifetimes && !Keys.Any(key => key.Type.Descriptor().IsGenericTypeDefinition()))
            {
                return Lifetime;
            }

            var lifetimeKey = new LifetimeKey(type.Descriptor().GetGenericTypeArguments());
            var lifetimeHashCode = lifetimeKey.GetHashCode();

            if (!hasLifetimes)
            {
                _lifetimes = _lifetimes.Set(lifetimeHashCode, lifetimeKey, Lifetime);
                return Lifetime;
            }

            var lifetime = _lifetimes.Get(lifetimeHashCode, lifetimeKey);
            if (lifetime != default(ILifetime))
            {
                return lifetime;
            }

            lifetime = Lifetime.Create();
            _lifetimes = _lifetimes.Set(lifetimeHashCode, lifetimeKey, lifetime);

            return lifetime;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            Lifetime?.Dispose();
            foreach (var lifetime in _lifetimes)
            {
                lifetime.Value.Dispose();
            }

            _resource.Dispose();
        }

        public override string ToString() => 
            $"{string.Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

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
