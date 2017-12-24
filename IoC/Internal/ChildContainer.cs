namespace IoC.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ChildContainer : IContainer, IInstanceStore, IResourceStore, IEnumerable<Key>
    {
        private static long _registrationId;
        private readonly string _name;
        [NotNull] private readonly IContainer _parentContainer;
        [NotNull] private readonly Dictionary<Key, IResolver> _resolvers = new Dictionary<Key, IResolver>();
        [NotNull] private readonly Dictionary<IInstanceKey, object> _instances = new Dictionary<IInstanceKey, object>();
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();

        public ChildContainer([NotNull] string name = "", params IConfiguration[] configurations)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parentContainer = new NullContainer();
            if (configurations.Length > 0)
            {
                _resources.Add(this.Apply(configurations));
            }
        }

        public ChildContainer(string name, [NotNull] IContainer parentContainer, bool root, params IConfiguration[] configurations)
            :this(name, configurations)
        {
            _parentContainer = parentContainer ?? throw new ArgumentNullException(nameof(parentContainer));
            if (!root)
            {
                if (parentContainer.TryGet<IResourceStore>(out var parentResourceStore))
                {
                    parentResourceStore.AddResource(this);
                    AddResource(Disposable.Create(() => parentResourceStore.RemoveResource(this)));
                }
            }
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

        public bool TryRegister(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            var keyArray = keys as Key[] ?? keys.ToArray();
            lock (_resolvers)
            {
                if (keyArray.Any(key => _resolvers.ContainsKey(key)))
                {
                    registrationToken = default(IDisposable);
                    return false;
                }

                var registrationId = _registrationId++;
                registrationToken = Disposable.Create(keyArray.Select(key => RegisterResolver(key, new Resolver(registrationId, key, this, UnregisterResolver(key), factory, lifetime))));
                return true;
            }
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
            _resolvers.Add(key, resolver);
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
