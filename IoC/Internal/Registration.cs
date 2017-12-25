namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Registration<T>: IRegistration<T>
    {
        public Registration([NotNull] IContainer container, [NotNull] params Type[] contractTypes)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            ContractsTypes = contractTypes ?? throw new ArgumentNullException(nameof(contractTypes));
            Lifetime = null;
            Tags = Enumerable.Empty<object>();
        }

        public Registration(IRegistration<T> registration, Lifetime lifetime)
        {
            Container = registration.Container;
            ContractsTypes = registration.ContractsTypes;
            Tags = registration.Tags;
            Lifetime = registration.Container.Tag(lifetime).Get<ILifetime>();
        }

        public Registration(IRegistration<T> registration, [NotNull] ILifetime lifetime)
        {
            Container = registration.Container;
            ContractsTypes = registration.ContractsTypes;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = registration.Tags;
        }

        public Registration(IRegistration<T> registration, [CanBeNull] object tagValue)
        {
            Container = registration.Container;
            ContractsTypes = registration.ContractsTypes;
            Lifetime = registration.Lifetime;
            Tags = registration.Tags.Concat(Enumerable.Repeat(tagValue, 1)).Distinct().ToArray();
        }

        public IContainer Container { get; }

        public IEnumerable<Type> ContractsTypes { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }
    }
}
