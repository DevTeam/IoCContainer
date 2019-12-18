# [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) sample

### [Create the IoC container](App.xaml.cs#L26)

```csharp
internal readonly IContainer Container = IoC.Container
    .Create()
    .Using<ClockConfiguration>()
    .Using<AppConfiguration>();
```

Where _Configuration_ is the [configuration](Configuration.cs) of IoC container.

### [Subscribe to event _Navigating_](App.xaml.cs#L44)

```csharp
rootFrame.Navigating += OnNavigating;
```

```csharp
private void OnNavigating(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
{
    if (!(sender is Frame frame))
    {
        return;
    }

    frame.Content = Container.Resolve<Page>(navigatingCancelEventArgs.SourcePageType, navigatingCancelEventArgs.Parameter);
    navigatingCancelEventArgs.Cancel = true;
}
```

### Implement a data provider class, for instance like [DataProvider](Samples/Uwp/DataProvider.cs)

```csharp
internal class DataProvider
{
    private readonly IContainer _container =
        (Application.Current as App)?.Container
        // Resolves from Design Time Container
        ?? Container.Create().Using<ClockDesignTimeConfiguration>();

    private Type _objectType;

    public string ObjectType
    {
        set => _objectType = value != null ? Type.GetType(value, true) : typeof(object);
    }

    public object Tag { get; set; }

    public object It => _container.Resolve<object>(_objectType, Tag);
}
```

Where _ConfigurationDesignTime_ is the [desing time configuration](ConfigurationDesignTime.cs) of IoC container.

### Use it in XAML do bind view models like [here](Views/MainWindow.xaml)

```xml
<Page
    x:Class="UwpApp.Views.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:uwpApp="using:UwpApp">
    <Page.Resources>
        <uwpApp:DataProvider x:Key="ClockViewModel" ObjectType="SampleModels.VewModels.IClockViewModel, SampleModels"/>
    </Page.Resources>
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center" DataContext="{StaticResource ClockViewModel}">
            <!-- ReSharper disable once Xaml.BindingWithContextNotResolved -->
            <TextBlock Text="{Binding It.Date}" FontSize="64" />
            <!-- ReSharper disable once Xaml.BindingWithContextNotResolved -->
            <TextBlock Text="{Binding It.Time}" FontSize="64" />
        </StackPanel>
    </Grid>
</Page>

```