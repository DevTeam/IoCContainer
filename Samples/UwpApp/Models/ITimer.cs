namespace UwpApp.Models
{
    using System;

    internal interface ITimer: IObservable<Tick>
    {
    }
}
