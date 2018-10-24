namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker: IObserver<ContainerEvent>
    {
        private readonly Container _container;
        private readonly Dictionary<Key, object> _instances = new Dictionary<Key, object>();
        private readonly List<IBuilder> _builders = new List<IBuilder>();
        private readonly List<IAutowiringStrategy> _autowiringStrategies = new List<IAutowiringStrategy> { DefaultAutowiringStrategy.Shared };

        public RegistrationTracker(Container container)
        {
            _container = container;
            _autowiringStrategies.Add(DefaultAutowiringStrategy.Shared);
        }

        public ICollection<IBuilder> Builders => _builders;

        public IAutowiringStrategy AutowiringStrategy => _autowiringStrategies[0];

        public void OnNext(ContainerEvent value)
        {
            _container.Reset();
            switch (value.EventTypeType)
            {
                case EventType.DependencyRegistration:
                    var container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        if (Track<IBuilder>(key, container, i => _builders.Add(i)))
                        {
                            continue;
                        }

                        if (Track<IAutowiringStrategy>(key, container, i => _autowiringStrategies.Insert(0, i)))
                        {
                            // ReSharper disable once RedundantJumpStatement
                            continue;
                        }
                    }

                    break;

                case EventType.DependencyUnregistration:
                    foreach (var key in value.Keys)
                    {
                        if (_builders.Count > 0)
                        {
                            if (Untrack<IBuilder>(key, i => _builders.Remove(i)))
                            {
                                continue;
                            }
                        }

                        if (_autowiringStrategies.Count > 1)
                        {
                            if (Untrack<IAutowiringStrategy>(key, i => _autowiringStrategies.Remove(i)))
                            {
                                // ReSharper disable once RedundantJumpStatement
                                continue;
                            }
                        }
                    }

                    break;
            }
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }

        private bool Track<T>(Key key, IContainer container, Action<T> trackAction)
        {
            if (key.Type != typeof(T))
            {
                return false;
            }

            if (!container.TryGetResolver<T>(key.Type, key.Tag, out var resolver, out _, container))
            {
                return false;
            }

            var instance = resolver(container);
            _instances[key] = instance;
            trackAction(instance);
            return true;
        }

        private bool Untrack<T>(Key key, Action<T> untrackAction)
        {
            if (key.Type != typeof(T))
            {
                return false;
            }

            if (!_instances.TryGetValue(key, out var instance))
            {
                return true;
            }

            untrackAction((T)instance);
            return true;
        }
    }
}
