namespace WpfAppNetCore
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Data;
    using IoC;
    using SampleModels;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class DataProvider: ObjectDataProvider
    {
        private readonly IContainer _container =
            (Application.Current as App)?.Container
            // Resolves from Design Time Container
            ?? Container.Create().Using<ClockDesignTimeConfiguration>();

        public object Tag { get; set; }

        protected override void BeginQuery() => OnQueryFinished(_container.Resolve<object>(ObjectType, Tag));
    }
}
