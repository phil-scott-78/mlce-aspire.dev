---
title: Get started with the KurrentDB integrations
description: Learn how to set up the Aspire KurrentDB hosting and client integrations simply.
order: 295
---



<Badge text="⭐ Community Toolkit" size="small" />

[KurrentDB](https://www.kurrent.io/) is an event store database designed for Event Sourcing, offering high availability and reliability for storing events. The Aspire KurrentDB integration enables you to connect to existing KurrentDB instances or create new instances from Aspire using the KurrentDB container image.

In this introduction, you'll see how to install and use the Aspire KurrentDB integrations in a simple configuration. If you already have this knowledge, see [KurrentDB hosting integration](/integrations/databases/kurrentdb/kurrentdb-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire KurrentDB hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.KurrentDB" />

In the AppHost project, register and consume the KurrentDB integration using the `AddKurrentDB` extension method to add the KurrentDB container to the application builder:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var kurrentdb = builder.AddKurrentDB("kurrentdb");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(kurrentdb);

// After adding all resources, run the app...
```

## Set up client integration

In the client-consuming project, install the Aspire KurrentDB client integration:

<InstallPackage PackageName="CommunityToolkit.Aspire.KurrentDB" />

In the `Program.cs` file of your client-consuming project, call the `AddKurrentDBClient` extension to register a `KurrentDBClient` for use via the dependency injection container:

```csharp title="C# — Program.cs"
builder.AddKurrentDBClient(connectionName: "kurrentdb");
```

You can then retrieve the `KurrentDBClient` instance using dependency injection. For example, to retrieve the client from a service:

```csharp
public class ExampleService(KurrentDBClient client)
{
    // Use client...
}
```

For full reference details, see [KurrentDB hosting integration](/integrations/databases/kurrentdb/kurrentdb-host) and [KurrentDB client integration](/integrations/databases/kurrentdb/kurrentdb-client).

## See also

- [KurrentDB](https://www.kurrent.io/)
- [KurrentDB .NET Client](https://github.com/kurrent-io/KurrentDB-Client-Dotnet)
- [Aspire Community Toolkit GitHub repo](https://github.com/CommunityToolkit/Aspire)
