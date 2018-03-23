namespace IoC.Extensibility
{
    /// <summary>
    /// Represents a container validator.
    /// </summary>
    [PublicAPI]
    public interface IValidator
    {
        /// <summary>
        /// Calidates a container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <returns>The validation results.</returns>
        ValidationResult Validate([NotNull] IContainer container);
    }
}
