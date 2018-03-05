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
    using Core.Collections;
    using Extensibility;
    using Features;

    using FullKey = Key;
    using ShortKey = System.Type;
    using ResolverDelegate = System.Delegate;

    /// <summary>
    /// The IoC container implementation.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}")]
    public sealed class Container: IContainer, IResourceStore, IObserver<ContainerEvent>
    {
        private const string RootName = "root:/";
        private static long _containerId;

        internal static readonly object[] EmptyArgs = new object[0];
        [NotNull] private static readonly Lazy<Container> BasicRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.BasicSet), true);
        [NotNull] private static readonly Lazy<Container> DefultRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.DefaultSet), true);
        [NotNull] private static readonly Lazy<Container> HighPerformanceRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.HighPerformanceSet), true);
        [NotNull] private readonly object _lockObject = new object();
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private readonly Dictionary<FullKey, RegistrationEntry> _registrationEntries = new Dictionary<FullKey, RegistrationEntry>();
        [NotNull] private readonly Dictionary<ShortKey, RegistrationEntry> _registrationEntriesForTagAny = new Dictionary<ShortKey, RegistrationEntry>();
        [NotNull] internal volatile Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal volatile Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        private volatile IEnumerable<Key>[] _allKeys;
        private volatile bool _hasResolver;
        private bool _isFreezed;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container(CreateContainerName(name), DefultRootContainer.Value, true);
        }

        /// <summary>
        /// Creates a high-performance root container.
        /// It requires access permissions to types/constructors/initialization methods.
        /// Also you could add the attribute <code>[assembly: InternalsVisibleTo(IoC.Features.HighPerformanceFeature.DynamicAssemblyName)]</code> for your assembly to allow use internal classes/methods/properties in a dependency injection.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container CreateHighPerformance([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container(CreateContainerName(name), HighPerformanceRootContainer.Value, true);
        }

        /// <summary>
        /// Creates a root container with basic features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container CreateBasic([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container(CreateContainerName(name), BasicRootContainer.Value, true);
        }

        /// <summary>
        /// Creates a root container with specified features.
        /// </summary>
        /// <param name="configurations">The set of features.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container Create([NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Create("", configurations);
        }

        /// <summary>
        /// Creates a root container with specified name and features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <param name="configurations">The set of features.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return new Container(CreateContainerName(name), CreateRootContainer(configurations), true);
        }

        private static Container CreateRootContainer([NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            var container = new Container(RootName);
            container.ApplyConfigurations(configurations);
            return container;
        }

        private Container([NotNull] string name = "")
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = new NullContainer();
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, bool root)
        {
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            if (!root && _parent is IResourceStore resourceStore)
            {
                resourceStore.AddResource(this);
            }

            // Subscribe to events from the parent container
            ((IResourceStore)this).AddResource(_parent.Subscribe(_eventSubject));

            // Subscribe to reset resolvers
            ((IResourceStore)this).AddResource(_eventSubject.Subscribe(this));
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        private IIssueResolver IssueResolver => this.Resolve<IIssueResolver>();

        /// <inheritdoc />
        public override string ToString()
        {
            return _name;
        }

        /// <inheritdoc />
        public bool TryRegister(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registeredKeys = new List<Key>();
            void UnregisterKeys()
            {
                lock (_lockObject)
                {
                    foreach (var key in registeredKeys)
                    {
                        if (key.Tag == Key.AnyTag)
                        {
                            TryUnregister(key, key.Type, _registrationEntriesForTagAny);
                        }
                        else
                        {
                            TryUnregister(key, key, _registrationEntries);
                        }
                    }
                }
            }

            var registrationEntry = new RegistrationEntry(
                ResolverExpressionBuilder.Shared,
                dependency,
                lifetime,
                Disposable.Create(UnregisterKeys),
                registeredKeys);

            try
            {
                lock (_lockObject)
                {
                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new Key(type, curKey.Tag) : curKey;

                        if (key.Tag == Key.AnyTag)
                        {
                            isRegistered &= TryRegister(key, key.Type, registrationEntry, _registrationEntriesForTagAny);
                        }
                        else
                        {
                            isRegistered &= TryRegister(key, key, registrationEntry, _registrationEntries);
                        }

                        if (!isRegistered)
                        {
                            break;
                        }

                        registeredKeys.Add(key);
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
                    registrationToken = registrationEntry;
                }
                else
                {
                    registrationEntry.Dispose();
                    registrationToken = default(IDisposable);
                }
            }

            return isRegistered;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            if (tag == null)
            {
                resolver = Extensions.TryGetResolver<T>(ResolversByType, type.GetHashCode(), type, container ?? this);
                if (resolver != default(Resolver<T>))
                {
                    return true;
                }
            }
            else
            {
                resolver = Extensions.TryGetResolver<T>(ResolversByType, Resolvers, type, tag, container ?? this);
                if (resolver != default(Resolver<T>))
                {
                    return true;
                }
            }

            return TryCreateResolver(new Key(type, tag), out resolver, container ?? this);
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = Extensions.TryGetResolver<T>(ResolversByType, type.GetHashCode(), type, container ?? this);
            if (resolver != default(Resolver<T>))
            {
                return true;
            }

            return TryCreateResolver(new Key(type), out resolver, container ?? this);
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryCreateResolver<T>(FullKey key, out Resolver<T> resolver, IContainer container)
        {
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                if (!registrationEntry.TryCreateResolver(key, container, out var resolverDelegate))
                {
                    resolver = default(Resolver<T>);
                    return false;
                }

                resolver = AddResolver<T>(key, resolverDelegate, true);
                return true;
            }

            if (!_parent.TryGetResolver(key.Type, key.Tag, out resolver, container))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            if (container == this)
            {
                resolver = AddResolver<T>(key, resolver, false);
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Resolver<T> AddResolver<T>(FullKey key, ResolverDelegate resolverDelegate, bool currentContainer)
        {
            lock (_lockObject)
            {
                if (currentContainer)
                {
                    _hasResolver = true;
                }

                var resolver = (Resolver<T>)resolverDelegate;
                if (typeof(T) == key.Type)
                {
                    resolverDelegate = resolver;
                }

                Resolvers = Resolvers.Set(key.GetHashCode(), key, resolverDelegate);
                if (key.Tag == null)
                {
                    ResolversByType = ResolversByType.Set(key.Type.GetHashCode(), key.Type, resolverDelegate);
                }

                return resolver;
            }
        }

        /// <inheritdoc />
        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                dependency = registrationEntry.Dependency;
                lifetime = registrationEntry.GetLifetime(key.Type);
                return true;
            }

            return _parent.TryGetDependency(key, out dependency, out lifetime);
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryGetRegistrationEntry(FullKey key, out RegistrationEntry registrationEntry)
        {
            lock (_lockObject)
            {
                if (_registrationEntries.TryGetValue(key, out registrationEntry))
                {
                    return true;
                }

                var type = key.Type;
                var typeInfo = type.Info();
                if (typeInfo.IsConstructedGenericType)
                {
                    var genericType = typeInfo.GetGenericTypeDefinition();
                    var genericKey = new FullKey(genericType, key.Tag);
                    if (_registrationEntries.TryGetValue(genericKey, out registrationEntry))
                    {
                        return true;
                    }

                    if (_registrationEntriesForTagAny.TryGetValue(genericType, out registrationEntry))
                    {
                        return true;
                    }
                }

                if (_registrationEntriesForTagAny.TryGetValue(type, out registrationEntry))
                {
                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_parent is IResourceStore resourceStore)
            {
                resourceStore.RemoveResource(this);
            }

            IDisposable resource;
            lock (_lockObject)
            {
                _registrationEntries.Clear();
                _registrationEntriesForTagAny.Clear();
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
                resource = Disposable.Create(_resources);
                _resources.Clear();
            }

            resource.Dispose();
        }

        void IResourceStore.AddResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Add(resource);
            }
        }

        void IResourceStore.RemoveResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<IEnumerable<Key>> GetEnumerator()
        {
            return GetAllKeys().Concat(_parent).GetEnumerator();
        }

        private IEnumerable<IEnumerable<Key>> GetAllKeys()
        {
            lock (_lockObject)
            {
                return _allKeys ?? (_allKeys = _registrationEntries.Select(i => i.Value).Distinct().Select(i => (IEnumerable<Key>) i.Keys).ToArray());
            }
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return _eventSubject.Subscribe(observer);
        }

        private bool TryRegister<TKey>(Key originalKey, TKey key, [NotNull] RegistrationEntry registrationEntry, [NotNull] Dictionary<TKey, RegistrationEntry> entries)
        {
            var isRegistered = false;
            try
            {
                entries.Add(key, registrationEntry);
                ResetResolvers();
                _allKeys = null;
                isRegistered = true;
            }
            catch (ArgumentException)
            {
            }

            if (!isRegistered)
            {
                return false;
            }

            _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Registration, originalKey));
            return true;
        }

        private bool TryUnregister<TKey>(Key originalKey, TKey key, [NotNull] Dictionary<TKey, RegistrationEntry> entries)
        {
            var isUnregistered = entries.Remove(key);
            if (isUnregistered)
            {
                ResetResolvers();
                _allKeys = null;
            }

            if (!isUnregistered)
            {
                return false;
            }

            _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Unregistration, originalKey));
            return true;
        }

        private void ResetResolvers()
        {
            if (_isFreezed || !_hasResolver)
            {
                return;
            }

            _hasResolver = false;
            foreach (var registration in _registrationEntries.Select(i => i.Value))
            {
                registration.Reset();
            }

            Resolvers = Table<FullKey, ResolverDelegate>.Empty;
            ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        }

        void IObserver<ContainerEvent>.OnNext(ContainerEvent value)
        {
            if (value.Container == this)
            {
                return;
            }

            lock (_lockObject)
            {
                ResetResolvers();
            }
        }

        void IObserver<ContainerEvent>.OnError(Exception error)
        {
        }

        void IObserver<ContainerEvent>.OnCompleted()
        {
        }

        [NotNull]
        internal static string CreateContainerName([CanBeNull] string name = "")
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);
        }

        private void ApplyConfigurations(IEnumerable<IConfiguration> configurations)
        {
            try
            {
                _isFreezed = true;
                _resources.Add(this.Apply(configurations));
            }
            finally
            {
                _isFreezed = false;
            }
        }
    }
}
