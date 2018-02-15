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

    [PublicAPI]
    public sealed class Container: IContainer, IResourceStore, IObserver<ContainerEvent>
    {
        private const string RootName = "container://";
        private static long _containerId;
        [NotNull] private static readonly Lazy<Container> RootContainer = new Lazy<Container>(CreateRootContainer, true);
        [NotNull] private static readonly Lazy<Container> PureRootContainer = new Lazy<Container>(CreatePureRootContainer, true);
        [NotNull] private readonly object _lockObject = new object();
        [NotNull] private readonly Dictionary<Key, RegistrationEntry> _registrationEntries = new Dictionary<Key, RegistrationEntry>();
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly System.Collections.Generic.List<IDisposable> _resources = new System.Collections.Generic.List<IDisposable>();
        [NotNull] private volatile HashTable<Key, object> _resolversByKey = HashTable<Key, object>.Empty;
        [NotNull] private volatile HashTable<Type, object> _resolversByType = HashTable<Type, object>.Empty;
        private volatile System.Collections.Generic.List<IEnumerable<Key>> _allKeys;
        private volatile bool _hasResolver;

        private static Container CreateRootContainer()
        {
            return new Container(
                RootName,
                CoreFeature.Shared,
                EnumerableFeature.Shared,
                FuncFeature.Shared,
                TaskFeature.Shared,
                ConfigurationFeature.Shared);
        }

        private static Container CreatePureRootContainer()
        {
            return new Container(RootName, CoreFeature.Shared);
        }

        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container($"{RootName}{CreateContainerName(name)}", RootContainer.Value, true);
        }

        [NotNull]
        public static Container CreatePure([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return new Container($"{RootName}{CreateContainerName(name)}", PureRootContainer.Value, true);
        }

        private Container([NotNull] string name = "", [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = new NullContainer();
            if (configurations.Length > 0)
            {
                _resources.Add(this.Apply(configurations));
            }
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, bool root, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            // Subscribe to events from the parent container
            ((IResourceStore)this).AddResource(_parent.Subscribe(_eventSubject));

            // Subscribe to reset resolvers
            ((IResourceStore)this).AddResource(_eventSubject.Subscribe(this));

            if (!root && _parent is IResourceStore resourceStore)
            {
                resourceStore.AddResource(this);
            }
        }

        public IContainer Parent => _parent;

        private IIssueResolver IssueResolver => GetResolver<IIssueResolver>(typeof(IIssueResolver))(this);

        public bool TryRegister(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var resolvers = new System.Collections.Generic.List<IDisposable>();
            try
            {
                var registeredKeys = new System.Collections.Generic.List<Key>();

                void UnregisterKeys()
                {
                    foreach (var key in registeredKeys)
                    {
                        TryUnregister(key);
                    }
                }

                var resource = Disposable.Create(UnregisterKeys);
                var resolver = new RegistrationEntry(ResolverExpressionBuilder.Shared, dependency, lifetime, resource, registeredKeys);
                foreach (var key in keys)
                {
                    resolvers.Add(resolver);
                    isRegistered &= TryRegister(key, resolver);
                    if (isRegistered)
                    {
                        registeredKeys.Add(key);
                    }
                    else
                    {
                        break;
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
                var token = Disposable.Create(resolvers);
                if (isRegistered)
                {
                    registrationToken = token;
                    if (lifetime is IDisposable disposableLifetime)
                    {
                        _resources.Add(disposableLifetime);
                    }
                }
                else
                {
                    token.Dispose();
                    registrationToken = default(IDisposable);
                }
            }

            return isRegistered;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Resolver<T> GetResolver<T>(Type type)
        {
            if (!TryGetResolver<T>(type, out var resolver))
            {
                return IssueResolver.CannotGetResolver<T>(this, new Key(type, null));
            }

            return resolver;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, out Resolver<T> resolver)
        {
            var hashCode = type.GetHashCode();
            var typeTree = _resolversByType.Buckets[hashCode & (_resolversByType.Divisor - 1)];
            while (typeTree.Height != 0 && typeTree.HashCode != hashCode)
            {
                typeTree = hashCode < typeTree.HashCode ? typeTree.Left : typeTree.Right;
            }

            if (typeTree.Height != 0 && ReferenceEquals(typeTree.Key, type))
            {
                resolver = (Resolver<T>)typeTree.Value;
                return true;
            }

            if (typeTree.Duplicates.Items.Length > 0)
            {
                for (var index = 0; index < typeTree.Duplicates.Items.Length; index++)
                {
                    var keyValue = typeTree.Duplicates.Items[index];
                    if (ReferenceEquals(keyValue.Key, type))
                    {
                        resolver = (Resolver<T>) keyValue.Value;
                        return true;
                    }
                }
            }

            return TryCreateResolver(type, out resolver, null, this);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Resolver<T> GetResolver<T>(Type type, object tag)
        {
            if (!TryGetResolver<T>(type, tag, out var resolver))
            {
                return IssueResolver.CannotGetResolver<T>(this, new Key(type, tag));
            }

            return resolver;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver)
        {
            var key = new Key(type, tag);
            var hashCode = key.HashCode;
            var keyTree = _resolversByKey.Buckets[hashCode & (_resolversByKey.Divisor - 1)];
            while (keyTree.Height != 0 && keyTree.HashCode != hashCode)
            {
                keyTree = hashCode < keyTree.HashCode ? keyTree.Left : keyTree.Right;
            }

            if (keyTree.Height != 0 && Key.Equals(keyTree.Key, key))
            {
                resolver = (Resolver<T>)keyTree.Value;
                return true;
            }

            if (keyTree.Duplicates.Items.Length > 0)
            {
                for (var index = 0; index < keyTree.Duplicates.Items.Length; index++)
                {
                    var keyValue = keyTree.Duplicates.Items[index];
                    if (Key.Equals(keyValue.Key, key))
                    {
                        resolver = (Resolver<T>) keyValue.Value;
                        return true;
                    }
                }
            }

            return TryCreateResolver(type, out resolver, tag, null);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool TryGetResolver<T>(IContainer container, Type type, object tag, out Resolver<T> resolver)
        {
            return tag == null ? TryGetResolver(type, out resolver) : TryGetResolver(type, tag, out resolver);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool TryCreateResolver<T>(Type type, out Resolver<T> resolver, object tag, IContainer container)
        {
            var key = new Key(type, tag);
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                if (!registrationEntry.TryCreateResolver(key, container ?? this, out resolver))
                {
                    return false;
                }

                lock (_lockObject)
                {
                    _hasResolver = true;
                    _resolversByKey = _resolversByKey.Add(key, resolver);
                    if (tag == null)
                    {
                        _resolversByType = _resolversByType.Add(type, resolver);
                    }
                }

                return true;
            }

            if (_parent.TryGetResolver(container, type, tag, out resolver))
            {
                lock (_lockObject)
                {
                    _hasResolver = true;
                    _resolversByKey = _resolversByKey.Add(key, resolver);
                    if (tag == null)
                    {
                        _resolversByType = _resolversByType.Add(key.Type, resolver);
                    }
                }

                return true;
            }

            return false;
        }

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (TryGetRegistrationEntry(key, out var registrationEntry))
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
        private bool TryGetRegistrationEntry(Key key, out RegistrationEntry registrationEntry)
        {
            lock (_lockObject)
            {
                if (_registrationEntries.TryGetValue(key, out registrationEntry))
                {
                    return true;
                }

                var typeInfo = key.Type.Info();
                if (typeInfo.IsConstructedGenericType)
                {
                    var genericTypeDefinition = typeInfo.GetGenericTypeDefinition();
                    var genericTypeDefinitionKey = new Key(genericTypeDefinition, key.Tag);
                    if (_registrationEntries.TryGetValue(genericTypeDefinitionKey, out registrationEntry))
                    {
                        return true;
                    }
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

            lock (_lockObject)
            {
                Disposable.Create(_registrationEntries.Values).Dispose();
                _registrationEntries.Clear();

                _resolversByKey = HashTable<Key, object>.Empty;
                _resolversByType = HashTable<Type, object>.Empty;


                Disposable.Create(_resources).Dispose();
                _resources.Clear();
            }
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
            if (_allKeys == null)
            {
                lock (_lockObject)
                {
                    if (_allKeys == null)
                    {
                        _allKeys = _registrationEntries.Values.Distinct().Select(i => (IEnumerable<Key>) i.Keys).ToList();
                    }
                }
            }

            return _allKeys.Concat(_parent).GetEnumerator();
        }

        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return _eventSubject.Subscribe(observer);
        }

        private bool TryRegister([NotNull] Key key, [NotNull] RegistrationEntry registrationEntry)
        {
            bool added;
            lock (_lockObject)
            {
                added = !_registrationEntries.ContainsKey(key);
                if (added)
                {
                    _registrationEntries.Add(key, registrationEntry);
                    ResetResolvers();
                    _allKeys = null;
                }
            }

            if (added)
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Registration, key));
                return true;
            }

            return false;
        }

        private bool TryUnregister([NotNull] Key key)
        {
            bool removed;
            lock (_lockObject)
            {
                removed = _registrationEntries.Remove(key);
                if (removed)
                {
                    ResetResolvers();
                    _allKeys = null;
                }
            }

            if (removed)
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Unregistration, key));
            }

            return false;
        }

        private void ResetResolvers()
        {
            if (!_hasResolver)
            {
                return;
            }

            foreach (var registration in _registrationEntries.Values)
            {
                registration.Reset();
            }

            _resolversByKey = HashTable<Key, object>.Empty;
            _resolversByType = HashTable<Type, object>.Empty;
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
