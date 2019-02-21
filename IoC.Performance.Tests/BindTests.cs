namespace IoC.Performance.Tests
{
    using NUnit.Framework;

    public class BindTests
    {
        private Container _containerComplex;
        
        [SetUp]
        public void SetUp()
        {
            _containerComplex = Container.CreateCore();
        }

        [TearDown]
        public void TearDown()
        {
            _containerComplex.Dispose();
        }

        [Test]
        public void Bind()
        {          
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                _containerComplex.Bind(type).To(type);
            }
        }
    }
}