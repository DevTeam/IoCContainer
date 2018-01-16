namespace IoC.Core
{
    using System;

    internal interface IResourceStore: IDisposable
    {
        void AddResource([NotNull] IDisposable resource);

        void RemoveResource([NotNull] IDisposable resource);
    }
}
