// Run this from the working directory where the solution or project to build is located.
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using NuGet.Versioning;

const string packageId = "IoC.Container";
const string solutionFile = "IoC.sln";

var currentDir = Environment.CurrentDirectory;
if (!File.Exists(solutionFile))
{
    Error($"Cannot find the solution \"{solutionFile}\". Current directory is \"{currentDir}\".");
    return 1;
}

var nativeMsbuildPath = (
    from  specialFolder in new[] { Environment.SpecialFolder.ProgramFiles, Environment.SpecialFolder.ProgramFilesX86 }
    // C:\Program Files (x86)\Microsoft Visual Studio
    from vsDir in Directory.EnumerateDirectories(Path.Combine(Environment.GetFolderPath(specialFolder)), "Microsoft Visual Studio")
    // C:\Program Files (x86)\Microsoft Visual Studio\2022
    from vsVersionDir in Directory.EnumerateDirectories(vsDir, "2022")
    // C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools
    from vsInstanceDir in Directory.EnumerateDirectories(vsVersionDir)
    // C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild
    from msbuildDir in Directory.EnumerateDirectories(vsInstanceDir, "MSBuild")
    // C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current
    from msbuildCurrentDir in Directory.EnumerateDirectories(msbuildDir, "Current")
    // C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin
    from msbuildBinDir in Directory.EnumerateDirectories(msbuildCurrentDir, "Bin")
    // Check MSBuild.exe
    where Directory.EnumerateFiles(msbuildBinDir, "MSBuild.exe").Any()
    select msbuildDir).FirstOrDefault();

if (nativeMsbuildPath != default)
{
    Info($"Native MSBuild path: {nativeMsbuildPath}");
}
else
{
    Error("Cannot find MSBuild for Windows. Please install it from https://visualstudio.microsoft.com/en/downloads/");
    Assertion.Exit();
}

var packageProjects = new []{ "IoC", "IoC.Source", "IoC.AspNetCore", "IoC.AspNetCore.Source", "IoC.Interception", "IoC.Interception.Source" }; 
var configuration = Property.Get("configuration", "Release");
var apiKey = Property.Get("apiKey", "");
var defaultVersion = NuGetVersion.Parse(Property.Get("version", "1.0.0-dev", Tools.UnderTeamCity));

var packageVersion = Version.GetNext(new NuGetRestoreSettings(packageId), defaultVersion);
var buildProps = new[]
{
    ("version", packageVersion.ToString()),
    ("MSBuildNative", nativeMsbuildPath ?? string.Empty)
};

Assertion.Succeed(
    new DotNetToolRestore()
        .Run(),
    "Restore tools"
);

Assertion.Succeed(
    new DotNetBuild()
        .WithProject(solutionFile)
        .WithConfiguration(configuration)
        .WithProps(buildProps)
        .Build()
);

Assertion.Succeed(
    new DotNetTest()
        .WithProject(solutionFile)
        .WithConfiguration(configuration)
        .WithProps(buildProps)
        .WithNoBuild(true)
        .Build()
);

Assertion.Succeed(
    new DotNetPack()
        .WithProject(solutionFile)
        .WithConfiguration(configuration)
        .WithProps(buildProps)
        .WithNoBuild(true)
        .Build()
);

var packages = new List<string>();
foreach (var packageProject in packageProjects)
{
    var packagePath = Path.Combine(packageProject, "bin", configuration);
    var packageFile = Directory.GetFiles(packagePath, $"*.{packageVersion}.nupkg").SingleOrDefault();
    if (packageFile != default)
    {
        packages.Add(packageFile);
    }
    else
    {
        Error($"Cannot find a package in {packagePath}.");
        Assertion.Exit();
    }
}

var teamCityWriter = GetService<ITeamCityWriter>();
foreach (var package in packages)
{
    teamCityWriter.PublishArtifact($"{package} => .");
}

if (!string.IsNullOrWhiteSpace(apiKey) && packageVersion.Release != "dev")
{
    var push = new DotNetNuGetPush().WithApiKey(apiKey).WithSources("https://api.nuget.org/v3/index.json");
    foreach (var package in packages)
    {
        Assertion.Succeed(push.WithPackage(package).Run(), $"Pushing {Path.GetFileName(package)}");
    }
}
else
{
    Info("Pushing NuGet packages were skipped.");
}

return 0;