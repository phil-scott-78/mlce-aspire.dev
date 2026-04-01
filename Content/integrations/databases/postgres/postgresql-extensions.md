---
title: PostgreSQL hosting extensions
order: 316
---



<IntegrationIcon Src="/assets/icons/postgresql-icon.png" Alt="PostgreSQL logo">
<Badge text="⭐ Community Toolkit" size="small" />

The Aspire Community Toolkit PostgreSQL hosting extensions package provides extra functionality to the [Aspire.Hosting.PostgreSQL](https://www.nuget.org/packages/Aspire.Hosting.PostgreSQL) hosting package.
</IntegrationIcon>

This package provides the following features:

- [Adminer](https://adminer.org/) management UI
- [DbGate](https://dbgate.org/) management UI

## Hosting integration

To get started with the Aspire Community Toolkit PostgreSQL hosting extensions, install the [CommunityToolkit.Aspire.Hosting.PostgreSQL.Extensions](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.PostgreSQL.Extensions) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.PostgreSQL.Extensions" />

## Add management UI

### DbGate management UI

To add the DbGate management UI to your PostgreSQL resource, call the `WithDbGate` method on the `PostgresServerResource` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithDbGate();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(postgres);

// After adding all resources, run the app...
```

This adds a new DbGate resource to the app host which is available from the Aspire dashboard. DbGate is a comprehensive database management tool that provides a web-based interface for managing your PostgreSQL databases.

### Adminer management UI

To add the Adminer management UI to your PostgreSQL resource, call the `WithAdminer` method on the `PostgresServerResource` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithAdminer();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(postgres);

// After adding all resources, run the app...
```

This adds a new Adminer resource to the app host which is available from the Aspire dashboard. Adminer is a lightweight database management tool that provides a simple web interface for database operations.

### Using both management UIs

You can use both management UIs together on the same PostgreSQL resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithDbGate()
                      .WithAdminer();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(postgres);

// After adding all resources, run the app...
```

## See also

- [Adminer documentation](https://www.adminer.org/)
- [DbGate documentation](https://dbgate.org/)
- [PostgreSQL integration](/integrations/databases/postgres/postgres-host)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
