namespace UwpApp
{
    using System;
    using Windows.ApplicationModel.Core;
    using SampleModels.VewModels;
    using static Windows.UI.Core.CoreDispatcherPriority;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class UIDispatcher: IUIDispatcher
    {
        public void Dispatch(Action action) => 
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Normal, () => action()).AsTask().Wait();
    }
}
