namespace IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Core;
    using Core.Collections;
    using Features;

    using FullKey = Core.Collections.KeyValue<System.Type, object>;
    using ShortKey = System.Type;

    [PublicAPI]
    public sealed class Container: IContainer, IResourceStore, IObserver<ContainerEvent>
    {
        private const string RootName = "container://";
        private static long _containerId;

        [NotNull] private readonly object _lockObject = new object();
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly System.Collections.Generic.List<IDisposable> _resources = new System.Collections.Generic.List<IDisposable>();

        [NotNull] private volatile HashTable<FullKey, RegistrationEntry> _registrationEntries = HashTable<FullKey, RegistrationEntry>.Empty;
        [NotNull] private volatile HashTable<ShortKey, RegistrationEntry> _registrationEntriesForTagAny = HashTable<ShortKey, RegistrationEntry>.Empty;
        [NotNull] private volatile HashTable<FullKey, object> _resolvers = HashTable<FullKey, object>.Empty;
        [NotNull] private volatile HashTable<ShortKey, object> _resolversByType = HashTable<ShortKey, object>.Empty;

        private volatile IEnumerable<Key>[] _allKeys;
        private volatile bool _hasResolver;
        private bool _isFreezed;

        private static Container CreateRootContainer()
        {
            var container = new Container(RootName);
            container.ApplyConfigurations(
                CoreFeature.Shared,
                EnumerableFeature.Shared,
                FuncFeature.Shared,
                TaskFeature.Shared,
                TupleFeature.Shared,
                LazyFeature.Shared,
                ConfigurationFeature.Shared);
            return container;
        }

        private static Container CreatePureRootContainer()
        {
            var container = new Container(RootName);
            container.ApplyConfigurations(CoreFeature.Shared);
            return container;
        }

        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container($"{RootName}{CreateContainerName(name)}", CreateRootContainer(), true);
        }

        [NotNull]
        public static Container CreatePure([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container($"{RootName}{CreateContainerName(name)}", CreatePureRootContainer(), true);
        }

        private Container([NotNull] string name = "")
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = new NullContainer();
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, bool root)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
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

        private void ApplyConfigurations(params IConfiguration[] configurations)
        {
            if (configurations.Length == 0)
            {
                return;
            }

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

        public IContainer Parent => _parent;

        private IIssueResolver IssueResolver => GetResolver<IIssueResolver>(typeof(IIssueResolver))(this);

        public bool TryRegister(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registeredKeys = new System.Collections.Generic.List<Key>();
            void UnregisterKeys()
            {
                var registrationAnyEntries = _registrationEntriesForTagAny;
                var registrationEntries = _registrationEntries;

                foreach (var key in registeredKeys)
                {
                    if (key.Tag == Key.AnyTag)
                    {
                        TryUnregister(key, key.Type, ref registrationAnyEntries);
                    }
                    else
                    {
                        TryUnregister(key, new FullKey(key.Type, key.Tag), ref registrationEntries);
                    }
                }

                _registrationEntriesForTagAny = registrationAnyEntries;
                _registrationEntries = registrationEntries;
            }

            var registrationEntry = new RegistrationEntry(
                ResolverExpressionBuilder.Shared,
                dependency,
                lifetime,
                Disposable.Create(UnregisterKeys),
                registeredKeys);

            try
            {
                var registrationAnyEntries = _registrationEntriesForTagAny;
                var registrationEntries = _registrationEntries;

                foreach (var key in keys)
                {
                    if (key.Tag == Key.AnyTag)
                    {
                        isRegistered &= TryRegister(key, key.Type, registrationEntry, ref registrationAnyEntries);
                    }
                    else
                    {
                        isRegistered &= TryRegister(key, new FullKey(key.Type, key.Tag), registrationEntry, ref registrationEntries);
                    }

                    if (isRegistered)
                    {
                        registeredKeys.Add(key);
                    }
                    else
                    {
                        break;
                    }
                }

                if (isRegistered)
                {
                    _registrationEntriesForTagAny = registrationAnyEntries;
                    _registrationEntries = registrationEntries;
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

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            if (tag == null && _resolversByType.Count > 0)
            {
                resolver = (Resolver<T>)_resolversByType.Get(type);
                if (resolver != null)
                {
                    return true;
                }
            }

            resolver = (Resolver<T>)_resolvers.Get(new FullKey(type, tag));
            if (resolver != null)
            {
                return true;
            }

            return TryCreateResolver(type, tag, out resolver, container ?? this);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = (Resolver<T>) _resolversByType.Get(type);
            if (resolver != null)
            {
                return true;
            }

            return TryCreateResolver(type, null, out resolver, container ?? this);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Resolver<T> GetResolver<T>(Type type, object tag, IContainer container = null)
        {
            if (!TryGetResolver<T>(type, tag, out var resolver, container))
            {
                return IssueResolver.CannotGetResolver<T>(this, new Key(type, tag));
            }

            return resolver;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Resolver<T> GetResolver<T>(Type type, IContainer container = null)
        {
            if (!TryGetResolver<T>(type, out var resolver, container))
            {
                return IssueResolver.CannotGetResolver<T>(this, new Key(type));
            }

            return resolver;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool TryCreateResolver<T>(Type type, [CanBeNull] object tag, out Resolver<T> resolver, IContainer container)
        {
            if (TryGetRegistrationEntry(type, tag, out var registrationEntry))
            {
                if (!registrationEntry.TryCreateResolver(type, tag, container, out resolver))
                {
                    return false;
                }

                AddResolver(type, tag, resolver);
                return true;
            }

            if (!_parent.TryGetResolver(type, tag, out resolver, container))
            {
                return false;
            }

            AddResolver(type, tag, resolver);
            return true;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void AddResolver<T>(Type type, object tag, Resolver<T> resolver)
        {
            lock (_lockObject)
            {
                _hasResolver = true;
                _resolvers = _resolvers.Add(new FullKey(type, tag), resolver);
                if (tag == null)
                {
                    _resolversByType = _resolversByType.Add(type, resolver);
                }
            }
        }

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            if (TryGetRegistrationEntry(key.Type, key.Tag, out var registrationEntry))
            {
                dependency = registrationEntry.Dependency;
                lifetime = registrationEntry.GetLifetime(key.Type);
                return true;
            }

            return _parent.TryGetDependency(key, out dependency, out lifetime);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool TryGetRegistrationEntry(Type type, object tag, out RegistrationEntry registrationEntry)
        {
            lock (_lockObject)
            {
                registrationEntry = _registrationEntries.Get(new FullKey(type, tag));
                if (registrationEntry != null)
                {
                    return true;
                }

                var typeInfo = type.Info();
                if (typeInfo.IsConstructedGenericType)
                {
                    var genericType = typeInfo.GetGenericTypeDefinition();
                    registrationEntry = _registrationEntries.Get(new FullKey(genericType, tag));
                    if (registrationEntry != null)
                    {
                        return true;
                    }

                    registrationEntry = _registrationEntriesForTagAny.Get(genericType);
                    if (registrationEntry != null)
                    {
                        return true;
                    }
                }

                registrationEntry = _registrationEntriesForTagAny.Get(type);
                if (registrationEntry != null)
                {
                    return true;
                }

                return false;
            }
        }

        public void Dispose()
        {
            if (_parent is IResourceStore resourceStore)
            {
                resourceStore.RemoveResource(this);
            }

            IDisposable resource;
            lock (_lockObject)
            {
                _registrationEntries = HashTable<FullKey, RegistrationEntry>.Empty;
                _registrationEntriesForTagAny = HashTable<ShortKey, RegistrationEntry>.Empty;
                _resolvers = HashTable<FullKey, object>.Empty;
                _resolversByType = HashTable<ShortKey, object>.Empty;
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

        public IEnumerator<IEnumerable<Key>> GetEnumerator()
        {
            lock (_lockObject)
            {
                if (_allKeys == null)
                {
                    _allKeys = _registrationEntries.Enumerate().Select(i => i.Value).Distinct().Select(i => (IEnumerable<Key>) i.Keys).ToArray();
                }

                return _allKeys.Concat(_parent).GetEnumerator();
            }
        }

        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return _eventSubject.Subscribe(observer);
        }

        private bool TryRegister<TKey>(Key originalKey, TKey key, [NotNull] RegistrationEntry registrationEntry, [NotNull] ref HashTable<TKey, RegistrationEntry> entries)
        {
            bool added;
            lock (_lockObject)
            {
                added = entries.Get(key) == null;
                if (added)
                {
                    entries = entries.Add(key, registrationEntry);
                    ResetResolvers();
                    _allKeys = null;
                }
            }

            if (added)
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Registration, originalKey));
                return true;
            }

            return false;
        }

        private bool TryUnregister<TKey>(Key originalKey, TKey key, [NotNull] ref HashTable<TKey, RegistrationEntry> entries)
        {
            bool removed;
            lock (_lockObject)
            {
                entries = entries.Remove(key, out removed);
                if (removed)
                {
                    ResetResolvers();
                    _allKeys = null;
                }
            }

            if (removed)
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Unregistration, originalKey));
            }

            return false;
        }

        private void ResetResolvers()
        {
            if (_isFreezed || !_hasResolver)
            {
                return;
            }

            _hasResolver = false;
            foreach (var registration in _registrationEntries.Enumerate().Select(i => i.Value))
            {
                registration.Reset();
            }

            _resolvers = HashTable<FullKey, object>.Empty;
            _resolversByType = HashTable<ShortKey, object>.Empty;
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
    }
}
