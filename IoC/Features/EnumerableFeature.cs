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
                .Bind<IEnumerable<TT>>()
                .As(Lifetime.ContainerSingleton)
                .To(ctx => new Enumeration<TT>(ctx.Container, ctx.Args));
        }

        private class Enumeration<T>: IObserver<ContainerEvent>, IDisposable, IEnumerable<T>
        {
            private readonly IContainer _container;
            [NotNull] [ItemCanBeNull] private readonly object[] _args;
            private readonly IDisposable _subscription;
            private volatile Lazy<Resolver<T>[]> _currentResolvers;

            public Enumeration([NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args)
            {
                _container = container;
                _args = args;
                _subscription = container.Subscribe(this);
                Reset();
            }

            public void OnNext(ContainerEvent value)
            {
                Reset();
            }

            public void OnError(Exception error)
            {
            }

            public void OnCompleted()
            {
            }

            public void Dispose()
            {
                _subscription.Dispose();
            }

            private void Reset()
            {
                lock (_subscription)
                {
                    _currentResolvers = new Lazy<Resolver<T>[]>(() => GetResolvers(_container).ToArray());
                }
            }

            private static IEnumerable<Resolver<T>> GetResolvers(IContainer container)
            {
                return from keyGroup in container
                    let key = keyGroup.FirstOrDefault(key => ReferenceEquals(key.Type, typeof(T)))
                    where !Equals(key, default(Key))
                    select container.GetResolver<T>(key.Type, key.Tag, container);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _currentResolvers.Value.Select(resolver => resolver(_container, _args)).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
