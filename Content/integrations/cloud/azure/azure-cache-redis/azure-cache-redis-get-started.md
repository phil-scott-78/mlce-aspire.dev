---
title: Get started with the Azure Cache for Redis integrations
description: Learn how to set up the Aspire Azure Cache for Redis hosting and client integrations simply.
order: 200
---



<IntegrationIcon Src="/assets/icons/azure-cacheredis-icon.png" Alt="Azure Cache for Redis logo">

[Azure Cache for Redis](https://learn.microsoft.com/azure/azure-cache-for-redis/cache-overview) provides an in-memory data store based on the [Redis](https://redis.io/) software. Redis improves the performance and scalability of an application that uses backend data stores heavily. It's able to process large volumes of application requests by keeping frequently accessed data in the server memory, which can be written to and read from quickly.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure Cache for Redis integrations in a simple configuration. If you already have this knowledge, see [Azure Cache for Redis Hosting integration](/integrations/cloud/azure/azure-cache-redis/azure-cache-redis-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure Cache for Redis Hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Azure.Redis" />

Next, in the AppHost project, create an Azure Cache for Redis resource and pass it to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddAzureManagedRedis("redis");

builder.AddProject<Projects.ExampleProject>()
    .WithReference(redis);

// After adding all resources, run the app...

builder.Build().Run();
```

> [!CAUTION]
> When you call `AddAzureManagedRedis`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

## Set up client integration

To get started with the Aspire Azure Cache for Redis client integration, install the [📦 Aspire.StackExchange.Redis](https://www.nuget.org/packages/Aspire.StackExchange.Redis) NuGet package in the client-consuming project:

<InstallPackage PackageName="Aspire.StackExchange.Redis" />

In the `Program.cs` file of your client-consuming project, call the `AddRedisClient` extension method to register an `IConnectionMultiplexer`:

```csharp title="C# — Program.cs"
builder.AddRedisClient(connectionName: "redis");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Redis resource in the AppHost project.

### Use injected Azure Cache for Redis properties

In the AppHost, when you used the `WithReference` method to pass an Azure Cache for Redis resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Host` property of a resource called `redis` becomes `REDIS_HOST`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# — Obtain configuration properties"
string redisHost = builder.Configuration.GetValue<string>("REDIS_HOST");
string redisPort = builder.Configuration.GetValue<string>("REDIS_PORT");
string redisUri = builder.Configuration.GetValue<string>("REDIS_URI");
```

> [!TIP]
> The full set of properties that Aspire injects is available in the client integration documentation. For more information, see [Azure Cache for Redis Client integration](/integrations/cloud/azure/azure-cache-redis/azure-cache-redis-client).

## Use Azure Cache for Redis resources in client code

After adding the `IConnectionMultiplexer`, you can retrieve the connection instance using dependency injection:

```csharp title="C# — ExampleService.cs"
public class ExampleService(IConnectionMultiplexer redis)
{
    // Use redis...
}
```

For more information, see [StackExchange.Redis documentation](https://stackexchange.github.io/StackExchange.Redis/) for examples on using the `IConnectionMultiplexer`.

## See also

<CardGrid>
  <LinkCard Title="Azure Cache for Redis Hosting integration"
    Href="/integrations/cloud/azure/azure-cache-redis/azure-cache-redis-host">Learn about Azure Cache for Redis hosting integration, including provisioning, customization, and configuration.</LinkCard>
  <LinkCard Title="Azure Cache for Redis Client integration"
    Href="/integrations/cloud/azure/azure-cache-redis/azure-cache-redis-client">Learn about Azure Cache for Redis client integration, including distributed cache, output cache, and telemetry.</LinkCard>
</CardGrid>
