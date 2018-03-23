namespace IoC
{
    /// <summary>
    /// Represents the resolver delegate.
    /// </summary>
    /// <typeparam name="T">The type of resolving instance.</typeparam>
    /// <param name="container">The resolving container.</param>
    /// <param name="args">The optional resolving arguments.</param>
    /// <returns></returns>
    [PublicAPI]
    [NotNull] public delegate T Resolver<out T>([NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args);
}

