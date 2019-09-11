namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotGetGenericTypeArguments : ICannotGetGenericTypeArguments
    {
        public static readonly ICannotGetGenericTypeArguments Shared = new CannotGetGenericTypeArguments();

        private CannotGetGenericTypeArguments() { }

        public Type[] Resolve(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type {type.Name}.");
        }
    }
}