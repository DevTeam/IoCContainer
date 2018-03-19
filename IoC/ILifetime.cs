namespace IoC
{
    using Extensibility;

    /// <summary>
    /// Represents a lifetime for an instance.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IExpressionBuilder<object>
    {
        /// <summary>
        /// Clone this lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Clone();
    }
}
