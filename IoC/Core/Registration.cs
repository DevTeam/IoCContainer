﻿namespace IoC.Core
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
        internal static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull]
        internal static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));

        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull] [ItemNotNull] internal static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression> { ContainerParameter, ArgsParameter };

        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] private readonly IDisposable _resource;
        [NotNull] internal readonly ICollection<Key> Keys;
        [NotNull] internal readonly IDependency Dependency;

        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public Registration(
            [NotNull] IMutableContainer container,
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

        public IMutableContainer Container { get; }

        public bool TryCreateResolver<T>(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IRegistrationTracker registrationTracker,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            out Resolver<T> resolver,
            out Exception error)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Registration));
            }

            var buildContext = new BuildContext(null, key, resolvingContainer, registrationTracker.Builders, registrationTracker.AutowiringStrategy, ArgsParameter, ContainerParameter);
            var lifetime = GetLifetime(key.Type);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            resolver = default(Resolver<T>);
            try
            {
                foreach (var compiler in registrationTracker.Compilers)
                {
                    if (compiler.TryCompileResolver(buildContext, resolverExpression, out resolver))
                    {
                        eventObserver.OnNext(ContainerEvent.ResolverCompilation(Container, Enumerable.Repeat(key, 1), Dependency, lifetime, resolverExpression ));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex;
                eventObserver.OnNext(ContainerEvent.ResolverCompilationFailed(Container, Enumerable.Repeat(key, 1), Dependency, lifetime, resolverExpression, error));
                return false;
            }

            error = default(Exception);
            return resolver != default(Resolver<T>);
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