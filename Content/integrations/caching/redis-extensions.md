---
title: Redis hosting extensions
order: 252
---




<IntegrationIcon Src="/assets/icons/redis-icon.png" Alt="Redis logo">

<Badge text="⭐ Community Toolkit" size="small" />

The Aspire Community Toolkit Redis hosting extensions package provides extra functionality to the [Aspire.Hosting.Redis](https://www.nuget.org/packages/Aspire.Hosting.Redis) hosting package.
</IntegrationIcon>

This package provides the following features:

- [DbGate](https://dbgate.org/) management UI

## Hosting integration

To get started with the Aspire Community Toolkit Redis hosting extensions, install the [CommunityToolkit.Aspire.Hosting.Redis.Extensions](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.Redis.Extensions) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.Redis.Extensions" />

## Add management UI

### DbGate management UI

To add the DbGate management UI to your Redis resource, call the `WithDbGate` method on the `RedisResource` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis")
                   .WithDbGate();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(redis);

// After adding all resources, run the app...
```

This adds a new DbGate resource to the app host which is available from the Aspire dashboard. DbGate is a comprehensive database management tool that provides a web-based interface for managing your Redis databases.

## See also

- [DbGate documentation](https://dbgate.org/)
- [Redis integration](/integrations/caching/redis/redis-get-started)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
