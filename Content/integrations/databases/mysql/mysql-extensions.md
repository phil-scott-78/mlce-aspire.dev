---
title: MySQL hosting extensions
order: 311
---



<IntegrationIcon Src="/assets/icons/mysqlconnector-icon.png" Alt="MySQL logo">
<Badge text="⭐ Community Toolkit" size="small" />

The Aspire Community Toolkit MySQL hosting extensions package provides extra functionality to the [Aspire.Hosting.MySql](https://www.nuget.org/packages/Aspire.Hosting.MySql) hosting package.
</IntegrationIcon>

This package provides the following features:

- [Adminer](https://adminer.org/) management UI
- [DbGate](https://dbgate.org/) management UI

## Hosting integration

To get started with the Aspire Community Toolkit MySQL hosting extensions, install the [CommunityToolkit.Aspire.Hosting.MySql.Extensions](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.MySql.Extensions) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.MySql.Extensions" />

## Add management UI

### DbGate management UI

To add the DbGate management UI to your MySQL resource, call the `WithDbGate` method on the `MySqlServerResourceBuilder` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
                   .WithDbGate();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(mysql);

// After adding all resources, run the app...
```

This adds a new DbGate resource to the app host which is available from the Aspire dashboard. DbGate is a comprehensive database management tool that provides a web-based interface for managing your MySQL databases.

### Adminer management UI

To add the Adminer management UI to your MySQL resource, call the `WithAdminer` method on the `MySqlServerResourceBuilder` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
                   .WithAdminer();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(mysql);

// After adding all resources, run the app...
```

This adds a new Adminer resource to the app host which is available from the Aspire dashboard. Adminer is a lightweight database management tool that provides a simple web interface for database operations.

### Using both management UIs

You can use both management UIs together on the same MySQL resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
                   .WithDbGate()
                   .WithAdminer();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(mysql);

// After adding all resources, run the app...
```

## See also

- [Adminer documentation](https://www.adminer.org/)
- [DbGate documentation](https://dbgate.org/)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
