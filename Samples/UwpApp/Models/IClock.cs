namespace UwpApp.Models
{
    using System;

    internal interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
