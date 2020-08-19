# [WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/index) sample

### Create the IoC container, like [here](App.xaml.cs)

```csharp
public partial class App
{
    internal readonly IContainer Container = IoC.Container
        .Create()
        .Using<ClockConfiguration>()
        .Using<AppConfiguration>();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container.Resolve<IMainWindowView>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Container.Dispose();
        base.OnExit(e);
    }
}
```

Where
* [ClockConfiguration](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/Samples/SampleModels/ClockConfiguration.cs) is IoC configuration for view models and models
* [AppConfiguration](AppConfiguration.cs) is IoC configuration for views

### Implement a data provider class, for instance like [DataProvider](DataProvider.cs)

```csharp
public class DataProvider: ObjectDataProvider
{
    private readonly IContainer _container =
        (Application.Current as App)?.Container
        // Resolves from Design Time Container
        ?? Container.Create().Using<ClockDesignTimeConfiguration>();

    public object Tag { get; set; }

    protected override void BeginQuery() => OnQueryFinished(_container.Resolve<object>(ObjectType, Tag));
}
```

Where _ClockDesignTimeConfiguration_ is the [desing time configuration](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/Samples/SampleModels/ClockDesignTimeConfiguration.cs) of IoC container for view models.

### Use it in XAML do bind view models like [here](Views/MainWindow.xaml)

```xml
<Window x:Class="WpfApp.Views.MainWindow" x:ClassModifier="internal"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:WpfApp"
        xmlns:viewModels="clr-namespace:Clock.ViewModels;assembly=Clock">
    <Window.Resources>
        <local:DataProvider x:Key="ClockViewModel" ObjectType="{x:Type viewModels:IClockViewModel}" />
    </Window.Resources>
    <Grid DataContext="{StaticResource ClockViewModel}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
            <!-- ReSharper disable once Xaml.BindingWithContextNotResolved -->
            <TextBlock Text="{Binding Date}" FontSize="64" />
            <!-- ReSharper disable once Xaml.BindingWithContextNotResolved -->
            <TextBlock Text="{Binding Time}" FontSize="64" />
            <StackPanel.Effect>
                <DropShadowEffect Color="Black" Direction="20" ShadowDepth="5" Opacity="0.5"/>
            </StackPanel.Effect>
        </StackPanel>
    </Grid>
</Window>


```
