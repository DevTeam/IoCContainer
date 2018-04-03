namespace WpfApp.Models
{
    using System;

    internal interface ITimer: IObservable<Tick>
    {
    }
}
