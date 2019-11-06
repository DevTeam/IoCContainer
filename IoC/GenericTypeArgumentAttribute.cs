namespace IoC
{
    using System;

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GenericTypeArgumentAttribute : Attribute { }
}
