namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Interception<T> : IInterception<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        public Interception([NotNull] IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Tags = Enumerable.Empty<object>();
        }

        public Interception([NotNull] IInterception<T> binding, [CanBeNull] object tagValue)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Tags = binding.Tags.Concat(Enumerable.Repeat(tagValue, 1));
        }

        public IContainer Container { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }
    }
}