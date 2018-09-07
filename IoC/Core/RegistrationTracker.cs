namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker: IObserver<ContainerEvent>
    {
        private readonly Container _container;
        private readonly Dictionary<Key, IBuilder> _builders = new Dictionary<Key, IBuilder>();

        public RegistrationTracker(Container container)
        {
            _container = container;
        }

        public ICollection<IBuilder> Builders => _builders.Values;

        public void OnNext(ContainerEvent value)
        {
            _container.Reset();
            switch (value.EventTypeType)
            {
                case EventType.DependencyRegistration:
                    foreach (var key in value.Keys)
                    {
                        if (key.Type == typeof(IBuilder))
                        {
                            if (value.Container.TryGetResolver<IBuilder>(key.Type, key.Tag, out var resolver, out _, value.Container))
                            {
                                _builders[key] = resolver(value.Container);
                            }
                        }
                    }

                    break;

                case EventType.DependencyUnregistration:
                    foreach (var key in value.Keys)
                    {
                        _builders.Remove(key);
                    }

                    break;
            }
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }
    }
}
