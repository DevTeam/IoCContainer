using HostApi;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable ArrangeTypeModifiers

class Deploy
{
    private readonly Settings _settings;
    private readonly ICommandLineRunner _commandLineRunner;
    private readonly Build _build;

    public Deploy(
        Settings settings,
        ICommandLineRunner commandLineRunner,
        Build build)
    {
        _settings = settings;
        _commandLineRunner = commandLineRunner;
        _build = build;
    }

    public void Run()
    {
        var packages = _build.Run();
        if (!string.IsNullOrWhiteSpace(_settings.NuGetKey) && _settings.Version.Release != "dev")
        {
            var push = new DotNetNuGetPush().WithApiKey(_settings.NuGetKey).WithSources("https://api.nuget.org/v3/index.json");
            foreach (var package in packages)
            {
                Assertion.Succeed(_commandLineRunner.Run(push.WithPackage(package)), $"Pushing {Path.GetFileName(package)}");
            }
        }
        else
        {
            Info("Pushing NuGet packages were skipped.");
        }
    }
}