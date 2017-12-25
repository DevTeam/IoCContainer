namespace IoC
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull] Key[] keys);

        [NotNull] object CannotResolve([NotNull] IContainer container, Key key);

        [NotNull] IResolver CannotGetResolver([NotNull] IContainer container, Key key);

        [NotNull] ConstructorInfo CannotFindConsructor([NotNull] Type type, [NotNull] params Has[] dependencies);

        [NotNull] MethodInfo CannotFindMethod([NotNull] Type type, HasMethod method);

        [NotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        [NotNull] IFactory CannotBeCeated([NotNull] Type type);

        int CannotFindParameter([NotNull] ParameterInfo[] parameters, Parameter parameter);

        void CyclicDependenceDetected(Context context, [NotNull] Type type, int reentrancy);
    }
}
