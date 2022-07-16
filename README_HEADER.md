# Simple, powerful and fast Inversion of Control container for .NET

[![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[<img src="http://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_DevTeam_IoCContainer_BuildAndTest)/statusIcon"/>](http://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_DevTeam_IoCContainer_BuildAndTest&guest=1)

#### Base concepts:

- maximum performance
  - based on compiled expressions
  - free of boxing and unboxing
  - avoid using delegates

- thoughtful design
  - code is fully independent of the IoC framework
  - supports for BCL types out of the box
  - ultra-fine tuning of generic types
  - aspect-oriented DI
  - predictable dependency graph
  - _Func<... ,T>_ based factories passing a state

