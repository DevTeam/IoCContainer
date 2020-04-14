﻿namespace IoC
{
    using System;

    /// <summary>
    /// Represents a holder for a created instance.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    [PublicAPI]
    public interface IHolder<out TInstance>: IDisposable
    {
        /// <summary>
        /// The created instance.
        /// </summary>
        [NotNull] TInstance Instance { get; }
    }
}