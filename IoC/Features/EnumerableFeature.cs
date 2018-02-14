namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [PublicAPI]
    public sealed class EnumerableFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new EnumerableFeature();

        private EnumerableFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind(typeof(IEnumerable<>))
                .As(Lifetime.ContainerSingleton)
                .To(typeof(InstanceEnumerable<>));
        }

        private sealed class InstanceEnumerable<T> : IEnumerable<T>, IDisposable, IObserver<ContainerEvent>
        {
            private readonly object[] _args;
            private static readonly Key ResolvingKey = Key.Create<T>();
            private readonly IContainer _container;
            private readonly object _lockObject = new object();
            private readonly IDisposable _eventsSubscription;
            private volatile IEnumerable<T> _resolvers;

            public InstanceEnumerable(Context context)
            {
                _container = context.Container;
                _args = context.Args;
                _eventsSubscription = _container.Subscribe(this);
            }

            public IEnumerator<T> GetEnumerator()
            {
                if (_resolvers != null)
                {
                    return _resolvers.GetEnumerator();
                }

                if (_resolvers == null)
                {
                    lock (_lockObject)
                    {
                        if (_resolvers == null)
                        {
                            Resolver<T> resolver = null;
                            _resolvers = (
                                    from key in _container
                                    where key.Type == ResolvingKey.Type
                                    where _container.TryGetResolver(_container, key.Type, key.Tag, out resolver)
                                    select resolver)
                                .ToList()
                                .Select(r => r(_container, _args));
                        }
                    }
                }

                return _resolvers.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
                _eventsSubscription.Dispose();
            }

            void IObserver<ContainerEvent>.OnNext(ContainerEvent value)
            {
                lock (_lockObject)
                {
                    _resolvers = null;
                }
            }

            void IObserver<ContainerEvent>.OnError(Exception error)
            {
            }

            void IObserver<ContainerEvent>.OnCompleted()
            {
            }
        }
    }
}
