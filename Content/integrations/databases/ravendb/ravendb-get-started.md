---
title: Get started with the RavenDB integrations
description: Learn how to set up the Aspire RavenDB hosting and client integrations simply.
order: 320
---



<Badge text="⭐ Community Toolkit" size="small" />

[RavenDB](https://ravendb.net/) is a high-performance, open-source NoSQL database designed for fast, efficient, and scalable data storage. It supports advanced features like ACID transactions, distributed data replication, and time-series data management, making it an excellent choice for modern application development. The Aspire RavenDB integration enables you to connect to existing RavenDB instances or create new instances from Aspire using the `docker.io/ravendb/ravendb` container image.

In this introduction, you'll see how to install and use the Aspire RavenDB integrations in a simple configuration. If you already have this knowledge, see [RavenDB hosting integration](/integrations/databases/ravendb/ravendb-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire RavenDB Hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.RavenDB" />

Next, in the AppHost project, call the `AddRavenDB` extension method to add a RavenDB server resource, then call `AddDatabase` on the server resource to add a database:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var ravenServer = builder.AddRavenDB("ravenServer");
var ravendb = ravenServer.AddDatabase("ravendb");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(ravendb)
       .WaitFor(ravendb);

// After adding all resources, build and run the app...
```

> [!CAUTION]
> A valid RavenDB license is required. If you don't have one yet, you can
>   request a free Community license at
>   [ravendb.net/license/request/community](https://ravendb.net/license/request/community).

## Set up client integration

In each of the consuming client projects, install the Aspire RavenDB client integration:

<InstallPackage PackageName="CommunityToolkit.Aspire.RavenDB.Client" />

In the `Program.cs` file of your client-consuming project, call the `AddRavenDBClient` extension method to register an `IDocumentStore` for use via the dependency injection container. The method takes a connection name parameter.

```csharp
builder.AddRavenDBClient(connectionName: "ravendb");
```

You can then retrieve the `IDocumentStore` instance using dependency injection. For example, to retrieve the client from a service:

```csharp
public class ExampleService(IDocumentStore client)
{
    // Use client...
}
```

For full reference details, see [RavenDB hosting integration](/integrations/databases/ravendb/ravendb-host) and [RavenDB client integration](/integrations/databases/ravendb/ravendb-client).

## See also

- [RavenDB](https://ravendb.net/)
- [RavenDB Dockerfile guide](https://docs.ravendb.net/6.2/start/containers/dockerfile/guide/)
- [Aspire Community Toolkit GitHub repo](https://github.com/CommunityToolkit/Aspire)
