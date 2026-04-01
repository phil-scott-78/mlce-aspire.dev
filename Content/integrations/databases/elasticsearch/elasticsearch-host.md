---
title: Elasticsearch hosting integration
description: Learn how to use the Elasticsearch hosting integration to create and manage Elasticsearch instances.
order: 271
---



<IntegrationIcon Src="/assets/icons/elastic-icon.png" Alt="Elasticsearch logo">

The Elasticsearch hosting integration models an Elasticsearch instance as the `ElasticsearchResource` type. To access this type and APIs, add the [📦 Aspire.Hosting.Elasticsearch](https://www.nuget.org/packages/Aspire.Hosting.Elasticsearch) NuGet package in your AppHost project:
</IntegrationIcon>

<InstallPackage PackageName="Aspire.Hosting.Elasticsearch" />

For an introduction to the Elasticsearch integration, see [Get started with the Elasticsearch integrations](/integrations/databases/elasticsearch/elasticsearch-get-started).

## Add Elasticsearch resource

In your AppHost project, call `AddElasticsearch` on the builder instance to add an Elasticsearch resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var elasticsearch = builder.AddElasticsearch("elasticsearch");

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(elasticsearch);
```

When Aspire adds a container image to the AppHost, it creates a new Elasticsearch instance on your local machine. The Elasticsearch resource includes default credentials with a `username` of `"elastic"` and a randomly generated password.

> [!TIP]
> If you'd rather connect to an existing Elasticsearch instance, call
>   `AddConnectionString` instead. For more information, see [Reference existing
>   resources](/docs/fundamentals/core-concepts/resources).

## Add Elasticsearch resource with data volume

To add a data volume to the Elasticsearch resource, call the `WithDataVolume` method on the Elasticsearch resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var elasticsearch = builder.AddElasticsearch("elasticsearch")
                           .WithDataVolume(isReadOnly: false);

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(elasticsearch);
```

The data volume is used to persist the Elasticsearch data outside the lifecycle of its container. The data volume is mounted at the `/usr/share/elasticsearch/data` path in the Elasticsearch container and when a `name` parameter isn't provided, the name is generated at random. For more information on data volumes and details on why they're preferred over bind mounts, see [Docker docs: Volumes](https://docs.docker.com/engine/storage/volumes).

## Add Elasticsearch resource with data bind mount

To add a data bind mount to the Elasticsearch resource, call the `WithDataBindMount` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var elasticsearch = builder.AddElasticsearch("elasticsearch")
                           .WithDataBindMount(
                               source: @"C:\Elasticsearch\Data",
                               isReadOnly: false);

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(elasticsearch);
```

> [!NOTE]
> Data bind mounts have limited functionality compared to volumes, and when you
>   use a bind mount, a file or directory on the host machine is mounted into a
>   container.

Data bind mounts rely on the host machine's filesystem to persist the Elasticsearch data across container restarts. For more information on data bind mounts, see [Docker docs: Bind mounts](https://docs.docker.com/engine/storage/bind-mounts).

## Add Elasticsearch resource with password parameter

When you want to explicitly provide the password used by the container image, you can provide these credentials as parameters:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);
var elasticsearch = builder.AddElasticsearch("elasticsearch", password);

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(elasticsearch);
```

For more information on providing parameters, see [External parameters](/docs/fundamentals/core-concepts/resources).

## Hosting integration health checks

The Elasticsearch hosting integration automatically adds a health check for the Elasticsearch resource. The health check verifies that the Elasticsearch instance is running and that a connection can be established to it.

The hosting integration relies on the [📦 AspNetCore.HealthChecks.Elasticsearch](https://www.nuget.org/packages/AspNetCore.HealthChecks.Elasticsearch) NuGet package.

## Pass connection information to app resources

You can use the Elasticsearch hosting integration with app resources that consume environment variables. When you add a reference to an Elasticsearch resource from a Python or JavaScript app resource, Aspire injects connection properties into the consuming app's environment variables, which you can use to interact with Elasticsearch.

Consider the following example where a Python app is added to the Aspire app model and referenced to an Elasticsearch resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var elasticsearch = builder.AddElasticsearch("elasticsearch");

var pythonApp = builder.AddPythonApp("python-app", "./python_app", "main.py")
                       .WithReference(elasticsearch);
```

In the Python app, you can access the Elasticsearch connection information using the environment variable that Aspire injects. Here's an example of how to connect to Elasticsearch in Python using the official `elasticsearch` client:

```python title="Python - Connect to Elasticsearch"
from elasticsearch import Elasticsearch

// Get the Elasticsearch endpoint from environment variables
const elasticsearchEndpoint = process.env.ConnectionStrings__elasticsearch;

// Create Elasticsearch client
const client = new Client({
  node: elasticsearchEndpoint
});

// Use the client
const response = await client.ping();
console.log('Elasticsearch connection status:', response);
```
