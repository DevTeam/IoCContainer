namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal sealed class Registration : IToken
    {
        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull]
        internal static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer));

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull]
        internal static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]));

        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull] [ItemNotNull] internal static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression> { ContainerParameter, ArgsParameter };

        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] private readonly IDisposable _resource;
        [NotNull] internal readonly ICollection<Key> Keys;
        [NotNull] private readonly IObserver<ContainerEvent> _eventObserver;
        [NotNull] internal readonly IDependency Dependency;

        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public Registration(
            [NotNull] IMutableContainer container,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] ICollection<Key> keys)
        {
            Container = container;
            _eventObserver = eventObserver;
            Dependency = dependency;
            Lifetime = lifetime;
            _resource = resource;
            Keys = keys;
        }

        public IMutableContainer Container { get; }

        [MethodImpl((MethodImplOptions)0x200)]
        public bool TryCreateResolver<T>(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IRegistrationTracker registrationTracker,
            out Resolver<T> resolver,
            out Exception error)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Registration));
            }

            var buildContext = new BuildContext(
                null,
                key,
                resolvingContainer,
                registrationTracker.Compilers,
                registrationTracker.Builders,
                registrationTracker.AutowiringStrategy,
                ArgsParameter,
                ContainerParameter,
                _eventObserver);

            var lifetime = GetLifetime(key.Type);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            if (buildContext.TryCompile(resolverExpression, out var resolverDelegate, out error))
            {
                resolver = (Resolver<T>) resolverDelegate;
                return true;
            }

            resolver = default(Resolver<T>);
            return false;
        }

        [MethodImpl((MethodImplOptions)0x200)]
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

            if (!hasLifetimes)
            {
                _lifetimes = _lifetimes.Set(lifetimeKey, Lifetime);
                return Lifetime;
            }

            var lifetime = _lifetimes.Get(lifetimeKey);
            if (lifetime != default(ILifetime))
            {
                return lifetime;
            }

            lifetime = Lifetime.Create();
            _lifetimes = _lifetimes.Set(lifetimeKey, lifetime);

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

        private struct LifetimeKey: IEquatable<LifetimeKey>
        {
            private readonly Type[] _genericTypes;
            private readonly int _hashCode;

            public LifetimeKey(Type[] genericTypes)
            {
                _genericTypes = genericTypes;
                _hashCode = genericTypes.GetHash();
            }

            public bool Equals(LifetimeKey other) =>
                CoreExtensions.SequenceEqual(_genericTypes, other._genericTypes);

            // ReSharper disable once PossibleNullReferenceException
            public override bool Equals(object obj) =>
                CoreExtensions.SequenceEqual(_genericTypes, ((LifetimeKey)obj)._genericTypes);

            public override int GetHashCode() => _hashCode;
        }
    }
}
