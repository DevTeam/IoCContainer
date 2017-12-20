namespace IoC.Impl
{
    using System;
    using System.Reflection;

    internal class IssueResolver: IIssueResolver
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

        public ConstructorInfo CannotFindConsructor(TypeInfo typeInfo, params Dependency[] dependencies)
        {
            throw new InvalidOperationException($"Cannot find an appropriate constructor in the type \"{typeInfo.Name}\" using dependencies: {string.Join(",", "dependencies")}.");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }
    }
}
