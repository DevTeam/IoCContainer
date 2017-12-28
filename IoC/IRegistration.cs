namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IRegistration<in T>
    {
        [NotNull] IContainer Container { get; }

        [NotNull][ItemNotNull] IEnumerable<Type> ContractsTypes { get; }

        [NotNull][ItemCanBeNull] IEnumerable<object> Tags { get; }

        [CanBeNull] ILifetime Lifetime { get; }
    }
}
