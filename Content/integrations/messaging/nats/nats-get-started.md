---
title: Get started with the NATS integration
description: Learn how to set up the Aspire NATS hosting and client integrations simply.
order: 354
---



<IntegrationIcon Src="/assets/icons/nats-icon.png" Alt="NATS logo">

[NATS](https://nats.io) is a simple, secure and high-performance open-source messaging system for cloud-native applications, IoT messaging, and microservices architectures. The Aspire NATS integration provides a way to connect to existing NATS instances or create new instances from Aspire with the `docker.io/library/nats` container image.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire NATS integrations in a simple configuration. If you already have this knowledge, see [NATS hosting integration](/integrations/messaging/nats/nats-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire NATS hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Nats" />

Next, in the AppHost project, create instances of NATS resources and pass them to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var nats = builder.AddNats("nats");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(nats);

builder.Build().Run();
```

## Set up client integration

To get started with the Aspire NATS client integration, install the package:

<InstallPackage PackageName="Aspire.NATS.Net" />

In the `Program.cs` file of your client-consuming project, call the `AddNatsClient` extension method to register an `INatsConnection` for use through dependency injection:

```csharp
builder.AddNatsClient(connectionName: "nats");
```

You can then retrieve the `INatsConnection` instance using dependency injection:

```csharp
public class ExampleService(INatsConnection connection)
{
    // Use connection...
}
```

## See also

- [NATS hosting integration](/integrations/messaging/nats/nats-host)
- [NATS client integration](/integrations/messaging/nats/nats-client)
- [NATS documentation](https://docs.nats.io)
