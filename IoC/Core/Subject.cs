namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal class Subject<T>: IObservable<T>, IObserver<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        [MethodImpl((MethodImplOptions)256)]
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

        [MethodImpl((MethodImplOptions)256)]
        public void OnNext(T value)
        {
            lock (_observers)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnNext(value);
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public void OnError(Exception error)
        {
            lock (_observers)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnError(error);
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public void OnCompleted()
        {
            lock (_observers)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnCompleted();
                }
            }
        }
    }
}
