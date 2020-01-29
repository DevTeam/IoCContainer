// ReSharper disable ForCanBeConvertedToForeach
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker : IRegistrationTracker
    {
        private readonly Container _container;
        private readonly ITracker[] _trackers = new ITracker[3];
        private readonly Tracker<IBuilder> _builderTracker;
        private readonly Tracker<IAutowiringStrategy> _autowiringStrategyTracker;
        private readonly Tracker<ICompiler> _compilerTracker;

        public RegistrationTracker([NotNull] Container container)
        {
            _container = container;

            _trackers[0] = _autowiringStrategyTracker = new Tracker<IAutowiringStrategy>((list, val) => list.Insert(0, val));
            _autowiringStrategyTracker.Items.Add(DefaultAutowiringStrategy.Shared);

            _trackers[1] = _compilerTracker = new Tracker<ICompiler>((list, val) => list.Insert(0, val));
            _compilerTracker.Items.Add(DefaultCompiler.Shared);

            _trackers[2] = _builderTracker = new Tracker<IBuilder>((list, val) => list.Add(val));
        }

        public IEnumerable<IBuilder> Builders => _builderTracker.Items;

        public IAutowiringStrategy AutowiringStrategy => _autowiringStrategyTracker.Items[0];

        public IEnumerable<ICompiler> Compilers => _compilerTracker.Items;

        public void OnNext(ContainerEvent value)
        {
            if (value.Keys == null)
            {
                return;
            }

            IContainer container;
            switch (value.EventType)
            {
                case EventType.RegisterDependency:
                    _container.Reset();
                    container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        for (var index = 0; index < _trackers.Length; index++)
                        {
                            if (_trackers[index].Track(key, container))
                            {
                                break;
                            }
                        }
                    }

                    break;

                case EventType.UnregisterDependency:
                    _container.Reset();
                    container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        for (var index = 0; index < _trackers.Length; index++)
                        {
                            if (_trackers[index].Untrack(key, container))
                            {
                                break;
                            }
                        }
                    }

                    break;
            }
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }

        private interface ITracker
        {
            bool Track(Key key, IContainer container);

            bool Untrack(Key key, IContainer container);
        }

        private class Tracker<T> : ITracker
            where T: class
        {
            private readonly Action<IList<T>, T> _updater;
            public readonly IList<T> Items = new List<T>();
            private Table<IContainer, T> _map = Table<IContainer, T>.Empty;

            public Tracker(Action<IList<T>, T> updater) => 
                _updater = updater;

            public bool Track(Key key, IContainer container)
            {
                if (key.Type != typeof(T))
                {
                    return false;
                }

                var hashCode = container.GetHashCode();
                if (_map.GetByRef(hashCode, container) != null)
                {
                    return true;
                }

                if (container.TryGetResolver<T>(key.Type, key.Tag, out var resolver, out _, container))
                {
                    var instance = resolver(container);
                    _map = _map.Set(hashCode, container, instance);
                    _updater(Items, instance);
                }

                return true;
            }

            public bool Untrack(Key key, IContainer container)
            {
                if (key.Type != typeof(T))
                {
                    return false;
                }

                var hashCode = container.GetHashCode();
                var instance = _map.GetByRef(hashCode, container);
                if (instance != null)
                {
                    Items.Remove(instance);
                }

                return true;
            }
        }
    }
}
