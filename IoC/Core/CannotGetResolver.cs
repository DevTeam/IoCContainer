namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotGetResolver : ICannotGetResolver
    {
        public static readonly ICannotGetResolver Shared = new CannotGetResolver();

        private CannotGetResolver() { }

        public Resolver<T> Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (error == null) throw new ArgumentNullException(nameof(error));
            throw new InvalidOperationException($"Cannot get resolver for {key} from {container}.", error);
        }
    }
}