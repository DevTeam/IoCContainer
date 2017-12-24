namespace IoC.Internal
{
    using System;
    using System.Reflection;

    internal sealed class IssueResolver : IIssueResolver
    {
        public static readonly IIssueResolver Shared = new IssueResolver();

        private IssueResolver()
        {
        }

        public object CannotResolve(IContainer container, Key key)
        {
            throw new InvalidOperationException($"Cannot resolve instance for the key \"{key}\" from  the container \"{container}\".");
        }

        public IResolver CannotGetResolver(IContainer container, Key key)
        {
            throw new InvalidOperationException($"Cannot get resolver for the key \"{key}\" from the container \"{container}\".");
        }

        public ConstructorInfo CannotFindConsructor(ITypeInfo typeInfo, params Has[] dependencies)
        {
            throw new InvalidOperationException($"Cannot find an appropriate constructor in the type \"{typeInfo.Name}\" using dependencies: {string.Join(",", dependencies)}.");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }

        public IFactory CannotBeCeated(Type instanceType)
        {
            throw new InvalidOperationException($"An instance of the type \"{instanceType}\" cannot be created.");
        }

        public int CannotFindParameter(ParameterInfo[] parameters, Parameter parameter)
        {
            throw new InvalidOperationException($"The parameter \"{parameter}\" was not found.");
        }

        public void CyclicDependenceDetected(Context context, ITypeInfo typeInfo, int reentrancy)
        {
            throw new InvalidOperationException($"The cyclic dependence detected during creating instance of type \"{typeInfo.Name}\". The reentrancy is {reentrancy}.");
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            throw new InvalidOperationException($"The keys {string.Join(", ", keys)} cannot be registered in the container \"{container}\".");
        }
    }
}
