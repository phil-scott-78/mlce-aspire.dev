---
title: Garnet client integration
description: Learn how to use the Garnet client integration to connect to Garnet instances.
order: 264
---



<IntegrationIcon Src="/assets/icons/garnet-icon.png" Alt="Garnet logo">

Garnet is Redis-compatible, so you use the same Redis client packages. To get started, install the [📦 Aspire.StackExchange.Redis](https://www.nuget.org/packages/Aspire.StackExchange.Redis) NuGet package:
</IntegrationIcon>

<InstallPackage PackageName="Aspire.StackExchange.Redis" />

## Add Garnet client

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

## Connection properties

When you reference a Garnet resource using `WithReference`, the following connection properties are made available to the consuming project:

### Garnet

The Garnet resource exposes the following connection properties:

| Property Name | Description                                                                         |
| ------------- | ----------------------------------------------------------------------------------- |
| `Host`        | The hostname or IP address of the Garnet server                                     |
| `Port`        | The port number the Garnet server is listening on                                   |
| `Password`    | The password for authentication (available when a password parameter is configured) |
| `Uri`         | The connection URI, with the format `redis://:{Password}@{Host}:{Port}`             |

**Example connection string:**

```
Uri: redis://:p%40ssw0rd1@localhost:6379
```

> [!NOTE]
> Aspire exposes each property as an environment variable named
>   `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called
>   `cache` becomes `CACHE_URI`.

## Configuration and other client features

Since Garnet is Redis-compatible, all Redis client configuration, keyed services, health checks, and observability features work the same way. See the [Redis client integration](/integrations/caching/redis/redis-client) page for complete client integration documentation.

## Distributed caching and output caching

Garnet also supports Redis distributed caching and output caching. Install the respective packages:

- For distributed caching: [📦 Aspire.StackExchange.Redis.DistributedCaching](https://www.nuget.org/packages/Aspire.StackExchange.Redis.DistributedCaching)
- For output caching: [📦 Aspire.StackExchange.Redis.OutputCaching](https://www.nuget.org/packages/Aspire.StackExchange.Redis.OutputCaching)

See [Redis Distributed Caching](/integrations/caching/redis-distributed/redis-distributed-get-started) and [Redis Output Caching](/integrations/caching/redis-output/redis-output-get-started) for usage details.
