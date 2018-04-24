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
    using static Key;
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
        [NotNull] private Table<FullKey, RegistrationEntry> _registrationEntries = Table<FullKey, RegistrationEntry>.Empty;
        [NotNull] private Table<ShortKey, RegistrationEntry> _registrationEntriesForTagAny = Table<ShortKey, RegistrationEntry>.Empty;
        [NotNull] internal volatile Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal volatile Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        private IEnumerable<FullKey>[] _allKeys;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The roor container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, DefultRootContainer.Value);
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
            return Create(name, HighPerformanceRootContainer.Value);
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
            return Create(name, BasicRootContainer.Value);
        }

        [NotNull]
        private static Container Create([NotNull] string name, [NotNull] IContainer parentContainer)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            return new Container(CreateContainerName(name), parentContainer, true);
        }

        private static Container CreateRootContainer([NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            var container = new Container(RootName, NullContainer.Shared, true);
            container.ApplyConfigurations(configurations);
            return container;
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, bool root)
        {
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

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
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryRegister(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registrationEntry = new RegistrationEntry(dependency, lifetime, Disposable.Create(UnregisterKeys), keys);

            void UnregisterKeys()
            {
                lock (_lockObject)
                {
                    foreach (var curKey in keys)
                    {
                        if (curKey.Tag == AnyTag)
                        {
                            TryUnregister(curKey, curKey.Type, ref _registrationEntriesForTagAny);
                        }
                        else
                        {
                            TryUnregister(curKey, curKey, ref _registrationEntries);
                        }
                    }
                }
            }

            try
            {
                var registrationEntriesForTagAny = _registrationEntriesForTagAny;
                var registrationEntries = _registrationEntries;

                lock (_lockObject)
                {
                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new FullKey(type, curKey.Tag) : curKey;

                        if (key.Tag == AnyTag)
                        {
                            isRegistered &= TryRegister(key, key.Type, registrationEntry, ref registrationEntriesForTagAny);
                        }
                        else
                        {
                            isRegistered &= TryRegister(key, key, registrationEntry, ref registrationEntries);
                        }

                        if (!isRegistered)
                        {
                            break;
                        }
                    }

                    if (isRegistered)
                    {
                        _registrationEntriesForTagAny = registrationEntriesForTagAny;
                        _registrationEntries = registrationEntries;
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
        public bool TryGetResolver<T>(ShortKey type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            if (tag == null)
            {
                resolver = (Resolver<T>) ResolversByType.FastGet(type.GetHashCode(), type);
                if (resolver != default(Resolver<T>))
                {
                    return true;
                }
            }
            else
            {
                var key = new FullKey(type, tag);
                resolver = (Resolver<T>)Resolvers.Get(key.GetHashCode(), key);
                if (resolver != default(Resolver<T>))
                {
                    return true;
                }
            }

            return TryCreateResolver(new FullKey(type, tag), out resolver, container ?? this);
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(ShortKey type, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = (Resolver<T>) ResolversByType.FastGet(type.GetHashCode(), type);
            if (resolver != default(Resolver<T>))
            {
                return true;
            }

            return TryCreateResolver(new FullKey(type), out resolver, container ?? this);
        }

        /// <inheritdoc />
        public bool TryGetDependency(FullKey key, out IDependency dependency, out ILifetime lifetime)
        {
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                dependency = registrationEntry.Dependency;
                lifetime = registrationEntry.GetLifetime(key.Type);
                return true;
            }

            return _parent.TryGetDependency(key, out dependency, out lifetime);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_parent is IResourceStore resourceStore)
            {
                resourceStore.RemoveResource(this);
            }

            List<RegistrationEntry> entriesToDispose;
            IDisposable resource;
            lock (_lockObject)
            {
                entriesToDispose = _registrationEntries.Select(i => i.Value).Concat(_registrationEntriesForTagAny.Select(i => i.Value)).ToList();
                _registrationEntries = Table<FullKey, RegistrationEntry>.Empty;
                _registrationEntriesForTagAny = Table<ShortKey, RegistrationEntry>.Empty;
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
                resource = Disposable.Create(_resources);
                _resources.Clear();
            }

            foreach (var entry in entriesToDispose)
            {
                entry.Dispose();
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
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator()
        {
            return GetAllKeys().Concat(_parent).GetEnumerator();
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return _eventSubject.Subscribe(observer);
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

        [MethodImpl((MethodImplOptions) 256)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_lockObject)
            {
                return _allKeys ?? (_allKeys = _registrationEntries.Select(i => i.Value.Keys).Distinct().ToArray());
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private bool TryRegister<TKey>(FullKey originalKey, TKey key, [NotNull] RegistrationEntry registrationEntry, [NotNull] ref Table<TKey, RegistrationEntry> entries)
        {
            var hashCode = key.GetHashCode();
            var registered = entries.Get(hashCode, key) == default(RegistrationEntry);
            if (!registered)
            {
                return false;
            }

            try
            {
                entries = entries.Set(hashCode, key, registrationEntry);
                ResetResolvers();
                _allKeys = null;
            }
            catch (ArgumentException)
            {
            }

            _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Registration, originalKey));
            return true;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private bool TryUnregister<TKey>(FullKey originalKey, TKey key, [NotNull] ref Table<TKey, RegistrationEntry> entries)
        {
            entries = entries.Remove(key.GetHashCode(), key, out var unregistered);
            if (!unregistered)
            {
                return false;
            }

            _allKeys = null;
            _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Unregistration, originalKey));
            return true;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private void ResetResolvers()
        {
            Resolvers = Table<FullKey, ResolverDelegate>.Empty;
            ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
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
        private bool TryCreateResolver<T>(FullKey key, out Resolver<T> resolver, IContainer container)
        {
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                if (!registrationEntry.TryCreateResolver(key, container, out var resolverDelegate))
                {
                    resolver = default(Resolver<T>);
                    return false;
                }

                resolver = AddResolver(key, (Resolver<T>)resolverDelegate, true);
                return true;
            }

            if (!_parent.TryGetResolver(key.Type, key.Tag, out resolver, container))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            if (container == this)
            {
                resolver = AddResolver(key, resolver, false);
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Resolver<T> AddResolver<T>(FullKey key, [NotNull] Resolver<T> resolver, bool currentContainer)
        {
            lock (_lockObject)
            {
                Resolvers = Resolvers.Set(key.GetHashCode(), key, resolver);
                if (key.Tag == null)
                {
                    ResolversByType = ResolversByType.Set(key.Type.GetHashCode(), key.Type, resolver);
                }

                return resolver;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryGetRegistrationEntry(FullKey key, out RegistrationEntry registrationEntry)
        {
            lock (_lockObject)
            {
                registrationEntry = _registrationEntries.Get(key.GetHashCode(), key);
                if (registrationEntry != default(RegistrationEntry))
                {
                    return true;
                }

                var type = key.Type;
                var typeDescriptor = type.Descriptor();
                if (typeDescriptor.IsConstructedGenericType())
                {
                    var genericType = typeDescriptor.GetGenericTypeDefinition();
                    var genericKey = new FullKey(genericType, key.Tag);
                    registrationEntry = _registrationEntries.Get(genericKey.GetHashCode(), genericKey);
                    if (registrationEntry != default(RegistrationEntry))
                    {
                        return true;
                    }

                    registrationEntry = _registrationEntriesForTagAny.FastGet(genericType.GetHashCode(), genericType);
                    if (registrationEntry != default(RegistrationEntry))
                    {
                        return true;
                    }
                }

                registrationEntry = _registrationEntriesForTagAny.FastGet(type.GetHashCode(), type);
                if (registrationEntry != default(RegistrationEntry))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
