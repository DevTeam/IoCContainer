// ReSharper disable MemberCanBeProtected.Local
// ReSharper disable ForCanBeConvertedToForeach
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
    using Lifetimes;


    /// <summary>
    /// Allows to resolve enumeration of all instances related to corresponding bindings.
    /// </summary>
    [PublicAPI]
    public sealed class CollectionFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new CollectionFeature();

        private CollectionFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Resolvers<TT>(ctx.Container), new ContainerStateSingletonLifetime<object>(false));

            yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx, ctx.Container.Resolve<Resolvers<TT>>().Items), new ContainerStateSingletonLifetime<object>(false));
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => CreateList(ctx.Container.Resolve<Resolvers<TT>>().Items, ctx), new ContainerStateSingletonLifetime<object>(false));
            yield return container.Register(ctx => CreateArray(ctx.Container.Resolve<Resolvers<TT>>().Items, ctx), new ContainerStateSingletonLifetime<object>(false));
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => CreateHashSet(ctx.Container.Resolve<Resolvers<TT>>().Items, ctx), new ContainerStateSingletonLifetime<object>(false));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Resolve<Resolvers<TT>>().Items, ctx), new ContainerStateSingletonLifetime<object>(false));
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()), new ContainerStateSingletonLifetime<object>(false));
#endif
#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            yield return container.Register<IAsyncEnumerable<TT>>(ctx => new AsyncEnumeration<TT>(ctx.Container.Inject<IEnumerable<TT>>()), new ContainerStateSingletonLifetime<object>(false));
#endif
        }

        private static List<T> CreateList<T>(Resolver<T>[] resolvers, Context ctx)
        {
            var result = new List<T>(resolvers.Length);
            for (var i = 0; i < resolvers.Length; i++)
            {
                result.Add(resolvers[i](ctx.Container, ctx.Args));
            }

            return result;
        }

        private static T[] CreateArray<T>(Resolver<T>[] resolvers, Context ctx)
        {
            var result = new T[resolvers.Length];
            for (var i = 0; i < resolvers.Length; i++)
            {
                result[i] = resolvers[i](ctx.Container, ctx.Args);
            }

            return result;
        }

        private static HashSet<T> CreateHashSet<T>(Resolver<T>[] resolvers, Context ctx)
        {
            var result = new HashSet<T>();
            for (var i = 0; i < resolvers.Length; i++)
            {
                result.Add(resolvers[i](ctx.Container, ctx.Args));
            }

            return result;
        }

        internal sealed class Observable<T>: IObservable<T>
        {
            private readonly Resolver<T>[] _resolvers;
            private readonly Context _ctx;

            public Observable(Resolver<T>[] resolvers, Context ctx)
            {
                _resolvers = resolvers;
                _ctx = ctx;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                try
                {
                    for (var i = 0; i < _resolvers.Length; i++)
                    {
                        observer.OnNext(_resolvers[i](_ctx.Container, _ctx.Args));
                    }
                }
                catch (Exception error)
                {
                    observer.OnError(error);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            }
        }

        private sealed class Enumeration<T> : IEnumerable<T>
        {
            private readonly Context _context;
            private readonly Resolver<T>[] _resolvers;

            public Enumeration([NotNull] Context context, [NotNull] Resolver<T>[] resolvers)
            {
                _context = context;
                _resolvers = resolvers;
            }

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator() => new Enumerator<T>(_context, _resolvers);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class Enumerator<T>: IEnumerator<T>
        {
            private readonly Context _context;
            private readonly Resolver<T>[] _resolvers;
            private readonly int _length;
            private int _index = -1;
            private bool _hasCurrent;
            private T _current;

            public Enumerator([NotNull] Context context, [NotNull] Resolver<T>[] resolvers)
            {
                _context = context;
                _resolvers = resolvers;
                _length = resolvers.Length;
            }

            public bool MoveNext()
            {
                _hasCurrent = false;
                _current = default(T);
                return ++_index < _length;
            }

            public T Current
            {
                get
                {
                    if (!_hasCurrent)
                    {
                        _hasCurrent = true;
                        _current = _resolvers[_index](_context.Container, _context.Args);
                    }

                    return _current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public void Reset() => _index = -1;
        }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private sealed class AsyncEnumeration<T> : IAsyncEnumerable<T>
        {
            private readonly IEnumerable<T> _enumerable;

            public AsyncEnumeration(IEnumerable<T> enumerable) => _enumerable = enumerable;

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => 
                new AsyncEnumerator<T>(_enumerable.GetEnumerator());
        }

        private sealed class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public AsyncEnumerator(IEnumerator<T> enumerator) => 
                _enumerator = enumerator;

            public async ValueTask<bool> MoveNextAsync() =>
                await new ValueTask<bool>(_enumerator.MoveNext());

            public T Current => _enumerator.Current;

            public ValueTask DisposeAsync() => new ValueTask();
        }
#endif
        private class Resolvers<T>
        {
            public readonly Resolver<T>[] Items;

            public Resolvers(IContainer container)
            {
                Items = GetResolvers(container).ToArray();
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
