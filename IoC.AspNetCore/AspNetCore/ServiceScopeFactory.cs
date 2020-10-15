namespace IoC.Features.AspNetCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal sealed class ServiceScopeFactory : IServiceScopeFactory
    {
        [NotNull] private readonly Func<IServiceScope> _serviceScopeFactory;

        public ServiceScopeFactory([NotNull] Func<IServiceScope> serviceScopeFactory) => 
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

        public IServiceScope CreateScope() => _serviceScopeFactory();
    }
}
