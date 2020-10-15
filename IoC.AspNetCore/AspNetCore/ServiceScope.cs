// ReSharper disable ClassNeverInstantiated.Global
namespace IoC.Features.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal sealed class ServiceScope : IServiceScope, IServiceProvider, ISupportRequiredService
    {
        [NotNull] private readonly IScope _scope;
        [NotNull] private readonly IServiceProvider _serviceProvider;
        [NotNull] private readonly ISupportRequiredService _supportRequiredService;

        public ServiceScope(
            [NotNull] IScope scope,
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] ISupportRequiredService supportRequiredService)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _supportRequiredService = supportRequiredService ?? throw new ArgumentNullException(nameof(supportRequiredService));
        }

        public IServiceProvider ServiceProvider => this;

        public object GetService(Type serviceType)
        {
            using (_scope.Activate())
            {
                return _serviceProvider.GetService(serviceType);
            }
        }

        public object GetRequiredService(Type serviceType)
        {
            using (_scope.Activate())
            {
                return _supportRequiredService.GetRequiredService(serviceType);
            }
        }

        public void Dispose() => _scope.Dispose();
    }
}
