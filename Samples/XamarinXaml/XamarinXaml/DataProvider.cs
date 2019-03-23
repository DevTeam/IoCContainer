namespace XamarinXaml
{
    using System;
    using IoC;
    using SampleModels;
    using Xamarin.Forms;

    internal class DataProvider
    {
        // Design Time Container
        private static readonly Lazy<IContainer> ContainerDesignTime
            = new Lazy<IContainer>(() => Container.Create().Using(ClockDesignTimeConfiguration.Shared));

        private Type _type;

        public string ObjectType
        {
            set => _type = value != null ? Type.GetType(value, true) : typeof(object);
        }

        public object Tag { get; set; }

        public object It =>
            Application.Current is App app
                ? app.Container.Resolve<object>(_type, Tag)
                : ContainerDesignTime.Value.Resolve<object>(_type, Tag);
    }
}
