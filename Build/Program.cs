using CoreHtmlToImage;
using HostApi;
using NuGet.Versioning;
using Microsoft.Extensions.DependencyInjection;

var settings = new Settings(
    Environment.OSVersion.Platform == PlatformID.Win32NT ? "Release" : "Linux",
    Tools.GetNextVersion(new NuGetRestoreSettings("IoC.Container"), NuGetVersion.Parse(Property.Get("version", "1.0.0-dev", Tools.UnderTeamCity))),
    Property.Get("apiKey", string.Empty),
    VersionRange.Parse(Property.Get("RequiredSdkRange", "[6.0, )"), false),
    "IoC.sln");

GetService<IServiceCollection>()
    .AddSingleton<Root>()
    .AddSingleton(_ => settings)
    .AddSingleton<Build>()
    .AddSingleton<Deploy>()
    .AddSingleton<Benchmark>()
    .AddSingleton<HtmlConverter>()
    .BuildServiceProvider()
.GetRequiredService<Root>()
.Run();