namespace SampleModels.VewModels
{
    using System;

    // ReSharper disable once InconsistentNaming
    public interface IUIDispatcher
    {
        void Dispatch(Action action);
    }
}
