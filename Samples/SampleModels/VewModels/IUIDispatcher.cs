namespace SampleModels.VewModels
{
    using System;

    public interface IUIDispatcher
    {
        void Dispatch(Action action);
    }
}
