﻿#if !NET40
namespace IoC.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    public class ConfigurationTests
    {
        [Theory]
        [InlineData("ref IoC.Tests; using IoC.Tests; Bind<IMyService33<>>().Tag(33).Tag().Lifetime(Lifetime.Singleton).Tag(\"abc\").To<MyService33<>>();")]
        [InlineData("using IoC.Tests; ref IoC.Tests; Bind<IMyService33<>>().Tag().Tag(33).Lifetime(Singleton).Tag(\"abc\").To<MyService33<>>();")]
        [InlineData("ref IoC.Tests;\nusing IoC.Tests;\nBind<IMyService33<>>().Tag(33)\n.Tag().Lifetime(Lifetime.Singleton).Tag(\"abc\").To<MyService33<>>();\n")]
        public void ShouldConfigureViaText(string configurationText)
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Apply(configurationText))
                {
                    // Then
                    var actualInstance1 = container.Tag(33).Get<IMyService33<int>>();
                    var actualInstance2 = container.Tag("abc").Get<IMyService33<int>>();
                    var actualInstance3 = container.Get<IMyService33<int>>();
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