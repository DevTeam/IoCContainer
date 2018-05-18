namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a lifetime for an instance.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IDisposable
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression bodyExpression, [NotNull] IBuildContext buildContext);

        /// <summary>
        /// Creates the similar lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Create();

        /// <summary>
        /// Select default container to resolve dependencies.
        /// </summary>
        /// <param name="registrationContainer">The container where the entry was registered.</param>
        /// <param name="resolvingContainer">The container which is used to resolve an instance.</param>
        /// <returns>The selected container.</returns>
        [NotNull] IContainer SelectResolvingContainer([NotNull] IContainer registrationContainer, [NotNull] IContainer resolvingContainer);
    }
}
