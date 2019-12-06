namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal interface IRegistrationTracker : IObserver<ContainerEvent>
    {
        IEnumerable<IBuilder> Builders { get; }
        
        IAutowiringStrategy AutowiringStrategy { get; }

        IEnumerable<ICompiler> Compilers { get; }
    }
}