﻿namespace IoC
{
    using System;

    /// <summary>
    /// Represents the generic type arguments marker.
    /// </summary>
    [PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class GenericTypeArgumentAttribute : Attribute { }
}
