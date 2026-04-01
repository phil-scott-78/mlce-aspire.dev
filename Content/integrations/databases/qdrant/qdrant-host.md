---
title: Qdrant Hosting integration reference
description: Learn how to use the Aspire Qdrant Hosting integration to orchestrate and configure Qdrant vector databases in your Aspire projects.
order: 318
---



<IntegrationIcon Src="/assets/icons/qdrant-icon.svg" Alt="Qdrant logo">

To get started with the Aspire Qdrant integrations, follow the [Get started with Qdrant integrations](/integrations/databases/qdrant/qdrant-get-started) guide.
</IntegrationIcon>

This article includes full details about the Aspire Qdrant Hosting integration, which models Qdrant server resources as the `QdrantServerResource` type.

## Installation

The Aspire Qdrant hosting integration models the Qdrant vector database server as the `QdrantServerResource` type.

To access these types and APIs for expressing them as resources in your AppHost project, install the [📦 Aspire.Hosting.Qdrant](https://www.nuget.org/packages/Aspire.Hosting.Qdrant) NuGet package:

<InstallPackage PackageName="Aspire.Hosting.Qdrant" />

## Add Qdrant resource

In the AppHost project, call `AddQdrant` to add and return a Qdrant server resource builder:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var qdrant = builder.AddQdrant("qdrant")
                    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(qdrant)
       .WaitFor(qdrant);

// After adding all resources, run the app...
```

> [!NOTE]
> The Qdrant container can be slow to start, so it's best to use a persistent lifetime to avoid unnecessary restarts. For more information, see [Container resource lifetime](/docs/architecture/resource-model/#built-in-resources-and-lifecycle).

When Aspire adds a container image to the AppHost, as shown in the preceding example with the `qdrant/qdrant` image, it creates a new Qdrant server on your local machine. A reference to your Qdrant resource builder (the `qdrant` variable) is used to connect your application.

The `WithReference` method configures a connection in the `ExampleProject` named `"qdrant"`. For more information, see [Container resource lifecycle](/docs/architecture/resource-model/#built-in-resources-and-lifecycle).

> [!TIP]
> If you'd rather connect to an existing Qdrant server, call `AddConnectionString` instead. For more information, see [Reference existing resources](/docs/fundamentals/core-concepts/resources).

## Add Qdrant resource with API key parameter

The Qdrant resource includes a default, randomly generated API key. Qdrant supports configuration-based API keys. When you want to provide an API key explicitly, you can provide it as a parameter:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var apiKey = builder.AddParameter("apiKey", secret: true);

var qdrant = builder.AddQdrant("qdrant", apiKey)
                    .WithLifetime(ContainerLifetime.Persistent);

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(qdrant)
                       .WaitFor(qdrant);
```

The preceding code gets a parameter to pass to the `AddQdrant` API, and internally assigns the parameter to the Qdrant container configuration. The `apiKey` parameter is usually specified as a user secret:

```json title="JSON — secrets.json"
{
  "Parameters": {
    "apiKey": "your-secure-api-key"
  }
}
```

For more information, see [External parameters](/docs/fundamentals/core-concepts/resources).

## Add Qdrant resource with data volume

To add a data volume to the Qdrant resource, call the `WithDataVolume` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var qdrant = builder.AddQdrant("qdrant")
                    .WithDataVolume()
                    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(qdrant)
       .WaitFor(qdrant);

// After adding all resources, run the app...
```

The data volume is used to persist the Qdrant data outside the lifecycle of its container. The data volume is mounted at the `/qdrant/storage` path in the Qdrant container and when a `name` parameter isn't provided, the name is generated at random. For more information on data volumes and details on why they're preferred over [bind mounts](#add-qdrant-resource-with-data-bind-mount), see [Docker docs: Volumes](https://docs.docker.com/engine/storage/volumes).

## Add Qdrant resource with data bind mount

To add a data bind mount to the Qdrant resource, call the `WithDataBindMount` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var qdrant = builder.AddQdrant("qdrant")
                    .WithDataBindMount(source: @"C:\Qdrant\Data")
                    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(qdrant)
       .WaitFor(qdrant);

// After adding all resources, run the app...
```

> [!NOTE]
> Data [bind mounts](https://docs.docker.com/engine/storage/bind-mounts/) have limited functionality compared to [volumes](https://docs.docker.com/engine/storage/volumes/), which offer better performance, portability, and security, making them more suitable for production environments. However, bind mounts allow direct access and modification of files on the host system, ideal for development and testing where real-time changes are needed.
> 
> Data bind mounts rely on the host machine's filesystem to persist the Qdrant data across container restarts. The data bind mount is mounted at the `C:\Qdrant\Data` on Windows (or `/Qdrant/Data` on Unix) path on the host machine in the Qdrant container. For more information on data bind mounts, see [Docker docs: Bind mounts](https://docs.docker.com/engine/storage/bind-mounts).

## Pass connection information to app resources

When you use the `WithReference` method to pass a Qdrant resource to an app resource such as a Python or JavaScript app, Aspire automatically injects environment variables that describe the connection information.

For example, if you reference a Qdrant resource named `qdrant`:

```csharp title="C# — AppHost.cs"
var qdrant = builder.AddQdrant("qdrant");

var pythonApp = builder.AddUvicornApp("api", "./api", "main:app")
    .WithReference(qdrant);
```

The following environment variables are available in the Python application:

- `QDRANT_GRPCHOST` - The gRPC hostname of the Qdrant server
- `QDRANT_GRPCPORT` - The gRPC port number
- `QDRANT_HTTPHOST` - The HTTP hostname of the Qdrant server
- `QDRANT_HTTPPORT` - The HTTP port number
- `QDRANT_APIKEY` - The API key for authentication
- `QDRANT_URI` - The gRPC connection URI
- `QDRANT_HTTPURI` - The HTTP connection URI

You can access these environment variables in your application code:

```python title="Python example"
from qdrant_client import QdrantClient

// Get connection properties
const httpHost = process.env.QDRANT_HTTPHOST;
const httpPort = process.env.QDRANT_HTTPPORT;
const apiKey = process.env.QDRANT_APIKEY;

// Create Qdrant client
const client = new QdrantClient({
  url: `http://${httpHost}:${httpPort}`,
  apiKey: apiKey
});
```

## Hosting integration health checks

The Qdrant hosting integration automatically adds a health check for the Qdrant resource. The health check verifies that the Qdrant server is running and that a connection can be established to it.
