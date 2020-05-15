namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal sealed class Registration : IToken, ICompiler
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
        [NotNull] private readonly IRegistrationTracker _registrationTracker;
        [NotNull] private readonly IObserver<ContainerEvent> _eventObserver;
        [NotNull] internal readonly IDependency Dependency;

        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public Registration(
            [NotNull] IMutableContainer container,
            [NotNull] IRegistrationTracker registrationTracker,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] ICollection<Key> keys)
        {
            Container = container;
            _registrationTracker = registrationTracker;
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
            out Resolver<T> resolver,
            out Exception error)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Registration));
            }

            var buildContext = new BuildContext(null, key, resolvingContainer, _registrationTracker.Builders, _registrationTracker.AutowiringStrategy, this, ArgsParameter, ContainerParameter);
            var lifetime = GetLifetime(key.Type);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            if (TryCompile(buildContext, resolverExpression, lifetime, out var resolverDelegate, out error))
            {
                resolver = (Resolver<T>) resolverDelegate;
                return true;
            }

            resolver = default(Resolver<T>);
            return false;
        }

        private bool TryCompile([NotNull] IBuildContext context, [NotNull] LambdaExpression expression, [CanBeNull] ILifetime lifetime, out Delegate resolver, out Exception error)
        {
            error = default(Exception);
            try
            {
                foreach (var compiler in _registrationTracker.Compilers)
                {
                    if (compiler.TryCompile(context, expression, out resolver, out error))
                    {
                        _eventObserver.OnNext(ContainerEvent.Compilation(Container, new[] { context.Key }, Dependency, lifetime, expression));
                        return true;
                    }

                    _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { context.Key }, Dependency, lifetime, expression, error));
                }
            }
            catch (Exception ex)
            {
                error = ex;
                _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { context.Key }, Dependency, lifetime, expression, ex));
            }

            resolver = default(Delegate);
            return false;
        }

        public bool TryCompile(IBuildContext context, LambdaExpression expression, out Delegate resolver, out Exception error) =>
            TryCompile(context, expression, null, out resolver, out error);

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
