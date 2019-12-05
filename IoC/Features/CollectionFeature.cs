namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Core;


    /// <summary>
    /// Allows to resolve enumeration of all instances related to corresponding bindings.
    /// </summary>
    [PublicAPI]
    public sealed class CollectionFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new CollectionFeature();

        private CollectionFeature()
        {
        }



        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx), containerSingletonResolver(container));
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToList());
            yield return container.Register(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToArray());
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()), containerSingletonResolver(container));
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()));
#endif
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            yield return container.Register<IAsyncEnumerable<TT>>(ctx => new AsyncEnumeration<TT>(ctx), containerSingletonResolver(container));
#endif
        }

        internal class Observable<T>: IObservable<T>
        {
            private readonly IEnumerable<T> _source;

            public Observable([NotNull] IEnumerable<T> source) => _source = source ?? throw new ArgumentNullException(nameof(source));

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

        private class Enumeration<T> : EnumerationBase<T>, IEnumerable<T>
        {
            public Enumeration([NotNull] Context context)
            : base(context)
            {
            }

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator()
            {
                var resolvers = GetResolvers();

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < resolvers.Length; i++)
                {
                    yield return resolvers[i](Context.Container, Context.Args);
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private class AsyncEnumeration<T> : EnumerationBase<T>, IAsyncEnumerable<T>
        {
            public AsyncEnumeration([NotNull] Context context)
                : base(context)
            {
            }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => 
                new AsyncEnumerator<T>(this, cancellationToken);
        }

        private class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly AsyncEnumeration<T> _enumeration;
            private readonly CancellationToken _cancellationToken;
            private readonly TaskScheduler _taskScheduler;
            private int _index = -1;

            public AsyncEnumerator(AsyncEnumeration<T> enumeration, CancellationToken cancellationToken)
            {
                _enumeration = enumeration;
                _cancellationToken = cancellationToken;
                var container = enumeration.Context.Container;
                _taskScheduler = container.TryGetResolver(out Resolver<TaskScheduler> taskSchedulerResolver) ? taskSchedulerResolver(container) : TaskScheduler.Current;
            }

            public async ValueTask<bool> MoveNextAsync() =>
                await new ValueTask<bool>(
                    StartTask(
                        new Task<bool>(
                            () =>
                            {
                                var resolvers = _enumeration.GetResolvers();
                                var index = Interlocked.Increment(ref _index);
                                if (index >= resolvers.Length)
                                {
                                    return false;
                                }

                                Current = resolvers[index](_enumeration.Context.Container, _enumeration.Context.Args);
                                return true;
                            },
                            _cancellationToken)));

            public T Current { get; private set; }

            public ValueTask DisposeAsync() => new ValueTask();

            private Task<bool> StartTask(Task<bool> task)
            {                
                task.Start(_taskScheduler);
                return task;
            }
        }
#endif

        private class EnumerationBase<T>: IObserver<ContainerEvent>, IDisposable
        {
            [NotNull] protected internal readonly Context Context;
            private readonly IDisposable _subscription;
            private volatile Resolver<T>[] _resolvers;

            public EnumerationBase([NotNull] Context context)
            {
                Context = context;
                _subscription = context.Container.Subscribe(this);
                Reset();
            }

            public void OnNext(ContainerEvent value) => Reset();

            public void OnError(Exception error) { }

            public void OnCompleted() { }

            public void Dispose() => _subscription.Dispose();

            public Resolver<T>[] GetResolvers()
            {
                if (_resolvers != null)
                {
                    return _resolvers;
                }

                lock (_subscription)
                {
                    if (_resolvers == null)
                    {
                        _resolvers = GetResolvers(Context.Container).ToArray();
                    }
                }

                return _resolvers;
            }

            private void Reset()
            {
                lock (_subscription)
                {
                    _resolvers = null;
                }
            }

            private static IEnumerable<Resolver<T>> GetResolvers(IContainer container)
            {
                var targetType = TypeDescriptorExtensions.Descriptor<T>();
                var isConstructedGenericType = targetType.IsConstructedGenericType();
                var genericTargetType = default(TypeDescriptor);
                Type[] genericTypeArguments = null;
                if (isConstructedGenericType)
                {
                    genericTargetType = targetType.GetGenericTypeDefinition().Descriptor();
                    genericTypeArguments = targetType.GetGenericTypeArguments();
                }

                foreach (var keyGroup in container)
                {
                    foreach (var key in keyGroup)
                    {
                        Type typeToResolve = null;
                        var registeredType = key.Type.Descriptor();
                        if (registeredType.IsGenericTypeDefinition())
                        {
                            if (isConstructedGenericType && genericTargetType.IsAssignableFrom(registeredType))
                            {
                                typeToResolve = registeredType.MakeGenericType(genericTypeArguments);
                            }
                        }
                        else
                        {
                            if (targetType.IsAssignableFrom(registeredType))
                            {
                                typeToResolve = key.Type;
                            }
                        }

                        if (typeToResolve == null)
                        {
                            continue;
                        }

                        var tag = key.Tag;
                        if (tag == null || ReferenceEquals(tag, Key.AnyTag))
                        {
                            yield return container.GetResolver<T>(typeToResolve);
                        }
                        else
                        {
                            yield return container.GetResolver<T>(typeToResolve, tag.AsTag());
                        }

                        break;
                    }
                }
            }
        }
    }
}
