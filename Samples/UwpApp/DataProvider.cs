namespace UwpApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Windows.UI.Xaml;
    using IoC;

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
}
