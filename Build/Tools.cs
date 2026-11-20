#nullable enable

using HostApi;
using NuGet.Versioning;
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

static class Tools
{
    public static bool UnderTeamCity => Environment.GetEnvironmentVariable("TEAMCITY_VERSION") != default;

    public static NuGetVersion GetNextVersion(NuGetRestoreSettings settings, NuGetVersion defaultVersion)
    {
        var floatRange = defaultVersion.Release != string.Empty
            ? new FloatRange(NuGetVersionFloatBehavior.Prerelease, defaultVersion)
            : new FloatRange(NuGetVersionFloatBehavior.Minor, defaultVersion);

        return GetService<INuGet>()
            .Restore(settings.WithHideWarningsAndErrors(true).WithVersionRange(new VersionRange(defaultVersion, floatRange)))
            .Where(i => i.Name == settings.PackageId)
            .Select(i => i.NuGetVersion)
            .Select(i => defaultVersion.Release != string.Empty 
                ? new NuGetVersion(i.Major, i.Minor, i.Patch, defaultVersion.Release)
                : new NuGetVersion(i.Major, i.Minor, i.Patch + 1))
            .Max() ?? defaultVersion;
    }

    public static void CheckRequiredSdk(Version requiredSdkVersion)
    {
        Version? sdkVersion = default;
        if (
            new DotNetCustom("--version")
                .WithShortName($"Checking the .NET SDK version {requiredSdkVersion}")
                .Run(output=> Version.TryParse(output.Line, out sdkVersion)).ExitCode == 0
            && sdkVersion != requiredSdkVersion)
        {
            Error($".NET SDK {requiredSdkVersion} is required.");
            Environment.Exit(1);
        }
    }
}

static class Property
{
    public static string Get(string name, string defaultProp, bool showWarning = false) =>
        Get(Props, name, defaultProp, showWarning);

    public static string Get(IProperties props, string name, string defaultProp, bool showWarning = false)
    {
        if (props.TryGetValue(name, out var prop) && !string.IsNullOrWhiteSpace(prop))
        {
            return prop;
        }

        var message = $"The property \"{name}\" was not defined, the default value \"{defaultProp}\" was used.";
        if (showWarning)
        {
            Warning(message);
        }
        else
        {
            Info(message);
        }

        return defaultProp;
    }
}

static class Assertion
{
    public static bool Succeed(int? exitCode, string shortName)
    {
        if (exitCode == 0)
        {
            return true;
        }

        Error($"{shortName} failed.");
        Environment.Exit(1);
        return false;
    }
    
    public static bool Succeed(ICommandLineResult result, string shortName)
    {
        if (result.Error != default)
        {
            Error(result.Error.ToString());
        }

        return Succeed(result.ExitCode, shortName);
    }

    public static async Task<bool> Succeed(Task<int?> exitCodeTask, string shortName) =>
        Succeed(await exitCodeTask, shortName);

    private static bool CheckBuildResult(IBuildResult result, string shortName)
    {
        foreach (var error in result.Errors)
        {
            Error(error.ToString());
        }

        foreach (var failedTest in
                 from testResult in result.Tests
                 where testResult.State == TestState.Failed
                 select testResult.ToString())
        {
            Error(failedTest);
        }

        if (result.Summary.Errors == 0 && result.Summary.FailedTests == 0)
        {
            return true;
        }

        Error($"{shortName} failed");
        return false;
    }

    public static void Succeed(IBuildResult result, string shortName)
    {
        if (!CheckBuildResult(result, shortName))
        {
            Environment.Exit(1);
        }
    }

    public static async Task<bool> Succeed(Task<IBuildResult> resultTask, string shortName)
    {
        if (CheckBuildResult(await resultTask, shortName))
        {
            return true;
        }

        Environment.Exit(1);
        return true;
    }

    public static async Task<bool> Succeed(Task<IBuildResult[]> resultsTask, string shortName)
    {
        if ((await resultsTask).All(result => CheckBuildResult(result, shortName)))
        {
            return true;
        }

        Environment.Exit(1);
        return true;
    }
}