namespace SampleModels.Models
{
    using System;

    internal interface ITimer: IObservable<Tick>
    {
    }
}
