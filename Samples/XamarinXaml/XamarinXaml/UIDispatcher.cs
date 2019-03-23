// ReSharper disable InconsistentNaming
namespace XamarinXaml
{
    using System;
    using SampleModels.VewModels;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class UIDispatcher: IUIDispatcher
    {
        public void Dispatch(Action action) => action();        
    }
}
