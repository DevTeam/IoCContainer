namespace IoC
{
    using System;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull][ItemNotNull] Key[] keys);

        [NotNull] Tuple<IDependency, ILifetime> CannotResolveDependency([NotNull] IContainer container, [NotNull] Key key);

        [NotNull] Resolver<T> CannotGetResolver<T>([NotNull] IContainer container, [NotNull] Key key);

        [NotNull][ItemNotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        void CyclicDependenceDetected([NotNull] Key key, int reentrancy);

        [NotNull] Type CannotParseType([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);

        Lifetime CannotParseLifetime([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);

        [CanBeNull] object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);
    }
}
