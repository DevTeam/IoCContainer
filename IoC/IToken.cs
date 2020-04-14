namespace IoC
{
    using System;

    /// <summary>
    /// The binding token to manage binding lifetime.
    /// </summary>
    public interface IToken: IDisposable
    {
        /// <summary>
        /// The owner container.
        /// </summary>
        IMutableContainer Container { get; }
    }
}
