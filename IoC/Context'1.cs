namespace IoC
{
    /// <summary>
    /// Represent the resolving context with an instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public sealed class Context<T>: Context
    {
        /// <summary>
        /// The resolved instance.
        /// </summary>
        public readonly T It;

        internal Context(
            T it,
            Key key,
            [NotNull] IContainer container,
            [NotNull] [ItemCanBeNull] params object[] args)
            : base(key, container, args)
        {
            It = it;
        }
    }
}
