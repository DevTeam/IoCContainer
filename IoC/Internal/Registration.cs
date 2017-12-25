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
            switch (lifetime)
            {
                case IoC.Lifetime.Transient:
                    Lifetime = registration.Container.Tag(IoC.Lifetime.Transient).Get<ILifetime>();
                    break;

                case IoC.Lifetime.Singletone:
                    Lifetime = registration.Container.Tag(IoC.Lifetime.Singletone).Get<ILifetime>();
                    break;

                case IoC.Lifetime.Container:
                    Lifetime = registration.Container.Tag(IoC.Lifetime.Container).Get<ILifetime>();
                    break;

                default: throw new NotSupportedException($"{registration.Lifetime} is not supported");
            }
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
