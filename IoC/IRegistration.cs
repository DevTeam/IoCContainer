namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IRegistration<in T>
    {
        IContainer Container { get; }

        IEnumerable<Type> ContractsTypes { get; }

        IEnumerable<object> Tags { get; }

        ILifetime Lifetime { get; }
    }
}
