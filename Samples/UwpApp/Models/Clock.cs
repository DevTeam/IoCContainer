namespace UwpApp.Models
{
    using System;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}