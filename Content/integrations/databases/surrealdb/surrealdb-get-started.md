---
title: Get started with the SurrealDB integrations
description: Learn how to set up the Aspire SurrealDB hosting and client integrations simply.
order: 330
---



<IntegrationIcon Src="/assets/icons/surrealdb-icon.png" Alt="SurrealDB logo">
<Badge text="⭐ Community Toolkit" size="small" />

[SurrealDB](https://surrealdb.com) is a native, open-source, multi-model database that lets you store and manage data across relational, document, graph, time-series, vector & search, and geospatial models—all in one place. The Aspire SurrealDB integration enables you to connect to existing SurrealDB instances or create new instances from Aspire using the `docker.io/surrealdb/surrealdb` container image.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire SurrealDB integrations in a simple configuration. If you already have this knowledge, see [SurrealDB hosting integration](/integrations/databases/surrealdb/surrealdb-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire SurrealDB Hosting integration in your Aspire AppHost project. This integration allows you to create and manage SurrealDB instances from your Aspire hosting projects:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.SurrealDb" />

Next, in the AppHost project, register and consume the SurrealDB integration using the `AddSurrealServer` extension method to add the SurrealDB container to the application builder. Chain a call to the returned resource builder to `AddNamespace` and then `AddDatabase`, to add a SurrealDB database resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSurrealServer("surreal")
                .AddNamespace("ns")
                .AddDatabase("db");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...
```

## Set up client integration

In each of the consuming client projects, install the Aspire SurrealDB client integration:

<InstallPackage PackageName="CommunityToolkit.Aspire.SurrealDb" />

In the `Program.cs` file of your client-consuming project, call the `AddSurrealClient` extension method to register a `SurrealDbClient` for use via the dependency injection container. The method takes a connection name parameter.

```csharp
builder.AddSurrealClient(connectionName: "db");
```

You can then retrieve the `SurrealDbClient` instance using dependency injection. For example, to retrieve the client from a service:

```csharp
public class ExampleService(SurrealDbClient client)
{
    // Use client...
}
```

For full reference details, see [SurrealDB hosting integration](/integrations/databases/surrealdb/surrealdb-host) and [SurrealDB client integration](/integrations/databases/surrealdb/surrealdb-client).

## See also

- [SurrealDB](https://surrealdb.com)
- [SurrealDB documentation](https://surrealdb.com/docs/surrealdb)
- [.NET SDK for SurrealDB](https://github.com/surrealdb/surrealdb.net)
- [Aspire Community Toolkit GitHub repo](https://github.com/CommunityToolkit/Aspire)
