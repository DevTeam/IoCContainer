namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.ObjectModel;
    using System.Linq;
    using Core;
    using TypeExtensions = Core.TypeExtensions;

    /// <summary>
    /// Allows to resolve enumeration of all instances related to corresponding bindings.
    /// </summary>
    [PublicAPI]
    public sealed class CollectionFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new CollectionFeature();
        /// The high-performance instance.
        public static readonly IConfiguration HighPerformance = new CollectionFeature(true);

        private readonly bool _highPerformance;

        private CollectionFeature(bool highPerformance = false)
        {
            _highPerformance = highPerformance;
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            if (_highPerformance)
            {
                yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx.Container, ctx.Args).ToArray(), containerSingletonResolver(container));
            }
            else
            {
                yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx.Container, ctx.Args), containerSingletonResolver(container));
            }

            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToList());
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()), containerSingletonResolver(container));

#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()));
#endif
        }

        internal class Observable<T>: IObservable<T>
        {
            private readonly IEnumerable<T> _source;

            public Observable([NotNull] IEnumerable<T> source)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                foreach (var value in _source)
                {
                    observer.OnNext(value);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            }
        }

        internal class Enumeration<T>: IObserver<ContainerEvent>, IDisposable, IEnumerable<T>
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

            public IEnumerator<T> GetEnumerator()
            {
                return _currentResolvers.Value.Select(resolver => resolver(_container, _args)).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
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
                var typeInfo = TypeExtensions.Info<T>();
                return from keyGroup in container
                    let item = keyGroup.Select(key => new {type = CreateType(key.Type.Info(), typeInfo), tag = key.Tag}).FirstOrDefault(i => i.type != null)
                    where item != null
                    select container.GetResolver<T>(item.type, item.tag.AsTag());
            }

            private static Type CreateType(ITypeInfo registeredType, ITypeInfo targetType)
            {
                if (registeredType.IsGenericTypeDefinition)
                {
                    if (targetType.IsConstructedGenericType)
                    {
                        var genericTargetType = targetType.GetGenericTypeDefinition().Info();
                        if (genericTargetType.IsAssignableFrom(registeredType))
                        {
                            return registeredType.MakeGenericType(targetType.GenericTypeArguments);
                        }
                    }
                }

                return targetType.IsAssignableFrom(registeredType) ? registeredType.Type : null;
            }
        }
    }
}
