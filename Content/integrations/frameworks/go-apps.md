---
title: Go integration
order: 341
---



<Badge text="⭐ Community Toolkit" size="small" />

The Aspire Go hosting integration enables you to run Go applications alongside your Aspire projects in the Aspire app host.

## Hosting integration

To get started with the Aspire Go hosting integration, install the [CommunityToolkit.Aspire.Hosting.Golang](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.Golang) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.Golang" />

### Add Go app

To add a Go application to your app host, use the `AddGolangApp` extension method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var goApp = builder.AddGolangApp(
    name: "go-api",
    workingDirectory: "../go-app")
    .WithHttpEndpoint(port: 8080, env: "PORT");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(goApp);

// After adding all resources, run the app...
```

The `AddGolangApp` method requires:

- **name**: The name of the resource in the Aspire dashboard
- **workingDirectory**: The path to the directory containing your Go application

### Configure endpoints

Go applications typically use environment variables to configure the port they listen on. Use `WithHttpEndpoint` to configure the port and set the PORT environment variable:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var goApp = builder.AddGolangApp("go-api", "../go-app")
    .WithHttpEndpoint(port: 8080, env: "PORT");

// After adding all resources, run the app...
```

Your Go application can read the PORT environment variable to determine which port to listen on:

```go
package main
