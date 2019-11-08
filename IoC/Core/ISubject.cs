namespace IoC.Core
{
    using System;

    internal interface ISubject<T>: IObservable<T>, IObserver<T> { }
}
