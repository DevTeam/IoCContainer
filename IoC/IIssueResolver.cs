namespace IoC
{
    using System;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull] Key[] keys);

        [NotNull] Tuple<IDependency, ILifetime> CannotResolveDependency([NotNull] IContainer container, Key key);

        [NotNull] Resolver<T> CannotGetResolver<T>([NotNull] IContainer container, Key key);

        [NotNull][ItemNotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        void CyclicDependenceDetected(Key key, int reentrancy);

        [NotNull] Type CannotParseType([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);

        Lifetime CannotParseLifetime([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);

        [CanBeNull] object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);
    }
}
