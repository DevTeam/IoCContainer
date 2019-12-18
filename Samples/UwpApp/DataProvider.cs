namespace UwpApp
{
    using System;
    using Windows.UI.Xaml;
    using IoC;
    using SampleModels;

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
}
