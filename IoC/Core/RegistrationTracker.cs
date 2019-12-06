namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker: IRegistrationTracker
    {
        private readonly Container _container;
        private readonly Dictionary<Key, object> _instances = new Dictionary<Key, object>();
        private readonly List<IBuilder> _builders = new List<IBuilder>();
        private readonly List<IAutowiringStrategy> _autowiringStrategies = new List<IAutowiringStrategy> { DefaultAutowiringStrategy.Shared };
        private readonly List<ICompiler> _compilers = new List<ICompiler> { DefaultCompiler.Shared };

        public RegistrationTracker([NotNull] Container container)
        {
            _container = container;
            _autowiringStrategies.Add(DefaultAutowiringStrategy.Shared);
        }

        public IEnumerable<IBuilder> Builders => _builders;

        public IAutowiringStrategy AutowiringStrategy => _autowiringStrategies[0];

        public IEnumerable<ICompiler> Compilers => _compilers;

        public void OnNext(ContainerEvent value)
        {
            if (value.Keys == null)
            {
                return;
            }

            switch (value.EventType)
            {
                case EventType.RegisterDependency:
                    _container.Reset();
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

                        if (Track<ICompiler>(key, container, i => _compilers.Insert(0, i)))
                        {
                            // ReSharper disable once RedundantJumpStatement
                            continue;
                        }
                    }

                    break;

                case EventType.UnregisterDependency:
                    _container.Reset();
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

                        if (_compilers.Count > 1)
                        {
                            if (Untrack<ICompiler>(key, i => _compilers.Remove(i)))
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
