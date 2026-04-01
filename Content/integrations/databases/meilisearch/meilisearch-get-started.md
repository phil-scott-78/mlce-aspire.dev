---
title: Get started with the Meilisearch integrations
description: Learn how to set up the Aspire Meilisearch hosting and client integrations simply.
order: 298
---



<IntegrationIcon Src="/assets/icons/meilisearch-icon.png" Alt="Meilisearch logo">
<Badge text="⭐ Community Toolkit" size="small" />

[Meilisearch](https://meilisearch.com) is a fast, open-source search engine that makes search and discovery easy. The Aspire Meilisearch integration enables you to connect to existing Meilisearch instances or create new instances from Aspire using the `docker.io/getmeili/meilisearch` container image.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Meilisearch integrations in a simple configuration. If you already have this knowledge, see [Meilisearch hosting integration](/integrations/databases/meilisearch/meilisearch-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Meilisearch Hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.Meilisearch" />

Next, in the AppHost project, register and consume the Meilisearch integration using the `AddMeilisearch` extension method to add the Meilisearch container to the application builder:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var meilisearch = builder.AddMeilisearch("meilisearch");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(meilisearch);

// After adding all resources, run the app...
```

## Set up client integration

To get started with the Aspire Meilisearch client integration, install the NuGet package in the client-consuming project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Meilisearch" />

In the `Program.cs` file of your client-consuming project, call the `AddMeilisearchClient` extension method to register a `MeilisearchClient` for use via the dependency injection container:

```csharp
builder.AddMeilisearchClient(connectionName: "meilisearch");
```

You can then retrieve the `MeilisearchClient` instance using dependency injection. For example, to retrieve the client from a service:

```csharp
public class ExampleService(MeilisearchClient client)
{
    // Use client...
}
```

For full reference details, see [Meilisearch hosting integration](/integrations/databases/meilisearch/meilisearch-host) and [Meilisearch client integration](/integrations/databases/meilisearch/meilisearch-client).

## See also

- [Meilisearch](https://meilisearch.com)
- [Meilisearch Client](https://github.com/meilisearch/meilisearch-dotnet)
- [Aspire Community Toolkit GitHub repo](https://github.com/CommunityToolkit/Aspire)
