#if !NET40
namespace IoC.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    public class ConfigurationTests
    {
        [Theory]
        [InlineData("ref IoC.Tests; using IoC.Tests; Bind<IMyService33<>>().Tag(33).Tag().As(Lifetime.Singleton).Tag(\"abc\").To<MyService33<>>();")]
        [InlineData("using IoC.Tests; ref IoC.Tests; Bind<IMyService33<>>().Tag().Tag(33).As(Singleton).Tag(\"abc\").To<MyService33<>>();")]
        [InlineData("ref IoC.Tests;\nusing IoC.Tests;\nBind<IMyService33<>>().Tag(33)\n.Tag().As(Lifetime.Singleton).Tag(\"abc\").To<MyService33<>>();\n")]
        public void ShouldConfigureViaText(string configurationText)
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Apply(configurationText))
                {
                    // Then
                    var actualInstance1 = container.Resolve<IMyService33<int>>(33.AsTag());
                    var actualInstance2 = container.Resolve<IMyService33<int>>("abc".AsTag());
                    var actualInstance3 = container.Resolve<IMyService33<int>>();
                    actualInstance1.ShouldBeOfType<MyService33<int>>();
                    actualInstance1.ShouldBe(actualInstance2);
                    actualInstance1.ShouldBe(actualInstance3);
                }
            }
        }
    }

    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IMyService33<T>
    {
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MyService33<T> : IMyService33<T>
    {
    }
}
#endif