namespace IoC.Core
{
    using System;
    using System.Linq;

    internal sealed class IssueResolver : IIssueResolver
    {
        public static readonly IIssueResolver Shared = new IssueResolver();

        private IssueResolver()
        {
        }

        public object CannotResolveInstance(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot resolver the instance for the key \"{key}\" from  the container \"{container}\".");
        }

        public Tuple<IDependency, ILifetime> CannotResolveDependency(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot find the dependency for the key \"{key}\" from  the container \"{container}\".");
        }

        public Resolver<T> CannotGetResolver<T>(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot get resolver for the key \"{key}\" from the container \"{container}\".");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }

        public void CyclicDependenceDetected(Key key, int reentrancy)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            if (reentrancy >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependence detected resolving the dependency \"{key}\". The reentrancy is {reentrancy}.");
            }
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container \"{container}\".");
        }

        public Type CannotParseType(string statementText, int statementLineNumber, int statementPosition, string typeName)
        {
            throw new InvalidOperationException($"Cannot parse the type \"{typeName}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public Lifetime CannotParseLifetime(string statementText, int statementLineNumber, int statementPosition, string lifetimeName)
        {
            throw new InvalidOperationException($"Cannot parse the lifetime \"{lifetimeName}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, string tag)
        {
            throw new InvalidOperationException($"Cannot parse the tag \"{tag}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }
    }
}
