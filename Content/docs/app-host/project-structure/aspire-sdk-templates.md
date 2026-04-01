---
title: Aspire templates
description: Learn about the Aspire templates and how to use them to create new apps.
order: 50
---



Aspire provides a number of project templates to help you create new apps. You can use these templates to create full Aspire solutions, or add individual projects to existing Aspire solutions.

The Aspire templates are available in the [📦 Aspire.ProjectTemplates](https://www.nuget.org/packages/Aspire.ProjectTemplates) NuGet package.

## Available templates

The Aspire templates allow you to create new apps pre-configured with the Aspire solutions structure and default settings. These projects also provide a unified debugging experience across the different resources of your app.

Aspire templates are available in two categories: solution templates and project templates. Solution templates create a new Aspire solution with multiple projects, while project templates create individual projects that can be added to an existing Aspire solution.

### Solution templates

The following Aspire solution templates are available (assume the solution is named _AspireSample_):

- **Aspire Empty App**: A minimal Aspire project that includes the following:
  - **AspireSample.AppHost**: An orchestrator project designed to connect and configure the different projects and services of your app.
  - **AspireSample.ServiceDefaults**: An Aspire shared project to manage configurations that are reused across the projects in your solution related to [resilience](https://learn.microsoft.com/dotnet/core/resilience/http-resilience), [service discovery](/docs/fundamentals/services-and-networking/service-discovery), and [telemetry](/docs/fundamentals/observability/telemetry).

- **Aspire Starter App**: In addition to the AppHost and ServiceDefaults projects, the Aspire Starter App also includes the following:
  - **AspireSample.ApiService**: An [ASP.NET Core Minimal API](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis) project is used to provide data to the frontend. This project depends on the shared ServiceDefaults project.
  - **AspireSample.Web**: An [ASP.NET Core Blazor App](https://learn.microsoft.com/aspnet/core/blazor) project with default Aspire service configurations, this project depends on the ServiceDefaults project.
  - **AspireSample.Test**: Either an MSTest, NUnit, or xUnit test project with project references to the AppHost and an example _WebTests.cs_ file demonstrating an integration test.

- **Aspire Starter App with ASP.NET Core and React**: This template includes the AppHost and ServiceDefaults projects, plus:
  - **AspireSample.ApiService**: An [ASP.NET Core Minimal API](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis) project that provides data to the frontend.
  - **AspireSample.Web**: A React-based frontend application with TypeScript configured to work with the Aspire app.

- **Aspire Starter App with FastAPI and React**: This template provides a Python-based starter app with the following projects:
  - **AspireSample.AppHost**: The orchestrator project for managing the app's services.
  - **AspireSample.ApiService**: A [FastAPI](https://fastapi.tiangolo.com/) Python backend service that provides data to the frontend.
  - **AspireSample.Web**: A React-based frontend application with TypeScript.

  For more information on getting started with Python in Aspire, see [Build your first app](/docs/get-started/first-app/first-app-csharp-apphost/?lang=python).

### Project templates

The following Aspire project templates are available:

- **Aspire AppHost**: A standalone AppHost project that can be used to orchestrate and manage the different projects and services of your app.

- **Aspire Test projects**: These project templates are used to create test projects for your Aspire app, and they're intended to represent functional and integration tests. The test projects include the following templates:
  - **MSTest**: A project that contains MSTest integration of an Aspire AppHost project.
  - **NUnit**: A project that contains NUnit integration of an Aspire AppHost project.
  - **xUnit**: A project that contains xUnit.net integration of an Aspire AppHost project.

  For more information on the test templates, see [Testing overview](/docs/testing/overview).

- **Aspire Service Defaults**: A standalone ServiceDefaults project that can be used to manage configurations that are reused across the projects in your solution related to [resilience](https://learn.microsoft.com/dotnet/core/resilience/http-resilience), [service discovery](/docs/fundamentals/services-and-networking/service-discovery), and [telemetry](/docs/fundamentals/observability/telemetry).

> [!CAUTION]
> The service defaults project template takes a `FrameworkReference` dependency on `Microsoft.AspNetCore.App`. This may not be ideal for some project types. For more information, see [Aspire service defaults](/docs/app-host/project-structure/csharp-service-defaults).

## Install the Aspire templates

The Aspire templates are included with the Aspire workload. To install the templates, run the following command:

```bash title=".NET CLI"
dotnet new install Aspire.ProjectTemplates
```

> [!TIP]
> To install a specific version of the Aspire templates, use the `::{version}` syntax. For example, to install version 13.1.0, run the following command:
> 
> ```bash title=".NET CLI"
> dotnet new install Aspire.ProjectTemplates::13.1.0
> ```

## Create solutions and projects using templates

To create an Aspire solution or project, use Visual Studio, Visual Studio Code, or the Aspire CLI, and base it on the available templates. Explore additional Aspire templates in the [Aspire samples](https://github.com/dotnet/aspire-samples) repository.

<br />

