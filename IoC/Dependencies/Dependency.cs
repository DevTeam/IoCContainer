namespace IoC.Dependencies
{
    using System;

    [PublicAPI]
    public sealed class Dependency : IDependency
    {
        [NotNull] public readonly Key Key;
        public readonly Scope Scope;

        internal Dependency([NotNull] Key key, Scope scope)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Scope = scope;
        }

        public Type Type => Key.Type;
    }
}
