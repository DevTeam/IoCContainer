namespace WpfApp.Models
{
    using System;

    internal interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
