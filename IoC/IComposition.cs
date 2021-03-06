namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstract composition root.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public interface IComposition<out T>: IDisposable
    {
        /// <summary>
        /// The composition root instance.
        /// </summary>
        [NotNull] T Root { get; }
    }
}