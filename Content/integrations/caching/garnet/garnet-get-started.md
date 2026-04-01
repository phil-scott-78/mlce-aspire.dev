---
title: Get started with the Garnet integration
description: Learn how to set up the Aspire Garnet hosting and client integrations simply.
order: 262
---



<IntegrationIcon Src="/assets/icons/garnet-icon.png" Alt="Garnet logo">

[Garnet](https://microsoft.github.io/garnet/) is a high-performance cache-store from Microsoft Research that complies with the Redis serialization protocol (RESP). The Garnet integration enables you to connect to existing Garnet instances, or create new instances from Aspire with the `ghcr.io/microsoft/garnet` container image.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Garnet integrations in a simple configuration. If you already have this knowledge, see [Garnet hosting integration](/integrations/caching/garnet/garnet-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Garnet hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Garnet" />

Next, in the AppHost project, create instances of Garnet resources and pass them to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddGarnet("cache");

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(cache);

builder.Build().Run();
```

When Aspire adds a container image to the AppHost, it creates a new Garnet instance on your local machine.

> [!TIP]
> If you'd rather connect to an existing Garnet instance, call
>   `AddConnectionString` instead. For more information on how to use
>   `WithReference`, see [Resource dependencies](/docs/fundamentals/core-concepts/resources).

## Set up client integration

Garnet is Redis-compatible, so you use the same Redis client packages. To get started, install the package:

<InstallPackage PackageName="Aspire.StackExchange.Redis" />

In the `Program.cs` file of your client-consuming project, call the `AddRedisClient` extension method to register an `IConnectionMultiplexer`:

```csharp
builder.AddRedisClient(connectionName: "cache");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Garnet
>   resource in the AppHost project.

You can then retrieve the `IConnectionMultiplexer` instance using dependency injection:

```csharp
public class ExampleService(IConnectionMultiplexer connectionMux)
{
    // Use connection multiplexer...
}
```

## See also

- [Garnet hosting integration](/integrations/caching/garnet/garnet-host)
- [Garnet client integration](/integrations/caching/garnet/garnet-client)
- [Redis integration](/integrations/caching/redis/redis-get-started)
