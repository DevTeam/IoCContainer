namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of a binding token.
    /// </summary>
    public interface IToken: IDisposable
    {
        /// <summary>
        /// The configurable container owning the registered binding.
        /// </summary>
        IMutableContainer Container { get; }
    }
}
