namespace WcfServiceLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using IoC;

    [AttributeUsage(AttributeTargets.Class)]
    public class IocAttribute: Attribute, IServiceBehavior
    {
        private static readonly IContainer Container = IoC.Container.Create().Using<Configuration>();

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var serviceType = serviceDescription.ServiceType;
            var resolver = Container.GetResolver<object>(serviceType);
            var instanceProvider = new InstanceProvider(() => resolver(Container));
            var dispatchRuntimes =
                from dispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>()
                from endpointDispatcher in dispatcher.Endpoints
                select endpointDispatcher.DispatchRuntime;

            foreach(var dispatchRuntime in dispatchRuntimes)
            {
                dispatchRuntime.InstanceProvider = instanceProvider;
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        private class InstanceProvider: IInstanceProvider
        {
            private readonly Func<object> _factory;

            public InstanceProvider(Func<object> factory) => _factory = factory;

            public object GetInstance(InstanceContext instanceContext) => _factory();

            public object GetInstance(InstanceContext instanceContext, Message message) => _factory();

            public void ReleaseInstance(InstanceContext instanceContext, object instance) { }
        }
    }
}