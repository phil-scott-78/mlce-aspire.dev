---
title: RabbitMQ hosting integration
description: Learn how to use the RabbitMQ hosting integration to add a RabbitMQ resource to your AppHost.
order: 358
---



<IntegrationIcon Src="/assets/icons/rabbitmq-icon.svg" Alt="RabbitMQ logo">

The RabbitMQ hosting integration models a RabbitMQ server as the `RabbitMQServerResource` type. To access this type and its APIs add the [📦 Aspire.Hosting.RabbitMQ](https://www.nuget.org/packages/Aspire.Hosting.RabbitMQ) NuGet package in the [`AppHost`](/docs/fundamentals/core-concepts/app-host) project.
</IntegrationIcon>

<InstallPackage PackageName="Aspire.Hosting.RabbitMQ" />

## Add RabbitMQ server resource

In your AppHost project, call `AddRabbitMQ` on the `builder` instance to add a RabbitMQ server resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("messaging");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(rabbitmq);

// After adding all resources, run the app...
```


<Steps>
<Step stepNumber="1">
When Aspire adds a container image to the app host, as shown in the preceding example with the `docker.io/library/rabbitmq` image, it creates a new RabbitMQ server instance on your local machine. A reference to your RabbitMQ server (the `rabbitmq` variable) is added to the `ExampleProject`.

</Step>
<Step stepNumber="2">
The RabbitMQ server resource includes default credentials with a `username` of `"guest"` and randomly generated `password` using the `CreateDefaultPasswordParameter` method.

</Step>
<Step stepNumber="3">
The `WithReference` method configures a connection in the `ExampleProject` named `"messaging"`.

</Step>
</Steps>

> [!TIP]
> If you'd rather connect to an existing RabbitMQ server, chain a call to `AsExisting` instead—passing the appropriate parameters.

## Add RabbitMQ server resource with data volume

To add a data volume to the RabbitMQ server resource, call the `WithDataVolume` method on the RabbitMQ server resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("messaging")
                      .WithDataVolume(isReadOnly: false);

builder.AddProject<Projects.ExampleProject>()
        .WithReference(rabbitmq);

// After adding all resources, run the app...
```

The data volume is used to persist the RabbitMQ server data outside the lifecycle of its container. The data volume is mounted at the `/var/lib/rabbitmq` path in the RabbitMQ server container and when a `name` parameter isn't provided, the name is generated at random. For more information on data volumes and details on why they're preferred over [bind mounts](#add-rabbitmq-server-resource-with-data-bind-mount), see [Docker docs: Volumes](https://docs.docker.com/engine/storage/volumes).

## Add RabbitMQ server resource with data bind mount

To add a data bind mount to the RabbitMQ server resource, call the `WithDataBindMount` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("messaging")
                      .WithDataBindMount(
                          source: @"/RabbitMQ/Data",
                          isReadOnly: false);

builder.AddProject<Projects.ExampleProject>()
        .WithReference(rabbitmq);

// After adding all resources, run the app...
```

> [!NOTE]
> Data [bind mounts](https://docs.docker.com/engine/storage/bind-mounts/) have limited functionality compared to [volumes](https://docs.docker.com/engine/storage/volumes/), which offer better performance, portability, and security, making them more suitable for production environments. However, bind mounts allow direct access and modification of files on the host system, ideal for development and testing where real-time changes are needed.

Data bind mounts rely on the host machine's filesystem to persist the RabbitMQ server data across container restarts. The data bind mount is mounted at the `C:\RabbitMQ\Data` on Windows (or `/RabbitMQ/Data` on Unix) path on the host machine in the RabbitMQ server container. For more information on data bind mounts, see [Docker docs: Bind mounts](https://docs.docker.com/engine/storage/bind-mounts).

## Add RabbitMQ server resource with parameters

When you want to explicitly provide the username and password used by the container image, you can provide these credentials as parameters. Consider the following alternative example:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var rabbitmq = builder.AddRabbitMQ("messaging", username, password);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(rabbitmq);

// After adding all resources, run the app...
```

## Add RabbitMQ server resource with management plugin

To add the [RabbitMQ management plugin](https://www.rabbitmq.com/docs/management) to the RabbitMQ server resource, call the `WithManagementPlugin` method. Remember to use parameters to set the credentials for the container. You'll need these credentials to log into the management plugin:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var rabbitmq = builder.AddRabbitMQ("messaging", username, password)
                      .WithManagementPlugin();

builder.AddProject<Projects.ExampleProject>()
        .WithReference(rabbitmq);

// After adding all resources, run the app...
```

The RabbitMQ management plugin provides an HTTP-based API for management and monitoring of your RabbitMQ server. Aspire adds another container image [`docker.io/library/rabbitmq-management`](https://hub.docker.com/_/rabbitmq) to the AppHost that runs the management plugin. You can access the management plugin from the Aspire dashboard by selecting an endpoint for your RabbitMQ resource.

## Hosting integration health checks

The RabbitMQ hosting integration automatically adds a health check for the RabbitMQ server resource. The health check verifies that the RabbitMQ server is running and that a connection can be established to it.

The hosting integration relies on the [📦 AspNetCore.HealthChecks.Rabbitmq](https://www.nuget.org/packages/AspNetCore.HealthChecks.Rabbitmq) NuGet package.
