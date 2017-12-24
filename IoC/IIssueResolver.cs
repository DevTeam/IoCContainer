namespace IoC
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, Key[] keys);

        [NotNull] object CannotResolve([NotNull] IContainer container, Key key);

        [NotNull] IResolver CannotGetResolver([NotNull] IContainer container, Key key);

        [NotNull] ConstructorInfo CannotFindConsructor([NotNull] ITypeInfo typeInfo, [NotNull] params Has[] dependencies);

        [NotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        [NotNull] IFactory CannotBeCeated(Type instanceType);

        int CannotFindParameter([NotNull] ParameterInfo[] parameters, Parameter parameter);

        void CyclicDependenceDetected(Context context, [NotNull] ITypeInfo typeInfo, int reentrancy);
    }
}
