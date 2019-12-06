namespace IoC.Features.AspNetCore
{
    using System;
    using Issues;
    using Microsoft.Extensions.DependencyInjection;

    internal class TokenFactory : ICannotRegister
    {
        private readonly ServiceDescriptor _service;
        private readonly IBinding<object> _binding;
        private IBinding<object> _currentBinding;
        private int _tag;

        public TokenFactory(ServiceDescriptor service, IBinding<object> binding)
        {
            _service = service;
            _binding = binding;
            _currentBinding = binding;
        }

        public bool TryCreateToken(out IToken token)
        {
            if (_service.ImplementationType != null)
            {
                token = _currentBinding.To(_service.ImplementationType);
                return true;
            }

            if (_service.ImplementationFactory != null)
            {
                token = _currentBinding.To(ctx => _service.ImplementationFactory(ctx.Container.Inject<IServiceProvider>()));
                return true;
            }

            if (_service.ImplementationInstance != null)
            {
                token = _currentBinding.To(ctx => _service.ImplementationInstance);
                return true;
            }

            token = default(IToken);
            return false;
        }

        IToken ICannotRegister.Resolve(IContainer container, Key[] keys)
        {
            _currentBinding = _binding.Tag(_tag++);
            if (TryCreateToken(out var token))
            {
                return token;
            }

            throw new InvalidOperationException("Cannot create a binding token.");
        }
    }
}