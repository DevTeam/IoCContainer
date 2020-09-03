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
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
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
        public static readonly IConfiguration Set = new CollectionFeature();

        private CollectionFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(new[] { typeof(IEnumerable<TT>) }, new EnumerableDependency());
            yield return container.Register(new[] { typeof(TT[]) }, new ArrayDependency());
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => new List<TT>(ctx.Container.Inject<TT[]>()));
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<TT[]>()));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<TT[]>()));
#endif
#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            yield return container.Register<IAsyncEnumerable<TT>>(ctx => new AsyncEnumeration<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
#endif
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private static IEnumerable<Key> GetKeys(IContainer container, Type type)
        {
            var targetType = type.Descriptor();
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
                        yield return new Key(typeToResolve);
                    }
                    else
                    {
                        yield return new Key(typeToResolve, tag);
                    }

                    break;
                }
            }
        }

        private sealed class Observable<T>: IObservable<T>
        {
            private readonly IEnumerable<T> _instances;

            public Observable(IEnumerable<T> instances) => _instances = instances;

            [MethodImpl((MethodImplOptions)0x200)]
            public IDisposable Subscribe(IObserver<T> observer)
            {
                try
                {
                    foreach(var instance in _instances)
                    {
                        observer.OnNext(instance);
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

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private sealed class AsyncEnumeration<T> : IAsyncEnumerable<T>
        {
            private readonly IEnumerable<T> _instances;

            public AsyncEnumeration(IEnumerable<T> instances) => _instances = instances;

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => 
                new AsyncEnumerator<T>(_instances.GetEnumerator());
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

        private class EnumerableDependency : IDependency
        {
            private static readonly TypeDescriptor EnumerableTypeDescriptor = typeof(Enumerable<>).Descriptor();

            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var type = buildContext.Key.Type.Descriptor();
                if (!type.IsConstructedGenericType())
                {
                    throw new BuildExpressionException($"Unsupported enumerable type {type}.", null);
                }

                var genericTypeArguments = type.GetGenericTypeArguments();
                if (genericTypeArguments.Length != 1)
                {
                    throw new BuildExpressionException($"Unsupported enumerable type {type}.", null);
                }

                var elementType = genericTypeArguments[0];
                var keys = GetKeys(buildContext.Container, elementType).ToArray();
                var positionVar = Expression.Variable(typeof(int));

                var conditionExpression =
                    keys.Length < 5
                        ? CreateConditions(buildContext, keys, elementType, positionVar)
                        : CreateSwitchCases(buildContext, keys, elementType, positionVar);

                if (buildContext.TryCompile(Expression.Lambda(conditionExpression, positionVar, buildContext.ContainerParameter, buildContext.ArgsParameter), out var factory, out error))
                {
                    var ctor = EnumerableTypeDescriptor.MakeGenericType(elementType).Descriptor().GetDeclaredConstructors().Single();
                    var enumerableExpression = Expression.New(ctor, Expression.Constant(factory), Expression.Constant(keys.Length), buildContext.ContainerParameter, buildContext.ArgsParameter);
                    expression = enumerableExpression;
                    return true;
                }

                expression = default(Expression);
                return false;
            }

            private static Expression CreateConditions(IBuildContext buildContext, Key[] keys, Type elementType, ParameterExpression positionVar)
            {
                var conditionExpression = CreateDefault(elementType);
                for (var i = keys.Length - 1; i >= 0; i--)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    conditionExpression = Expression.Condition(
                        Expression.Equal(positionVar, Expression.Constant(i)),
                        Expression.Convert(context.CreateExpression(), elementType),
                        conditionExpression);
                }

                return conditionExpression;
            }

            private static Expression CreateSwitchCases(IBuildContext buildContext, Key[] keys, Type elementType, ParameterExpression positionVar)
            {
                var cases = new SwitchCase[keys.Length];
                for (var i = 0; i < keys.Length; i++)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    cases[i] = Expression.SwitchCase(Expression.Convert(context.CreateExpression(), elementType), Expression.Constant(i));
                }

                return Expression.Switch(positionVar, CreateDefault(elementType), cases);
            }

            private static Expression CreateDefault(Type elementType) =>
                Expression.Block(
                    Expression.Throw(Expression.Constant(new BuildExpressionException("Invalid enumeration state.", null))),
                    Expression.Default(elementType));
        }

        private sealed class Enumerable<T> : IEnumerable<T>
        {
            private readonly Func<int, IContainer, object[], T> _factory;
            private readonly int _count;
            private readonly IContainer _container;
            private readonly object[] _args;

            public Enumerable([NotNull] Func<int, IContainer, object[], T> factory, int count, IContainer container, object[] args)
            {
                _factory = factory;
                _count = count;
                _container = container;
                _args = args;
            }

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator() => new Enumerator<T>(_factory, _count, _container, _args);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class Enumerator<T> : IEnumerator<T>
        {
            private readonly Func<int, IContainer, object[], T> _factory;
            private readonly int _count;
            private readonly IContainer _container;
            private readonly object[] _args;
            private int _index = -1;
            private bool _hasCurrent;
            private T _current;

            public Enumerator([NotNull] Func<int, IContainer, object[], T> factory, int count, IContainer container, object[] args)
            {
                _factory = factory;
                _count = count;
                _container = container;
                _args = args;
            }

            [MethodImpl((MethodImplOptions)0x200)]
            public bool MoveNext()
            {
                _hasCurrent = false;
                _current = default(T);
                return ++_index < _count;
            }

            public T Current
            {
                [MethodImpl((MethodImplOptions)0x200)]
                get
                {
                    if (!_hasCurrent)
                    {
                        _hasCurrent = true;
                        _current = _factory(_index, _container, _args);
                    }

                    return _current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public void Reset() => _index = -1;
        }


        private class ArrayDependency: IDependency
        {
            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var type = buildContext.Key.Type.Descriptor();
                var elementType = type.GetElementType();
                if (elementType == null)
                {
                    throw new BuildExpressionException($"Unsupported array type {type}.", null);
                }

                var keys = GetKeys(buildContext.Container, elementType).ToArray();
                var expressions = new Expression[keys.Length];
                for (var i = 0; i < keys.Length; i++)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    expressions[i] = context.CreateExpression();
                }

                expression = Expression.NewArrayInit(elementType, expressions);
                error = default(Exception);
                return true;
            }
        }
    }
}
