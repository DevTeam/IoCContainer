using CoreHtmlToImage;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
// ReSharper disable StringLiteralTypo
// ReSharper disable CheckNamespace
// ReSharper disable ArrangeTypeModifiers

class Benchmark
{
    private readonly Settings _settings;
    private readonly ICommandLineRunner _commandLineRunner;
    private readonly ITeamCityWriter _teamCityWriter;
    private readonly HtmlConverter _htmlConverter;

    private static readonly string[] Reports = {
        "Singleton",
        "Transient",
        "Func",
        "Array",
        "Enum",
        "Complex"
    };

    public Benchmark(
        Settings settings, 
        ICommandLineRunner commandLineRunner,
        ITeamCityWriter teamCityWriter,
        HtmlConverter htmlConverter)
    {
        _settings = settings;
        _commandLineRunner = commandLineRunner;
        _teamCityWriter = teamCityWriter;
        _htmlConverter = htmlConverter;
    }

    public void Run()
    {
        var benchmark = new DotNetRun()
            .WithProject(Path.Combine("IoC.Benchmark", "IoC.Benchmark.csproj"))
            .WithFramework("net6.0")
            .WithConfiguration("Release")
            .WithArgs("--", "--filter")
            .AddArgs(Reports.Select(filter => $"*{filter}*").ToArray());

        Assertion.Succeed(_commandLineRunner.Run(benchmark), "Benchmarking");

        var resultsPath = Path.Combine("BenchmarkDotNet.Artifacts", "results");
        foreach (var reportName in Reports)
        {
            var reportFileName = Path.Combine(resultsPath, $"IoC.Benchmark.{reportName}-report");
            var reportFileNameHtml = reportFileName + ".html";
            if (!File.Exists(reportFileNameHtml))
            {
                Warning($"The benchmark report \"{reportFileNameHtml}\" s missing.");
                continue;
            }

            _teamCityWriter.PublishArtifact($"{reportFileNameHtml} => .");
            var bytes = _htmlConverter.FromHtmlString(File.ReadAllText(reportFileNameHtml));
            var reportFileNameJpg = reportFileName + ".jpg";
            File.WriteAllBytes(reportFileNameJpg, bytes);
            _teamCityWriter.PublishArtifact($"{reportFileNameJpg} => .");
        }
    }
}