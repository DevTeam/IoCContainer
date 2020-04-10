namespace IoC.Core
{
    using System;

    internal static class Observable
    {
        [NotNull]
        public static IObservable<T> Create<T>([NotNull] Func<IObserver<T>, IDisposable> factory) => 
            new InternalObservable<T>(factory ?? throw new ArgumentNullException(nameof(factory)));

        public static IDisposable Subscribe<T>([NotNull] this IObservable<T> source, [NotNull] Action<T> onNext, [NotNull] Action<Exception> onError, [NotNull] Action oncComplete) => 
            (source ?? throw new ArgumentNullException(nameof(source))).Subscribe(
                new InternalObserver<T>(
                    onNext ?? throw new ArgumentNullException(nameof(onNext)),
                    onError ?? throw new ArgumentNullException(nameof(onError)),
                    oncComplete ?? throw new ArgumentNullException(nameof(oncComplete))));

        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> source, Func<T, TResult> selector) =>
            Create<TResult>(observer => source.Subscribe(
                value => { observer.OnNext(selector(value)); },
                observer.OnError,
                observer.OnCompleted));

        public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> filter) =>
            Create<T>(observer => source.Subscribe(
                value => { if(filter(value)) observer.OnNext(value); },
                observer.OnError,
                observer.OnCompleted));

        private sealed class InternalObservable<T>: IObservable<T>
        {
            private readonly Func<IObserver<T>, IDisposable> _factory;

            public InternalObservable(Func<IObserver<T>, IDisposable> factory) => _factory = factory;

            public IDisposable Subscribe([NotNull] IObserver<T> observer) => _factory(observer ?? throw new ArgumentNullException(nameof(observer)));
        }

        private sealed class InternalObserver<T>: IObserver<T>
        {
            private readonly Action<T> _onNext;
            private readonly Action<Exception> _onError;
            private readonly Action _oncComplete;

            public InternalObserver(Action<T> onNext, Action<Exception> onError, Action oncComplete)
            {
                _onNext = onNext;
                _onError = onError;
                _oncComplete = oncComplete;
            }

            public void OnNext(T value) => _onNext(value);

            public void OnError(Exception error) => _onError(error);

            public void OnCompleted() => _oncComplete();
        }
    }
}
