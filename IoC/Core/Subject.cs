namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal sealed class Subject<T>: ISubject<T>
    {
        private readonly ILockObject _lockObject;
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public Subject([NotNull] ILockObject lockObject)
        {
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_lockObject)
            {
                _observers.Add(observer);
            }

            return Disposable.Create(() =>
            {
                lock (_lockObject)
                {
                    _observers.Remove(observer);
                }
            });
        }

        public void OnNext(T value)
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnNext(value);
                }
            }
        }

        public void OnError(Exception error)
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnError(error);
                }
            }
        }

        public void OnCompleted()
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnCompleted();
                }
            }
        }
    }
}
