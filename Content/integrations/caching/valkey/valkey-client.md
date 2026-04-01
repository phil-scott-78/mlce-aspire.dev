---
title: Valkey client integration
description: Learn how to use the Valkey client integration to connect to Valkey instances.
order: 261
---



<IntegrationIcon Src="/assets/icons/valkey-icon.png" Alt="Valkey logo">

Valkey is Redis-compatible, so you use the same Redis client packages. To get started, install the [📦 Aspire.StackExchange.Redis](https://www.nuget.org/packages/Aspire.StackExchange.Redis) NuGet package:
</IntegrationIcon>

<InstallPackage PackageName="Aspire.StackExchange.Redis" />

## Add Valkey client

In the `Program.cs` file of your client-consuming project, call the `AddRedisClient` extension method to register an `IConnectionMultiplexer`:

```csharp
builder.AddRedisClient(connectionName: "cache");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Valkey
>   resource in the AppHost project.

You can then retrieve the `IConnectionMultiplexer` instance using dependency injection:

```csharp
public class ExampleService(IConnectionMultiplexer connectionMux)
{
    // Use connection multiplexer...
}
```

## Connection properties

When you reference a Valkey resource using `WithReference`, the following connection properties are made available to the consuming project:

### Valkey

The Valkey resource exposes the following connection properties:

| Property Name | Description                                                              |
| ------------- | ------------------------------------------------------------------------ |
| `Host`        | The hostname or IP address of the Valkey server                          |
| `Port`        | The port number the Valkey server is listening on                        |
| `Password`    | The password for authentication                                          |
| `Uri`         | The connection URI, with the format `valkey://:{Password}@{Host}:{Port}` |

**Example connection string:**

```
Uri: valkey://:p%40ssw0rd1@localhost:6379
```

> [!NOTE]
> Aspire exposes each property as an environment variable named
>   `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called
>   `cache` becomes `CACHE_URI`.

## Configuration and other client features

Since Valkey is Redis-compatible, all Redis client configuration, keyed services, health checks, and observability features work the same way. See the [Redis client integration](/integrations/caching/redis/redis-client) page for complete client integration documentation.

## Distributed caching and output caching

Valkey also supports Redis distributed caching and output caching. Install the respective packages:

- For distributed caching: [📦 Aspire.StackExchange.Redis.DistributedCaching](https://www.nuget.org/packages/Aspire.StackExchange.Redis.DistributedCaching)
- For output caching: [📦 Aspire.StackExchange.Redis.OutputCaching](https://www.nuget.org/packages/Aspire.StackExchange.Redis.OutputCaching)

See [Redis Distributed Caching](/integrations/caching/redis-distributed/redis-distributed-get-started) and [Redis Output Caching](/integrations/caching/redis-output/redis-output-get-started) for usage details.
