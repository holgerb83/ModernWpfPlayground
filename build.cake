#tool "dotnet:?package=GitVersion.Tool"

using System;
var target = Argument("target", "Publish");
var configuration = Argument("configuration", "Release");

var gitVersion = GitVersion(new GitVersionSettings());

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/App/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetBuild("./ModernWpfPlayground.sln", new DotNetBuildSettings
    {
        Configuration = configuration, ArgumentCustomization = c=> c.Append($"/p:Version={gitVersion.AssemblySemVer}")
    
    });
});

Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetPublish("./src/App/ModernWpfPlayground.csproj", new DotNetPublishSettings{
            Configuration = configuration, 
            EnableCompressionInSingleFile = true,
            PublishReadyToRun = true,
            PublishSingleFile = true,
            Runtime = "win-x64",
            SelfContained = true,
        });
    });

// Task("Test")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     DotNetCoreTest("./src/Example.sln", new DotNetCoreTestSettings
//     {
//         Configuration = configuration,
//         NoBuild = true,
//     });
// });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);