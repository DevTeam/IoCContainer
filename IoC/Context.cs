namespace IoC
{
    /// <summary>
    /// Represents the resolving context.
    /// </summary>
    [PublicAPI]
    public class Context
    {
        /// <summary>
        /// The resolving key.
        /// </summary>
        public readonly Key Key;

        /// <summary>
        /// The resolving container.
        /// </summary>
        [NotNull] public readonly IContainer Container;

        /// <summary>
        /// The optional resolving arguments.
        /// </summary>
        [NotNull][ItemCanBeNull] public readonly object[] Args;

        internal Context(
            Key key,
            [NotNull] IContainer container,
            [NotNull][ItemCanBeNull] params object[] args)
        {
            Key = key;
            Container = container;
            Args = args;
        }
    }
}
