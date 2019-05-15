// ReSharper disable ClassNeverInstantiated.Global
namespace EntityFrameworkCore
{
    using System.Threading;

    internal class IdGenerator : IIdGenerator
    {
        private int _id;

        public int Generate() => Interlocked.Increment(ref _id);
    }
}