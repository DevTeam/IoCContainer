using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace BuildDocumentation;

internal static class ReadmeTarget
{
    public static async Task RunAsync(string solutionDirectory)
    {
        var scenariosDirectory = Path.Combine(solutionDirectory, "IoC.Tests", "UsageScenarios");
        var scenarios = new List<Scenario>();
        foreach (var path in Directory.EnumerateFiles(scenariosDirectory, "*.cs").OrderBy(path => path, StringComparer.Ordinal))
        {
            if (await ReadScenarioAsync(path) is { } scenario)
            {
                scenarios.Add(scenario);
            }
        }

        var groups = scenarios.GroupBy(scenario => scenario.Tag)
            .OrderBy(group => group.Key[0])
            .Select(group => new ScenarioGroup(
                group.Key[1..].Trim(),
                group.OrderBy(scenario => scenario.Priority + "_" + scenario.Title, StringComparer.InvariantCulture).ToArray()))
            .ToArray();
        var model = new ReadmeModel(
            groups,
            scenarios.OrderBy(scenario => scenario.Tag + scenario.Priority, StringComparer.InvariantCulture).ToArray());
        var examples = await RenderAsync(model);
        var body = await File.ReadAllTextAsync(Path.Combine(solutionDirectory, "README_BODY.md"));
        var footer = await File.ReadAllTextAsync(Path.Combine(solutionDirectory, "README_FOOTER.md"));
        var header = await File.ReadAllTextAsync(Path.Combine(solutionDirectory, "README_HEADER.md"));
        var features = await File.ReadAllTextAsync(Path.Combine(solutionDirectory, "README_FEATURES.md"));
        var readme = header + body + features + examples + footer;
        await File.WriteAllTextAsync(Path.Combine(solutionDirectory, "IoC", "README_TEMPLATE.md"), examples);
        var nugetReadme = features.Replace("](#", "](https://github.com/DevTeam/IoCContainer#");
        await File.WriteAllTextAsync(Path.Combine(solutionDirectory, "README_NUGET.md"), nugetReadme.ReplaceLineEndings(Environment.NewLine));
        var readmePath = Path.Combine(solutionDirectory, "README.md");
        await File.WriteAllTextAsync(readmePath, readme.ReplaceLineEndings(Environment.NewLine));
        Info($"Generated: {readmePath}");
    }

    private static async Task<string> RenderAsync(ReadmeModel model)
    {
        const string template = "/Templates/Readme.cshtml";
        var item = new RazorCompiledItemLoader().LoadItems(typeof(ReadmeTarget).Assembly)
            .Single(item => item.Identifier == template);
        using var writer = new StringWriter();
        var page = (RazorPage<ReadmeModel>)Activator.CreateInstance(item.Type)!;
        page.ViewData = new ViewDataDictionary<ReadmeModel>(
            new EmptyModelMetadataProvider(),
            new ModelStateDictionary()) { Model = model };
        page.ViewContext = new ViewContext { Writer = writer };
        page.HtmlEncoder = HtmlEncoder.Default;
        await page.ExecuteAsync();
        return writer.ToString().ReplaceLineEndings(Environment.NewLine);
    }

    private static async Task<Scenario?> ReadScenarioAsync(string path)
    {
        var metadata = new Dictionary<string, string>(StringComparer.Ordinal);
        var code = new StringBuilder();
        var indentation = 0;
        var inBody = false;
        foreach (var line in await File.ReadAllLinesAsync(path))
        {
            var trimmed = line.Trim();
            switch (trimmed)
            {
                case "// {":
                    inBody = true;
                    indentation = line.Length - line.TrimStart().Length;
                    continue;
                case "// }":
                    inBody = false;
                    continue;
            }

            if (trimmed.StartsWith("// $", StringComparison.Ordinal))
            {
                var parts = trimmed[4..].Split('=');
                if (parts.Length == 2)
                {
                    var (key, value) = (parts[0], parts[1]);
                    if (key is "header" or "footer" && metadata.TryGetValue(key, out var previous) && previous.Length > 0)
                    {
                        value = previous + Environment.NewLine + value;
                    }

                    metadata[key] = value;
                }

                continue;
            }

            if (!inBody)
            {
                continue;
            }

            code.AppendLine(line.Length > indentation ? line[indentation..] : line.TrimStart());
        }

        if (metadata.GetValueOrDefault("visible") != "true")
        {
            return null;
        }

        var title = metadata.GetValueOrDefault("description", "");

        return new Scenario(
            metadata.GetValueOrDefault("tag", " "),
            metadata.GetValueOrDefault("priority", ""),
            title,
            "#" + title.Replace(' ', '-').Replace("'", "").ToLowerInvariant() + "-",
            Path.GetFileName(path),
            metadata.GetValueOrDefault("header", ""),
            metadata.GetValueOrDefault("footer", ""),
            code.ToString());
    }
}

internal sealed record ReadmeModel(IReadOnlyList<ScenarioGroup> Groups, IReadOnlyList<Scenario> Scenarios);

internal sealed record ScenarioGroup(string Title, IReadOnlyList<Scenario> Scenarios);

internal sealed record Scenario(
    string Tag,
    string Priority,
    string Title,
    string Anchor,
    string FileName,
    string Header,
    string Footer,
    string Code);
