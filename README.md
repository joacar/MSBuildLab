# MSBuild lab environment

Experiment with MSBuild

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

Place the snippet in Directory.Build.targets

```xml
  <PropertyGroup>
    <CI Condition="'$(CI)' == ''">false</CI>
  </PropertyGroup>

  <Import Project="../SourceLink.targets" Condition="'$(CI)' == 'true'" />
```

Run `dotnet pack -c Release -p:CI=true` and the target will be included and a .snupkg will be created.

### AzureDevOps Server

Follow the steps in this [blog post](https://www.liftrtech.net/home/blog?name=ASP.NET-Core-Debugging-Nuget-Packages-with-AzureDevOps-VSTS-Symbol-Server) to get up and running.