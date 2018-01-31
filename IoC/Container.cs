namespace IoC
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
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
        [NotNull] private readonly ConcurrentDictionary<Key, RegistrationEntry> _registrationEntries = new ConcurrentDictionary<Key, RegistrationEntry>();
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly System.Collections.Generic.List<IDisposable> _resources = new System.Collections.Generic.List<IDisposable>();
        [NotNull] private volatile HashTable<Key, object> _resolversByKey = HashTable<Key, object>.Empty;
        [NotNull] private volatile HashTable<Type, object> _resolversByType = HashTable<Type, object>.Empty;

        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var rootContainer = new Container(
                RootName,
                CoreFeature.Shared,
                EnumerableFeature.Shared,
                FuncFeature.Shared,
                TaskFeature.Shared,
                ConfigurationFeature.Shared);

            return new Container($"{RootName}{CreateContainerName(name)}", rootContainer, true);
        }

        [NotNull]
        public static Container CreatePure([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            var rootContainer = new Container(RootName, CoreFeature.Shared);
            return new Container($"{RootName}{CreateContainerName(name)}", rootContainer, true);
        }

        internal Container([NotNull] string name = "", [NotNull][ItemNotNull] params IConfiguration[] configurations)
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

            if (_parent is IResourceStore resourceStore)
            {
                resourceStore.AddResource(this);
            }
        }

        public IContainer Parent => _parent;

        public bool TryRegister(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var resolvers = new System.Collections.Generic.List<IDisposable>();
            try
            {
                foreach (var key in keys)
                {
                    var resolver = new RegistrationEntry(ResolverGenerator.Shared, dependency, lifetime, Disposable.Create(() => TryUnregister(key)));
                    resolvers.Add(resolver);
                    isRegistered &= TryRegister(key, resolver);
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

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            

            if (!TryGetResolver(new Key(type, tag), out resolver, container))
            {
                return false;
            }

            lock (_resources)
            {
                _resolversByType = _resolversByType.Add(type, resolver);
            }

            return true;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetResolver<T>(Key key, out Resolver<T> resolver, IContainer container = null)
        {
            var tagIsNull = key.Tag is null;
            var hashCode = key.HashCode;
            if (tagIsNull)
            {
                var type = key.Type;
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
                    foreach (var keyValue in typeTree.Duplicates.Items)
                    {
                        if (ReferenceEquals(keyValue.Key, type))
                        {
                            resolver = (Resolver<T>)keyValue.Value;
                            return true;
                        }
                    }
                }
            }

            var keyTree = _resolversByKey.Buckets[hashCode & (_resolversByKey.Divisor - 1)];
            while (keyTree.Height != 0 && keyTree.HashCode != hashCode)
            {
                keyTree = hashCode < keyTree.HashCode ? keyTree.Left : keyTree.Right;
            }

            if (keyTree.Height != 0 && Key.Equals(keyTree.Key, key))
            {
                resolver = (Resolver<T>)keyTree.Value;
            }

            if (keyTree.Duplicates.Items.Length > 0)
            {
                foreach (var keyValue in keyTree.Duplicates.Items)
                {
                    if (Key.Equals(keyValue.Key, key))
                    {
                        resolver = (Resolver<T>)keyValue.Value;
                    }
                }
            }

            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                resolver = registrationEntry.CreateResolver<T>(key, container ?? this);
                lock(_resources)
                {
                    _resolversByKey = _resolversByKey.Add(key, resolver);
                    if (tagIsNull)
                    {
                        _resolversByType = _resolversByType.Add(key.Type, resolver);
                    }
                }

                return true;
            }

            if (_parent.TryGetResolver(key, out resolver))
            {
                lock (_resources)
                {
                    _resolversByKey = _resolversByKey.Add(key, resolver);
                    if (tagIsNull)
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
                lifetime = registrationEntry.Lifetime;
                return true;
            }

            return _parent.TryGetDependency(key, out dependency, out lifetime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(Type type, object tag, out object instance, params object[] args)
        {
#if DEBUG
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var key = tag is null ? new Key(type) : new Key(type, tag);
            if (!TryGetResolver<object>(key, out var resolver, this))
            {
                instance = null;
                return false;
            }

            instance = resolver(this, args);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(object tag, out T instance, params object[] args)
        {
#if DEBUG
            if (args == null) throw new ArgumentNullException(nameof(args));
#endif
            var key = tag is null ? Key.KeyContainer<T>.Shared : Key.Create<T>(tag);
            if (!TryGetResolver<T>(key, out var resolver, this))
            {
                instance = default(T);
                return false;
            }

            instance = resolver(this, args);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetRegistrationEntry(Key key, out RegistrationEntry registrationEntry)
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

        public void Dispose()
        {
            if (_parent is IResourceStore resourceStore)
            {
                resourceStore.RemoveResource(this);
            }

            lock (_resources)
            {
                Disposable.Create(_registrationEntries.Values).Dispose();
                _registrationEntries.Clear();

                Disposable.Create(_resources).Dispose();
                _resources.Clear();
            }
        }

        void IResourceStore.AddResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_resources)
            {
                _resources.Add(resource);
            }
        }

        void IResourceStore.RemoveResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_resources)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Key> GetEnumerator()
        {
            var keys = _registrationEntries.Keys.ToList();
            return keys.Concat(_parent).GetEnumerator();
        }

        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return _eventSubject.Subscribe(observer);
        }

        private bool TryRegister([NotNull] Key key, [NotNull] RegistrationEntry registrationEntry)
        {
            if (_registrationEntries.TryAdd(key, registrationEntry))
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Registration, key));
                return true;
            }

            return false;
        }

        private bool TryUnregister([NotNull] Key key)
        {
            if (_registrationEntries.TryRemove(key, out var _))
            {
                _eventSubject.OnNext(new ContainerEvent(this, ContainerEvent.EventType.Unregistration, key));
            }

            return false;
        }

        void IObserver<ContainerEvent>.OnNext(ContainerEvent value)
        {
            if (!TryGetRegistrationEntry(value.Key, out var registrationEntry))
            {
                return;
            }

            registrationEntry.Reset();
            lock (_resources)
            {
                _resolversByKey = HashTable<Key, object>.Empty;
                _resolversByType = HashTable<Type, object>.Empty;
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
