namespace IoC.Comparison
{
    using System;
    using JetBrains.dotMemoryUnit;

    public class MemoryPerformanceCounter: IPerformanceCounter
    {
        public ITestResult Result { get; private set; }

        public IDisposable Run()
        {
            var checkPoint = dotMemory.Check();
            return Disposable.Create(() =>
            {
                dotMemory.Check(memory =>
                {
                    Result = new TestResult(memory.GetTrafficFrom(checkPoint), memory.GetDifference(checkPoint));
                });
            });
        }

        private class TestResult : ITestResult, IComparable
        {
            private readonly Traffic _traffic;
            private readonly long _newObjectsSizeInBytes;
            private readonly int _newObjectsCount;

            public TestResult(Traffic traffic, SnapshotDifference snapshotDifference)
            {
                _traffic = traffic;
                _newObjectsSizeInBytes = snapshotDifference.GetNewObjects().SizeInBytes;
                _newObjectsCount = snapshotDifference.GetNewObjects().ObjectsCount;
            }

            public int CompareTo(object obj)
            {
                return obj is TestResult other ? (int)(_traffic.AllocatedMemory.SizeInBytes - other._traffic.AllocatedMemory.SizeInBytes) : 0;
            }

            public override string ToString()
            {
                return $"Memory(allocated/collected bytes): {_traffic.AllocatedMemory.SizeInBytes}/{_traffic.CollectedMemory.SizeInBytes}\nObjects(allocated/collected): {_traffic.AllocatedMemory.ObjectsCount}/{_traffic.CollectedMemory.ObjectsCount}\nNewObjects(bytes/count): {_newObjectsSizeInBytes}/{_newObjectsCount}";
            }
        }
    }
}
