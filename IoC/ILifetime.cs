namespace IoC
{
    using System.Linq.Expressions;
    using Extensibility;

    /// <summary>
    /// Represents a lifetime for an instance.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IExpressionBuilder<Expression>
    {
        /// <summary>
        /// Clone this lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Clone();
    }
}
