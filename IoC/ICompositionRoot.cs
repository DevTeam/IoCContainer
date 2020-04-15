namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstract composition root.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    [PublicAPI]
    public interface ICompositionRoot<out TInstance>: IDisposable
    {
        /// <summary>
        /// The composition root instance.
        /// </summary>
        [NotNull] TInstance Instance { get; }
    }
}