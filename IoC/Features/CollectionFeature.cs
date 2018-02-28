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
        /// The shared instance.
        public static readonly IConfiguration Shared = new CollectionFeature();

        private CollectionFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind<IEnumerable<TT>>()
                .As(Lifetime.ContainerSingleton)
                .To(ctx => new Enumeration<TT>(ctx.Container, ctx.Args));

            yield return container
                .Bind<List<TT>, IList<TT>, ICollection<TT>>()
                .To(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToList());

            yield return container
                .Bind<HashSet<TT>, ISet<TT>>()
                .To(ctx => new HashSet<TT>(ctx.Container.Inject<IEnumerable<TT>>()));

            yield return container
                .Bind<IObservable<TT>>()
                .As(Lifetime.ContainerSingleton)
                .To(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()));

#if !NET40
            yield return container
                .Bind<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>()
                .To(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()));
#endif
        }

        private class Observable<T>: IObservable<T>
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
                    select container.GetResolver<T>(item.type, item.tag, container);
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

                if (targetType.IsAssignableFrom(registeredType))
                {
                    return registeredType.Type;
                }

                return null;
            }
        }
    }
}
