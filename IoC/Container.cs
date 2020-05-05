namespace IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Core;
    using static Key;
    using FullKey = Key;
    using ShortKey = System.Type;
    using ResolverDelegate = System.Delegate;

    /// <summary>
    /// The base IoC container implementation.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}")]
    [DebuggerTypeProxy(typeof(ContainerDebugView))]
    public sealed class Container : IMutableContainer
    {
        private static long _containerId;

        [NotNull] internal Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        [NotNull] private Table<FullKey, Registration> _registrations = Table<FullKey, Registration>.Empty;
        [NotNull] private Table<ShortKey, Registration> _registrationsTagAny = Table<ShortKey, Registration>.Empty;

        private bool _isDisposed;
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private readonly RegistrationTracker _registrationTracker;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="configurations"></param>
        /// <returns>The root container.</returns>
        [PublicAPI]
        [NotNull]
        public static Container Create([NotNull] [ItemNotNull] params IConfiguration[] configurations) =>
            Create(string.Empty, configurations ?? throw new ArgumentNullException(nameof(configurations)));

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <param name="configurations"></param>
        /// <returns>The root container.</returns>
        [PublicAPI]
        [NotNull]
        public static Container Create([NotNull] string name = "", [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));

            // Create a root container
            var lockObject = new LockObject();
            var rootContainer = new Container(string.Empty, NullContainer.Shared, lockObject);
            rootContainer.Register<ILockObject>(ctx => lockObject);
            if (configurations.Length > 0)
            {
                rootContainer.ApplyConfigurations(configurations);
            }
            else
            {
                rootContainer.ApplyConfigurations(Features.DefaultFeature.Set);
            }

            // Create a target container
            var container = new Container(CreateContainerName(name), rootContainer, lockObject);
            container.RegisterResource(rootContainer);
            return container;
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, [NotNull] ILockObject lockObject)
        {
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(parent));
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _eventSubject = new Subject<ContainerEvent>(_lockObject);
            _registrationTracker = new RegistrationTracker(this);

            // Subscribe to events from the parent container
            RegisterResource(_parent.Subscribe(_eventSubject));

            // Creates a subscription to track infrastructure registrations
            RegisterResource(_eventSubject.Subscribe(_registrationTracker));

            // Register the current container in the parent container
            _parent.RegisterResource(this);

            // Notifies parent container about the child container creation
            (_parent as Container)?._eventSubject.OnNext(ContainerEvent.NewContainer(this));

            // Notifies about existing registrations in parent containers
            _eventSubject.OnNext(ContainerEvent.RegisterDependency(_parent, _parent.SelectMany(i => i)));
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        /// <inheritdoc />
        public override string ToString() => _name;

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryRegisterDependency(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IToken dependencyToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var isRegistered = true;
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                var registeredKeys = new List<FullKey>();
                var dependencyEntry = new Registration(this, dependency, lifetime, Disposable.Create(() => UnregisterKeys(registeredKeys, dependency, lifetime)), registeredKeys);
                try
                {
                    var dependenciesForTagAny = _registrationsTagAny;
                    var dependencies = _registrations;
                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new FullKey(type, curKey.Tag) : curKey;
                        if (key.Tag == AnyTag)
                        {
                            isRegistered &= !dependenciesForTagAny.TryGetByType(key.Type, out _);
                            if (isRegistered)
                            {
                                dependenciesForTagAny = dependenciesForTagAny.Set(key.Type, dependencyEntry);
                            }
                        }
                        else
                        {
                            isRegistered &= !dependencies.TryGetByKey(key, out _);
                            if (isRegistered)
                            {
                                dependencies = dependencies.Set(key, dependencyEntry);
                            }
                        }

                        if (!isRegistered)
                        {
                            break;
                        }

                        registeredKeys.Add(key);
                    }

                    if (isRegistered)
                    {
                        _registrationsTagAny = dependenciesForTagAny;
                        _registrations = dependencies;
                        _eventSubject.OnNext(ContainerEvent.RegisterDependency(this, registeredKeys, dependency, lifetime));
                    }
                }
                catch (Exception error)
                {
                    _eventSubject.OnNext(ContainerEvent.RegisterDependencyFailed(this, registeredKeys, dependency, lifetime, error));
                    isRegistered = false;
                    throw;
                }
                finally
                {
                    if (isRegistered)
                    {
                        dependencyToken = dependencyEntry;
                    }
                    else
                    {
                        dependencyEntry.Dispose();
                        dependencyToken = default(IToken);
                    }
                }
            }

            return isRegistered;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(ShortKey type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            FullKey key;
            if (tag == null)
            {
                if (ResolversByType.TryGetByType(type, out var curResolver)) // found in resolvers by type
                {
                    resolver = (Resolver<T>) curResolver;
                    error = default(Exception);
                    return true;
                }

                key = new FullKey(type);
            }
            else
            {
                key = new FullKey(type, tag);
                if (Resolvers.TryGetByKey(key, out var curResolver)) // found in resolvers
                {
                    resolver = (Resolver<T>) curResolver;
                    error = default(Exception);
                    return true;
                }
            }

            return TryGetResolver(key, out resolver, out error, resolvingContainer);
        }

        [MethodImpl((MethodImplOptions)256)]
        internal bool TryGetResolver<T>(FullKey key, out Resolver<T> resolver, out Exception error, [CanBeNull] IContainer resolvingContainer)
        {
            // tries finding in dependencies
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                var hasDependency = TryGetDependency(key, key.GetHashCode(), out var dependencyEntry);
                if (hasDependency)
                {
                    // tries creating resolver
                    resolvingContainer = resolvingContainer ?? this;
                    resolvingContainer = dependencyEntry.Lifetime?.SelectResolvingContainer(this, resolvingContainer) ?? resolvingContainer;
                    if (!dependencyEntry.TryCreateResolver(
                        key,
                        resolvingContainer,
                        _registrationTracker,
                        _eventSubject,
                        out resolver,
                        out error))
                    {
                        return false;
                    }
                }
                else
                {
                    // tries finding in parent
                    if (!_parent.TryGetResolver(key.Type, key.Tag, out resolver, out error, resolvingContainer ?? this))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }
                }

                // If it is resolving container only
                if (resolvingContainer == null || Equals(resolvingContainer, this))
                {
                    // Add resolver to tables
                    Resolvers = Resolvers.Set(key, resolver);
                    if (key.Tag == null)
                    {
                        ResolversByType = ResolversByType.Set(key.Type, resolver);
                    }
                }
            }

            error = default(Exception);
            return true;
        }

        /// <inheritdoc />
        public bool TryGetDependency(FullKey key, out IDependency dependency, out ILifetime lifetime)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                if (!TryGetDependency(key, key.GetHashCode(), out var dependencyEntry))
                {
                    return _parent.TryGetDependency(key, out dependency, out lifetime);
                }

                dependency = dependencyEntry.Dependency;
                lifetime = dependencyEntry.GetLifetime(key.Type);
                return true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            try
            {
                // Notifies parent container about the child container disposing
                (_parent as Container)?._eventSubject.OnNext(ContainerEvent.DisposeContainer(this));

                _parent.UnregisterResource(this);
                List<IDisposable> entriesToDispose;
                lock (_lockObject)
                {
                    entriesToDispose = new List<IDisposable>(_registrations.Count + _registrationsTagAny.Count + _resources.Count);
                    entriesToDispose.AddRange(_registrations.Select(i => i.Value));
                    entriesToDispose.AddRange(_registrationsTagAny.Select(i => i.Value));
                    entriesToDispose.AddRange(_resources);
                    _registrations = Table<FullKey, Registration>.Empty;
                    _registrationsTagAny = Table<ShortKey, Registration>.Empty;
                    Reset();
                    _resources.Clear();
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var index = 0; index < entriesToDispose.Count; index++)
                {
                    entriesToDispose[index].Dispose();
                }

                _eventSubject.OnCompleted();
            }
            finally
            {
                _isDisposed = true;
            }
        }

        /// <inheritdoc />
        public void RegisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                _resources.Add(resource);
            }
        }

        /// <inheritdoc />
        public void UnregisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <inheritdoc />
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator() =>
            GetAllKeys().Concat(_parent).GetEnumerator();

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                return _eventSubject.Subscribe(observer ?? throw new ArgumentNullException(nameof(observer)));
            }
        }

        internal void Reset()
        {
            lock (_lockObject)
            {
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private void CheckIsNotDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(ToString());
            }
        }

        private void UnregisterKeys(List<FullKey> registeredKeys, IDependency dependency, ILifetime lifetime)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                foreach (var curKey in registeredKeys)
                {
                    if (curKey.Tag == AnyTag)
                    {
                        TryUnregister(curKey.Type, ref _registrationsTagAny);
                    }
                    else
                    {
                        TryUnregister(curKey, ref _registrations);
                    }
                }

                _eventSubject.OnNext(ContainerEvent.UnregisterDependency(this, registeredKeys, dependency, lifetime));
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                return _registrations.Select(i => i.Value.Keys).Concat(_registrationsTagAny.Select(i => i.Value.Keys)).Distinct();
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryUnregister<TKey>(TKey key, [NotNull] ref Table<TKey, Registration> entries)
        {
            entries = entries.Remove(key, out var unregistered);
            if (!unregistered)
            {
                return false;
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        internal static string CreateContainerName([CanBeNull] string name = "") =>
            !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);

        [MethodImpl((MethodImplOptions)256)]
        private void ApplyConfigurations(params IConfiguration[] configurations) =>
            _resources.Add(this.Apply(configurations));

        private bool TryGetDependency(FullKey key, int hashCode, out Registration registration)
        {
            if (_registrations.TryGetByKey(key, out registration))
            {
                return true;
            }

            var type = key.Type;
            var typeDescriptor = type.Descriptor();

            // Generic type
            if (typeDescriptor.IsConstructedGenericType())
            {
                var genericType = typeDescriptor.GetGenericTypeDefinition();
                var genericKey = new FullKey(genericType, key.Tag);
                // For generic type
                if (_registrations.TryGetByKey(genericKey, out registration))
                {
                    return true;
                }

                // For generic type and Any tag
                if (_registrationsTagAny.TryGetByType(genericType, out registration))
                {
                    return true;
                }
            }

            // For Any tag
            if (_registrationsTagAny.TryGetByType(type, out registration))
            {
                return true;
            }

            // For array
            if (typeDescriptor.IsArray())
            {
                var arrayKey = new FullKey(typeof(IArray), key.Tag);
                // For generic type
                if (_registrations.TryGetByKey(arrayKey, out registration))
                {
                    return true;
                }

                // For generic type and Any tag
                if (_registrationsTagAny.TryGetByType(typeof(IArray), out registration))
                {
                    return true;
                }
            }

            return false;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class ContainerDebugView
        {
            private readonly Container _container;

            public ContainerDebugView([NotNull] Container container) =>
                _container = container ?? throw new ArgumentNullException(nameof(container));

            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            public FullKey[] Keys => _container.GetAllKeys().SelectMany(i => i).ToArray();

            public IContainer Parent => _container.Parent is NullContainer ? null : _container.Parent;

            public int ResolversCount => _container.Resolvers.Count + _container.ResolversByType.Count;

            public int ResourcesCount => _container._resources.Count;
        }
    }
}
