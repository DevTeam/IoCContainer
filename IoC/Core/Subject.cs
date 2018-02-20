namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class Subject<T>: IObservable<T>, IObserver<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }

            return Disposable.Create(() =>
            {
                lock (_observers)
                {
                    _observers.Remove(observer);
                }
            });
        }

        public void OnNext(T value)
        {
            lock (_observers)
            {
                if (_observers.Count == 0)
                {
                    return;
                }

                foreach (var observer in _observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        public void OnError(Exception error)
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.OnError(error);
                }
            }
        }

        public void OnCompleted()
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.OnCompleted();
                }
            }
        }
    }
}
