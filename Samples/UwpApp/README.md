# [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) sample

### [Create the IoC container](App.xaml.cs#L26)

```csharp
Container = IoC.Container.Create().Using<Configuration>();
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
    // Design Time Container
    private static readonly Lazy<IContainer> ContainerDesignTime 
        = new Lazy<IContainer>(() => Container.Create().Using<ConfigurationDesignTime>());

    private static readonly IEnumerable<TypeInfo> TypeInfos = typeof(DataProvider).GetTypeInfo().Assembly.DefinedTypes;
    private Type _type;

    public string ObjectType
    {
        private get => _type?.Name;
        set => 
            _type = value != null 
            ? TypeInfos.First(i => i.Name.EndsWith(value, StringComparison.CurrentCultureIgnoreCase)).AsType()
            : null;
    }

    public object Tag { get; set; }

    public object It => 
        Application.Current is App app 
            ? app.Container.Resolve<object>(_type, Tag)
            : ContainerDesignTime.Value.Resolve<object>(_type, Tag);
}
```

Where _ConfigurationDesignTime_ is the [desing time configuration](ConfigurationDesignTime.cs) of IoC container.

### Use it in XAML do bind view models like [here](Views/MainWindow.xaml)

```xml
<Page
    x:Class="UwpApp.Views.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:UwpApp">
    <Page.Resources>
        <local:DataProvider x:Key="ClockViewModel" ObjectType="IClockViewModel"/>
    </Page.Resources>
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center" DataContext="{StaticResource ClockViewModel}">
            <TextBlock Text="{Binding It.Date}" FontSize="64" />
            <TextBlock Text="{Binding It.Time}" FontSize="64" />
        </StackPanel>
    </Grid>
</Page>
```