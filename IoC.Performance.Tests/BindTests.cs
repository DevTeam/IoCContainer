namespace IoC.Performance.Tests
{
    using Xunit;

    public class BindTests
    {
        private readonly Container _containerComplex;

        public BindTests()
        {
            _containerComplex = Container.Create("Core", Features.Set.Core);
        }

        [Fact]
        public void TestBind()
        {
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                _containerComplex.Bind(type).To(type);
            }
        }
    }
}