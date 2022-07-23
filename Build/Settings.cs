using NuGet.Versioning;
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable CheckNamespace

record Settings(
    string Configuration,
    NuGetVersion Version,
    string NuGetKey,
    VersionRange RequiredSdkRange,
    string SolutionFile);