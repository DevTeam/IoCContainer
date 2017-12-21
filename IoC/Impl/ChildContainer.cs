namespace IoC.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    internal class ChildContainer: IContainer, IInstanceStore, IResourceStore, IEnumerable<Key>
    {
        private readonly string _name;
        private long _registrationId;
        [NotNull] private readonly IContainer _parentContainer;
        [NotNull] private readonly Dictionary<Key, IResolver> _resolvers = new Dictionary<Key, IResolver>();
        [NotNull] private readonly Dictionary<IInstanceKey, object> _instances = new Dictionary<IInstanceKey, object>();
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();

        public ChildContainer([NotNull] string name = "")
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parentContainer = new NullContainer();
            _resources.AddRange(RootConfiguration.Shared.Apply(this));
        }

        public ChildContainer(string name, [NotNull] IContainer parentContainer, bool root)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parentContainer = parentContainer ?? throw new ArgumentNullException(nameof(parentContainer));

            if (!root)
            {
                if (parentContainer.TryGet<IResourceStore>(out var parentResourceStore))
                {
                    parentResourceStore.AddResource(this);
                    AddResource(Disposable.Create(() => parentResourceStore.RemoveResource(this)));
                }
            }

            _resources.AddRange(ChildConfiguration.Shared.Apply(this));
        }

        public IContainer Parent => _parentContainer;

        public IDictionary<IInstanceKey, object> GetInstances()
        {
            return _instances;
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

        public IDisposable Register(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime = null)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            var registrationId = Interlocked.Increment(ref _registrationId);

            return Disposable.Create(
                from key in keys
                select RegisterResolver(key, new Resolver(registrationId, key, this, UnregisterResolver(key), factory, lifetime)));
        }

        public bool TryGetResolver(Key key, out IResolver resolver)
        {
            bool hasResolver;
            lock (_resolvers)
            {
                hasResolver = _resolvers.TryGetValue(key, out resolver);
                if (!hasResolver)
                {
                    var type = key.Contract.Type;
                    if (type.IsConstructedGenericType)
                    {
                        var genericInstanceType = type.GetGenericTypeDefinition();
                        key = new Key(new Contract(genericInstanceType), key.Tag);
                        hasResolver = _resolvers.TryGetValue(key, out resolver);
                    }
                }
            }

            return hasResolver || _parentContainer.TryGetResolver(key, out resolver);
        }

        public void Dispose()
        {
            lock (_resolvers)
            {
                Disposable.Create(
                    _instances.Values.OfType<IDisposable>()
                    .Concat(_resources)
                    .Concat(_resolvers.Values.Cast<IDisposable>()))
                    .Dispose();

                _instances.Clear();
                _resources.Clear();
                _resolvers.Clear();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Key> GetEnumerator()
        {
            List<Key> keys;
            lock (_resolvers)
            {
                keys = _resolvers.Keys.ToList();
            }

            return keys.Concat(_parentContainer as IEnumerable<Key> ?? Enumerable.Empty<Key>()).GetEnumerator();
        }

        private IDisposable RegisterResolver(Key key, Resolver resolver)
        {
            lock (_resolvers)
            {
                _resolvers.Add(key, resolver);
            }

            return resolver;
        }

        private IDisposable UnregisterResolver(Key key)
        {
            return Disposable.Create(() =>
            {
                lock (_resolvers)
                {
                    _resolvers.Remove(key);
                }
            });
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
