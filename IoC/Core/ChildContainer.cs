namespace IoC.Core
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Collections;

    internal sealed class ChildContainer: IContainer, IResourceStore, IObserver<ContainerEvent>
    {
        [NotNull] private readonly ConcurrentDictionary<Key, RegistrationEntry> _registrationEntries = new ConcurrentDictionary<Key, RegistrationEntry>();
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly System.Collections.Generic.List<IDisposable> _resources = new System.Collections.Generic.List<IDisposable>();
        [NotNull] private HashTable<Key, object> _resolvers = HashTable<Key, object>.Empty;

        public ChildContainer([NotNull] string name = "", [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = new NullContainer();
            if (configurations.Length > 0)
            {
                _resources.Add(this.Apply(configurations));
            }
        }

        public ChildContainer([NotNull] string name, [NotNull] IContainer parent, bool root, [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            // Subscribe to events from the parent container
            AddResource(_parent.Subscribe(_eventSubject));

            // Subscribe to reset resolvers
            AddResource(_eventSubject.Subscribe(this));

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

        public bool TryGetResolver<T>(Key key, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = _resolvers.Search(key) as Resolver<T>;
            if (!(resolver is null))
            {
                return true;
            }

            container = container ?? this;
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                resolver = registrationEntry.CreateResolver<T>(key, container);
                _resolvers = _resolvers.Add(key, resolver);
                return true;
            }

            if (_parent.TryGetResolver(key, out resolver))
            {
                _resolvers = _resolvers.Add(key, resolver);
                return true;
            }

            return false;
        }

        public bool TryGetDependency(Key key, out IDependency dependency)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (TryGetRegistrationEntry(key, out var registrationEntry))
            {
                dependency = registrationEntry.Dependency;
                return true;
            }

            return _parent.TryGetDependency(key, out dependency);
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

            Disposable.Create(_registrationEntries.Values).Dispose();
            _registrationEntries.Clear();

            Disposable.Create(_resources).Dispose();
            _resources.Clear();
        }

        public void AddResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            _resources.Add(resource);
        }

        public void RemoveResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            _resources.Remove(resource);
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
            _resolvers = HashTable<Key, object>.Empty;
        }

        void IObserver<ContainerEvent>.OnError(Exception error)
        {
        }

        void IObserver<ContainerEvent>.OnCompleted()
        {
        }
    }
}
