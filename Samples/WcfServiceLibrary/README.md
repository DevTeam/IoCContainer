# [WCF](https://docs.microsoft.com/en-us/dotnet/framework/wcf/index) sample

### Implement a _Ioc_ attribute to mark services' classes, like [here](IocAttribute.cs)

```csharp
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
```

Where _Configuration_ is the [configuration](Configuration.cs) of IoC container.

### Implement an extension element, for instance like [this](IocExtensionElement.cs)

```csharp
public class IocExtensionElement: BehaviorExtensionElement
{
    public override Type BehaviorType => typeof(IocAttribute);

    protected override object CreateBehavior() => new IocAttribute();
}
```

### Add several elements to the configuration file, [for example](App.config)

Add the Ioc behavior to the WCF service:

```xml
<behaviors>
  <serviceBehaviors>
    <behavior>

      <Ioc />

    </behavior>
  </serviceBehaviors>
</behaviors>
```

Add the Ioc behavior extension:

```xml
<system.serviceModel>
  <extensions>
    <behaviorExtensions>

      <add name="Ioc" type="WcfServiceLibrary.IocExtensionElement, WcfServiceLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
 
   </behaviorExtensions>
  </extensions>
</system.serviceModel>
```

### Add _Ioc_ attribute for WCF service implementations, for [example](Service.cs)

```csharp
[Ioc]
public class Service : IService
{
    public string GetData(int value) => $"You entered: {value}";
}

```