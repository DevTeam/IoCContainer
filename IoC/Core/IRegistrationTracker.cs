namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal interface IRegistrationTracker : IObserver<ContainerEvent>
    {
        [NotNull] IEnumerable<IBuilder> Builders { get; }

        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        [NotNull] IEnumerable<ICompiler> Compilers { get; }
    }
}