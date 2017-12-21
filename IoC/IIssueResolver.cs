namespace IoC
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public interface IIssueResolver
    {
        [NotNull]
        object CannotResolve([NotNull] IContainer container, Key key);

        [NotNull]
        IResolver CannotGetResolver([NotNull] IContainer container, Key key);

        [NotNull]
        ConstructorInfo CannotFindConsructor([NotNull] TypeInfo typeInfo, [NotNull] params Has[] dependencies);

        [NotNull]
        Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        IFactory CannotBeCeated(Type instanceType);

        int CannotFindParameter([NotNull] ParameterInfo[] parameters, Parameter parameter);

        void CyclicDependenceDetected(Context context, [NotNull] TypeInfo typeInfo, int reentrancy);
    }
}
