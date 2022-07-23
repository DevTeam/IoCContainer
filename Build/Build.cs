using System.Collections.ObjectModel;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
// ReSharper disable CheckNamespace
// ReSharper disable ArrangeTypeModifiers

class Build
{
    private static readonly string[] PackageProjects =
    {
        "IoC",
        "IoC.Source",
        "IoC.AspNetCore",
        "IoC.AspNetCore.Source",
        "IoC.Interception",
        "IoC.Interception.Source"
    };

    private readonly Settings _settings;
    private readonly IBuildRunner _buildRunner;
    private readonly ITeamCityWriter _teamCityWriter;

    public Build(
        Settings settings, 
        IBuildRunner buildRunner,
        ITeamCityWriter teamCityWriter)
    {
        _settings = settings;
        _buildRunner = buildRunner;
        _teamCityWriter = teamCityWriter;
    }

    public ReadOnlyCollection<string> Run()
    {
        var currentDir = Environment.CurrentDirectory;
        if (!File.Exists(_settings.SolutionFile))
        {
            Error($"Cannot find the solution \"{_settings.SolutionFile}\". Current directory is \"{currentDir}\".");
            Environment.Exit(1);
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
            Environment.Exit(1);
        }

        var configuration = Property.Get("configuration", _settings.Configuration);
        var buildProps = new[]
        {
            ("version", _settings.Version.ToString()),
            ("MSBuildNative", nativeMsbuildPath)
        };
        
        Assertion.Succeed(
            _buildRunner.Run(new DotNetBuild()
                .WithProject(_settings.SolutionFile)
                .WithConfiguration(configuration)
                .WithProps(buildProps))
        );

        Assertion.Succeed(
            _buildRunner.Run(new DotNetTest()
                .WithProject(_settings.SolutionFile)
                .WithConfiguration(configuration)
                .WithProps(buildProps)
                .WithNoBuild(true))
        );

        Assertion.Succeed(
            _buildRunner.Run(
            new DotNetPack()
                .WithProject(_settings.SolutionFile)
                .WithConfiguration(configuration)
                .WithProps(buildProps)
                .WithNoBuild(true))
        );
        
        var packages = new List<string>();
        foreach (var packageProject in PackageProjects)
        {
            var packagePath = Path.Combine(packageProject, "bin", configuration);
            var packageFile = Directory.GetFiles(packagePath, $"*.{_settings.Version}.nupkg").SingleOrDefault();
            if (packageFile != default)
            {
                packages.Add(packageFile);
            }
            else
            {
                Error($"Cannot find a package in {packagePath}.");
                Environment.Exit(1);
            }
        }

        foreach (var package in packages)
        {
            _teamCityWriter.PublishArtifact($"{package} => .");
        }

        return packages.AsReadOnly();
    }
}