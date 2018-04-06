# [WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/index) sample

### Create the IoC container, like [here](App.xaml.cs)

```csharp
public partial class App
{
    internal IContainer Container;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container = IoC.Container.Create().Using<Configuration>();
        Container.Resolve<IMainWindowView>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Container.Dispose();
        base.OnExit(e);
    }
}
```

Where _Configuration_ is the [configuration](Configuration.cs) of IoC container.

### Implement a data provider class, for instance like [DataProvider](DataProvider.cs)

```csharp
public class DataProvider: ObjectDataProvider
{
    // Design Time Container
    private static readonly Lazy<IContainer> ContainerDesignTime 
        = new Lazy<IContainer>(() => Container.Create().Using<ConfigurationDesignTime>());

    public object Tag { get; set; }

    protected override void BeginQuery()
    {
        base.BeginQuery();
        if (Application.Current is App app)
            OnQueryFinished(app.Container.Resolve<object>(ObjectType, Tag));
        else 
            OnQueryFinished(ContainerDesignTime.Value.Resolve<object>(ObjectType, Tag));
    }
}
```

Where _ConfigurationDesignTime_ is the [desing time configuration](ConfigurationDesignTime.cs) of IoC container.

### Use it in XAML do bind view models like [here](Views/MainWindow.xaml)

```xml
<Window x:Class="WpfApp.Views.MainWindow" x:ClassModifier="internal"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:WpfApp"
        xmlns:vewModels="clr-namespace:WpfApp.VewModels">
    <Window.Resources>
        <local:DataProvider x:Key="ClockViewModel" ObjectType="{x:Type vewModels:IClockViewModel}" />
    </Window.Resources>
    <Grid DataContext="{StaticResource ClockViewModel}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
            <TextBlock Text="{Binding Date}" FontSize="64" />
            <TextBlock Text="{Binding Time}" FontSize="64" />
        </StackPanel>
    </Grid>
</Window>
```
