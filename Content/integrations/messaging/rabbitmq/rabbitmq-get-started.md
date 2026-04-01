---
title: Get started with the RabbitMQ integration
description: Learn how to set up the Aspire RabbitMQ hosting and client integrations simply.
order: 357
---



<IntegrationIcon Src="/assets/icons/rabbitmq-icon.svg" Alt="RabbitMQ logo">

[RabbitMQ](https://www.rabbitmq.com/) is a reliable messaging and streaming broker, which is easy to deploy on cloud environments, on-premises, and on your local machine. The Aspire RabbitMQ integration enables you to connect to existing RabbitMQ instances, or create new instances from the [`docker.io/library/rabbitmq` container image](https://hub.docker.com/_/rabbitmq).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire RabbitMQ integrations in a simple configuration. If you already have this knowledge, see [RabbitMQ hosting integration](/integrations/messaging/rabbitmq/rabbitmq-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire RabbitMQ hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.RabbitMQ" />

Next, in the AppHost project, create instances of RabbitMQ resources and pass them to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("messaging");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(rabbitmq);

builder.Build().Run();
```

When Aspire adds a container image to the app host, it creates a new RabbitMQ server instance on your local machine. The RabbitMQ server resource includes default credentials with a `username` of `"guest"` and randomly generated `password`.

> [!TIP]
> If you'd rather connect to an existing RabbitMQ server, chain a call to `AsExisting` instead—passing the appropriate parameters.

## Set up client integration

To get started with the Aspire RabbitMQ client integration, install the package:

<InstallPackage PackageName="Aspire.RabbitMQ.Client" />

In the `Program.cs` file of your client-consuming project, call the `AddRabbitMQClient` extension method to register an `IConnection` for use via the dependency injection container:

```csharp
builder.AddRabbitMQClient(connectionName: "messaging");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the RabbitMQ server resource in the AppHost project.

You can then retrieve the `IConnection` instance using dependency injection:

```csharp
public class ExampleService(IConnection connection)
{
    // Use connection...
}
```

## See also

- [RabbitMQ hosting integration](/integrations/messaging/rabbitmq/rabbitmq-host)
- [RabbitMQ client integration](/integrations/messaging/rabbitmq/rabbitmq-client)
