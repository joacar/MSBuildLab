# MSBuild lab environment

Experiment with MSBuild

* [GitInfo](https://github.com/kzu/GitInfo)
* [MinVer](https://github.com/adamralph/minver)

## Integration tests

Goal: Filter what test projects to run based on certain condition

Reason: Hinder slow running tests and tests that have external dependencies that might not be satisfied on the current machine

Hypothesis: Use MSBuild props/targets to achieve this

## SourceLink

Target that include SourceLink references based on build system.

| Property | Value | Comment |
|----------|---------|---------|
| SourceLinkProvider | Microsoft.SourceLink.GitHub | Library for the specific source control provider |

For full list of available libraries check [here](https://github.com/dotnet/sourcelink).

### Usage

Place the snippet in Directory.Build.targets file src folder.

```xml
  <PropertyGroup>
    <CI Condition="'$(CI)' == ''">false</CI>
  </PropertyGroup>

  <Import Project="../SourceLink.targets" Condition="'$(CI)' == 'true'" />
```

Run `dotnet pack -c Release -p:CI=true` and the target will be included and a .snupkg will be created.

### AzureDevOps Server

Follow the steps in this [blog post](https://www.liftrtech.net/home/blog?name=ASP.NET-Core-Debugging-Nuget-Packages-with-AzureDevOps-VSTS-Symbol-Server) to get up and running.

## Versioning

Versioning use the excellent [MinVer](https://github.com/adamralph/minver). Settings are present in `Version.props` and the target that update `AssemblyVersion` and `AssemblyFileVersion` is `Version.targets`.

### Parameters

| Property | Value | Comment |
|----------|-------|---------|
| RevisionId | Incremental integer unique for each build scoped to version | Default is try read value from `Build.BuildId` in Azure DevOps |

### Usage

Add `Version.props` and `Version.targets` to suitable directory and import props and targets from respective root files. Example belows assumes the two files are in the root directory.

To set the `RevisionId` from example environment variable on other build system, simple add it as parameter `dotnet build -c Release -p:RevisionId=$(APPVEYOR_BUILD_NUMBER)` or update the `Version.targets` file.

```xml
<!-- Directory.Build.props -->
<Import Project="Version.props">

<!-- src/Directory.Build.targets -->
<Import Project="../Version.targets" />
```

## Git information

Included MSBuild task for retrieving Git information and generate file so it can easily be retrieved at run time.