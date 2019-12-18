// ReSharper disable InconsistentNaming
namespace WpfAppNetCore
{
    using System;
    using System.Windows;
    using SampleModels.VewModels;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class UIDispatcher: IUIDispatcher
    {
        public void Dispatch(Action action) => Application.Current?.Dispatcher?.Invoke(action);
    }
}
