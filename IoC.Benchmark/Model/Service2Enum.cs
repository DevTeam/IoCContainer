﻿namespace IoC.Benchmark.Model
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public sealed class Service2Enum : IService2
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public Service2Enum(IEnumerable<IService3> services)
        {
            foreach (var service in services)
            {
            }
        }
    }
}