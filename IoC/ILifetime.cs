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
        /// Creates the similar lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Create();
    }
}
