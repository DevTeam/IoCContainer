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
    using Features;
    using static Key;
    using FullKey = Key;
    using ShortKey = System.Type;
    using ResolverDelegate = System.Delegate;

    /// <summary>
    /// The IoC container implementation.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}")]
    [DebuggerTypeProxy(typeof(ContainerDebugView))]
    public sealed class Container: IContainer
    {
        private static long _containerId;
        [NotNull] private static readonly Lazy<Container> CoreRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.CoreSet), true);
        [NotNull] private static readonly Lazy<Container> DefaultRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.DefaultSet), true);
        [NotNull] private static readonly Lazy<Container> LightRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.LightSet), true);

        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private Table<FullKey, DependencyEntry> _dependencies = Table<FullKey, DependencyEntry>.Empty;
        [NotNull] private Table<ShortKey, DependencyEntry> _dependenciesForTagAny = Table<ShortKey, DependencyEntry>.Empty;
        [NotNull] internal volatile Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal volatile Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        private readonly RegistrationTracker _registrationTracker;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, DefaultRootContainer.Value);
        }

        /// <summary>
        /// Creates a root container with minimal set of features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateCore([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, CoreRootContainer.Value);
        }

        /// <summary>
        /// Creates a root container with minimalist default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateLight([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, LightRootContainer.Value);
        }

        [NotNull]
        private static Container Create([NotNull] string name, [NotNull] IContainer parentContainer)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            return new Container(CreateContainerName(name), parentContainer);
        }

        private static Container CreateRootContainer([NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            var container = new Container(string.Empty, NullContainer.Shared);
            container.ApplyConfigurations(configurations);
            return container;
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent)
        {
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _registrationTracker = new RegistrationTracker(this);

            // Subscribe to events from the parent container
            RegisterResource(_parent.Subscribe(_eventSubject));

            // Creates a subscription to track infrastructure registrations
            RegisterResource(_eventSubject.Subscribe(_registrationTracker));

            // Register the current container in the parent container
            _parent.RegisterResource(this);

            // Notifies about existing registrations in parent containers
            _eventSubject.OnNext(new ContainerEvent(_parent, EventType.DependencyRegistration, _parent.SelectMany(i => i)));
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        /// <inheritdoc />
        public override string ToString()
        {
            return _name;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryRegisterDependency(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IDisposable dependencyToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registeredKeys = new List<FullKey>();
            var dependencyEntry = new DependencyEntry(dependency, lifetime, Disposable.Create(UnregisterKeys), registeredKeys);

            void UnregisterKeys()
            {
                lock (_eventSubject)
                {
                    foreach (var curKey in registeredKeys)
                    {
                        if (curKey.Tag == AnyTag)
                        {
                            TryUnregister(curKey.Type, ref _dependenciesForTagAny);
                        }
                        else
                        {
                            TryUnregister(curKey, ref _dependencies);
                        }
                    }

                    _eventSubject.OnNext(new ContainerEvent(this, EventType.DependencyUnregistration, registeredKeys));
                }
            }

            try
            {
                var dependenciesForTagAny = _dependenciesForTagAny;
                var dependencies = _dependencies;

                lock (_eventSubject)
                {
                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new FullKey(type, curKey.Tag) : curKey;

                        if (key.Tag == AnyTag)
                        {
                            var hashCode = key.Type.GetHashCode();
                            isRegistered &= dependenciesForTagAny.GetByRef(hashCode, key.Type) == default(DependencyEntry);
                            if (isRegistered)
                            {
                                dependenciesForTagAny = dependenciesForTagAny.Set(hashCode, key.Type, dependencyEntry);
                            }
                        }
                        else
                        {
                            var hashCode = key.GetHashCode();
                            isRegistered &= dependencies.Get(hashCode, key) == default(DependencyEntry);
                            if (isRegistered)
                            {
                                dependencies = dependencies.Set(hashCode, key, dependencyEntry);
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
                        _dependenciesForTagAny = dependenciesForTagAny;
                        _dependencies = dependencies;
                        _eventSubject.OnNext(new ContainerEvent(this, EventType.DependencyRegistration, registeredKeys));
                    }
                }
            }
            catch (Exception)
            {
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
                    dependencyToken = default(IDisposable);
                }
            }

            return isRegistered;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(ShortKey type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            FullKey key;
            int hashCode;
            if (tag == null)
            {
                hashCode = type.GetHashCode();
                resolver = (Resolver<T>)ResolversByType.GetByRef(hashCode, type);
                if (resolver != default(Resolver<T>)) // found in resolvers by type
                {
                    error = default(Exception);
                    return true;
                }

                key = new FullKey(type);
            }
            else
            {
                key = new FullKey(type, tag);
                hashCode = key.GetHashCode();
                resolver = (Resolver<T>)Resolvers.Get(hashCode, key);
                if (resolver != default(Resolver<T>)) // found in resolvers
                {
                    error = default(Exception);
                    return true;
                }
            }

            // tries finding in dependencies
            bool hasDependency;
            lock (_eventSubject)
            {
                hasDependency = TryGetDependency(key, hashCode, out var dependencyEntry);
                if (hasDependency)
                {
                    // tries creating resolver
                    resolvingContainer = resolvingContainer ?? this;
                    resolvingContainer = dependencyEntry.Lifetime?.SelectResolvingContainer(this, resolvingContainer) ?? resolvingContainer;
                    if (!dependencyEntry.TryCreateResolver(key, resolvingContainer, _registrationTracker.Builders, _registrationTracker.AutowiringStrategy, out var resolverDelegate, out error))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }

                    resolver = (Resolver<T>) resolverDelegate;
                }
            }

            if (!hasDependency)
            {
                // tries finding in parent
                if (!_parent.TryGetResolver(type, tag, out resolver, out error, resolvingContainer ?? this))
                {
                    resolver = default(Resolver<T>);
                    return false;
                }
            }

            // If it is resolving container only
            if (resolvingContainer == null || resolvingContainer == this)
            {
                // Add resolver to tables
                lock (_eventSubject)
                {
                    Resolvers = Resolvers.Set(hashCode, key, resolver);
                    if (tag == null)
                    {
                        ResolversByType = ResolversByType.Set(hashCode, type, resolver);
                    }
                }
            }

            error = default(Exception);
            return true;
        }

        /// <inheritdoc />
        public bool TryGetDependency(FullKey key, out IDependency dependency, out ILifetime lifetime)
        {
            if (!TryGetDependency(key, key.GetHashCode(), out var dependencyEntry))
            {
                return _parent.TryGetDependency(key, out dependency, out lifetime);
            }

            dependency = dependencyEntry.Dependency;
            lifetime = dependencyEntry.GetLifetime(key.Type);
            return true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _parent.UnregisterResource(this);
            var entriesToDispose = new List<IDisposable>(_dependencies.Count + _dependenciesForTagAny.Count + _resources.Count);
            lock (_eventSubject)
            {
                entriesToDispose.AddRange(_dependencies.Select(i => i.Value));
                entriesToDispose.AddRange(_dependenciesForTagAny.Select(i => i.Value));
                entriesToDispose.AddRange(_resources);
                _dependencies = Table<FullKey, DependencyEntry>.Empty;
                _dependenciesForTagAny = Table<ShortKey, DependencyEntry>.Empty;
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

        /// <inheritdoc />
        public void RegisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_eventSubject)
            {
                _resources.Add(resource);
            }
        }

        /// <inheritdoc />
        public void UnregisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_eventSubject)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator()
        {
            return GetAllKeys().Concat(_parent).GetEnumerator();
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            return _eventSubject.Subscribe(observer);
        }
        
        internal void Reset()
        {
            lock (_eventSubject)
            {
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_eventSubject)
            {
                return _dependencies.Select(i => i.Value.Keys).Concat(_dependenciesForTagAny.Select(i => i.Value.Keys)).Distinct();
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private bool TryUnregister<TKey>(TKey key, [NotNull] ref Table<TKey, DependencyEntry> entries)
        {
            entries = entries.Remove(key.GetHashCode(), key, out var unregistered);
            if (!unregistered)
            {
                return false;
            }

            return true;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        internal static string CreateContainerName([CanBeNull] string name = "")
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);
        }

        [MethodImpl((MethodImplOptions) 256)]
        private void ApplyConfigurations(IEnumerable<IConfiguration> configurations)
        {
            _resources.Add(this.Apply(configurations));
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryGetDependency(FullKey key, int hashCode, out DependencyEntry dependencyEntry)
        {
            lock (_eventSubject)
            {
                dependencyEntry = _dependencies.Get(hashCode, key);
                if (dependencyEntry != default(DependencyEntry))
                {
                    return true;
                }

                dependencyEntry = _dependencies.Get(hashCode, key);
                if (dependencyEntry != default(DependencyEntry))
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
                    dependencyEntry = _dependencies.Get(genericKey.GetHashCode(), genericKey);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }

                    // For generic type and Any tag
                    dependencyEntry = _dependenciesForTagAny.GetByRef(genericType.GetHashCode(), genericType);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }
                }

                // For Any tag
                dependencyEntry = _dependenciesForTagAny.GetByRef(type.GetHashCode(), type);
                if (dependencyEntry != default(DependencyEntry))
                {
                    return true;
                }

                // For array
                if (typeDescriptor.IsArray())
                {
                    var arrayType = typeof(IArray);
                    var arrayKey = new FullKey(arrayType, key.Tag);
                    // For generic type
                    dependencyEntry = _dependencies.Get(arrayKey.GetHashCode(), arrayKey);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }

                    // For generic type and Any tag
                    dependencyEntry = _dependenciesForTagAny.GetByRef(arrayType.GetHashCode(), arrayType);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class ContainerDebugView
        {
            private readonly Container _container;

            public ContainerDebugView([NotNull] Container container)
            {
                _container = container ?? throw new ArgumentNullException(nameof(container));
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            public FullKey[] Keys => _container.GetAllKeys().SelectMany(i => i).ToArray();

            public IContainer Parent => _container.Parent is NullContainer ? null : _container.Parent;

            public int ResolversCount => _container.Resolvers.Count + _container.ResolversByType.Count;

            public int ResourcesCount => _container._resources.Count;
        }
    }
}
