namespace IoC
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull][ItemNotNull] Key[] keys);

        [NotNull] object CannotResolve([NotNull] IContainer container, [NotNull] Key key);

        [NotNull] IResolver CannotGetResolver([NotNull] IContainer container, [NotNull] Key key);

        [NotNull] ConstructorInfo CannotFindConsructor([NotNull] Type type, [NotNull] params Has[] dependencies);

        [NotNull] MethodInfo CannotFindMethod([NotNull] Type type, HasMethod method);

        [NotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        [NotNull] IFactory CannotBeCeated([NotNull] Type type);

        int CannotFindParameter([NotNull][ItemNotNull] ParameterInfo[] parameters, Parameter parameter);

        void CyclicDependenceDetected(Context context, [NotNull] Type type, int reentrancy);

        [CanBeNull] object CannotResolveParameter([NotNull] Type type, Has dependency, [NotNull] ParameterInfo parameter);
    }
}
