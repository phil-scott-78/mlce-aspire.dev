---
title: Write your first test
description: Learn how to test your Aspire solutions using xUnit.net, NUnit, and MSTest testing frameworks.
order: 62
---



In this article, you'll learn how to create a test project, write C# tests, and run them for your Aspire solutions. These aren't unit tests—they're functional and integration tests that validate how your distributed application components work together. Aspire provides testing project templates for xUnit.net, MSTest, and NUnit frameworks, each including a sample test to help you get started quickly.

## Conceptualizing distributed app testing

Before you start testing your Aspire solutions, you'll need the [📦 Aspire.Hosting.Testing](https://www.nuget.org/packages/Aspire.Hosting.Testing) NuGet package. This powerful package provides the `DistributedApplicationTestingBuilder` class—your gateway to creating a test host for distributed applications.

Think of the `DistributedApplicationTestingBuilder` as a test harness that launches your AppHost project with built-in instrumentation. This gives you precise control to access and manipulate the host throughout its lifecycle. You'll work with familiar Aspire types like `IDistributedApplicationBuilder` and `DistributedApplication` to build and start your [AppHost](/docs/fundamentals/core-concepts/app-host), making your tests feel natural and intuitive.

## Create a test project

To create an Aspire test project, use the testing project template. When starting a new Aspire project, both IDE and CLI tooling prompts you to create a test project for some templates. To add a test project to an existing Aspire solution, use the [`dotnet new`](https://learn.microsoft.com/dotnet/core/tools/dotnet-new) command:

Change directory to the newly created test project:

After adding the test project to your Aspire solution, add a project reference to the target AppHost. For example, if your Aspire solution contains an AppHost project named `AspireApp.AppHost`, add a project reference to it from the test project:

Finally, you can uncomment out the `IntegrationTest1.cs` file in the test project to explore the sample test.

## Explore the test project

The following example test project was created as part of the **Blazor & Minimal API starter** template. If you're unfamiliar with it, see [Build your first app—C#](/docs/get-started/first-app/first-app-csharp-apphost/?lang=csharp). The Aspire test project takes a project reference dependency on the target AppHost. Consider the template project:

The preceding project file is fairly standard. There's a `PackageReference` to the [📦 Aspire.Hosting.Testing](https://www.nuget.org/packages/Aspire.Hosting.Testing) NuGet package, which includes the required types to write tests for Aspire projects.

The template test project includes an `IntegrationTest1` class with a single test. The test verifies the following scenario:

- The AppHost is successfully created and started.
- The `webfrontend` resource is available and running.
- An HTTP request can be made to the `webfrontend` resource and returns a successful response (HTTP 200 OK).

Consider the following test class:

The preceding code:

- Relies on the `DistributedApplicationTestingBuilder.CreateAsync` API to asynchronously create the AppHost.
- The `appHost` is an instance of `IDistributedApplicationTestingBuilder` that represents the AppHost.
- The `appHost` instance has its service collection configured with logging and the standard HTTP resilience handler. For more information, see [Build resilient HTTP apps: Key development patterns](https://learn.microsoft.com/dotnet/core/resilience/http-resilience).
- The `appHost` has its `BuildAsync` method invoked, which returns the `DistributedApplication` instance as the `app`.
- The `app` is started asynchronously.
- An `HttpClient` is created for the `webfrontend` resource by calling `app.CreateHttpClient`.
- The `app.ResourceNotifications` is used to wait for the `webfrontend` resource to be healthy.
- A simple HTTP GET request is made to the root of the `webfrontend` resource.
- The test asserts that the response status code is `OK`.

## Test resource environment variables

To further test resources and their expressed dependencies in your Aspire solution, you can assert that environment variables are injected correctly. The following example demonstrates how to test that the `webfrontend` resource has an HTTPS environment variable that resolves to the `apiservice` resource:

The preceding code:

- Relies on the `DistributedApplicationTestingBuilder.CreateAsync` API to asynchronously create the AppHost.
- The `builder` instance is used to retrieve an `IResourceWithEnvironment` instance named "webfrontend" from the `Resources` property.
- Builds an `ExecutionConfiguration` for the `webfrontend` resource using `ExecutionConfigurationBuilder`, explicitly enabling environment variable configuration via `WithEnvironmentVariablesConfig`.
- Calls `BuildAsync` with `DistributedApplicationOperation.Publish` to generate the environment variables that will be published for the resource, including unresolved binding expressions.
- With the returned environment variables, the test asserts that the `webfrontend` resource has an HTTPS environment variable that resolves to the `apiservice` resource.

## Capture logs from tests

When writing tests for your Aspire solutions, you might want to capture and view logs to help with debugging and monitoring test execution. The `DistributedApplicationTestingBuilder` provides access to the service collection, allowing you to configure logging for your test scenarios.

### Configure logging providers

To capture logs from your tests, use the `AddLogging` method on the `builder.Services` to configure logging providers specific to your testing framework:

### Configure log filters

Since the _appsettings.json_ configuration from your application isn't automatically replicated in test projects, you need to explicitly configure log filters. This is important to avoid excessive logging from infrastructure components that might overwhelm your test output. The following snippet explicitly configures log filters:

```csharp
builder.Services.AddLogging(logging => logging
    .AddFilter("Default", LogLevel.Information)
    .AddFilter("Microsoft.AspNetCore", LogLevel.Warning)
    .AddFilter("Aspire.Hosting.Dcp", LogLevel.Warning));
```

The preceding configuration:

- Sets the default log level to `Information` for most application logs.
- Reduces noise from ASP.NET Core infrastructure by setting it to `Warning` level.
- Limits Aspire hosting infrastructure logs to `Warning` level to focus on application-specific logs.

### Popular logging packages

Different testing frameworks have different logging provider packages available to assist with managing logging during test execution:

## Summary

The Aspire testing project template makes it easier to create test projects for Aspire solutions. The template project includes a sample test that you can use as a starting point for your tests. The `DistributedApplicationTestingBuilder` follows a familiar pattern to the `WebApplicationFactory<T>` in ASP.NET Core. It allows you to create a test host for your distributed application and run tests against it.

Finally, when using the `DistributedApplicationTestingBuilder` all resource logs are redirected to the `DistributedApplication` by default. The redirection of resource logs enables scenarios where you want to assert that a resource is logging correctly.
