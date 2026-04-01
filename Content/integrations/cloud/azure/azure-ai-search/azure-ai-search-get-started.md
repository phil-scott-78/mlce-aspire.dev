---
title: Get started with the Azure AI Search integration
description: Learn how to set up the Aspire Azure AI Search hosting and client integrations simply.
order: 187
---



<IntegrationIcon Src="/assets/icons/azure-search-icon.png" Alt="Azure AI Search logo">

[Azure AI Search](https://learn.microsoft.com/azure/search) is an enterprise-ready information retrieval system for your heterogeneous content that you ingest into a search index, and surface to users through queries and apps. It comes with a comprehensive set of advanced search technologies, built for high-performance applications at any scale. The Aspire Azure AI Search integration enables you to connect to Azure AI Search services from your applications.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure AI Search integrations in a simple configuration. If you already have this knowledge, see [Azure AI Search Hosting integration](/integrations/cloud/azure/azure-ai-search/azure-ai-search-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure AI Search Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure AI Search resources from your Aspire hosting projects:

<InstallPackage PackageName="Aspire.Hosting.Azure.Search" />

Next, in the AppHost project, create an Azure AI Search resource and pass it to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var search = builder.AddAzureSearch("search");

builder.AddProject<Projects.ExampleProject>()
    .WithReference(search);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure AI Search resource named `search` to the AppHost project. The `WithReference` method passes the connection information to the `ExampleProject` project.

> [!CAUTION]
> When you call `AddAzureSearch`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure AI Search resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure AI Search Hosting integration](/integrations/cloud/azure/azure-ai-search/azure-ai-search-host).

## Set up client integration

To use Azure AI Search from your client applications, install the Aspire Azure AI Search client integration in your client project:

<InstallPackage PackageName="Aspire.Azure.Search.Documents" />

In the `Program.cs` file of your client-consuming project, call the `AddAzureSearchClient` extension method to register a `SearchIndexClient` for use via the dependency injection container:

```csharp
builder.AddAzureSearchClient(connectionName: "search");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Azure AI Search resource in the AppHost project.

### Use injected Azure AI Search properties

In the AppHost, when you used the `WithReference` method to pass an Azure AI Search resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `search` becomes `SEARCH_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# — Obtain configuration properties"
string searchUri = builder.Configuration.GetValue<string>("SEARCH_URI");
```

> [!TIP]
> The full set of properties that Aspire injects is available in the client integration documentation. For more information, see [Properties of the Azure AI Search resources](/integrations/cloud/azure/azure-ai-search/azure-ai-search-client/#properties-of-the-azure-ai-search-resources).

For full details on using the client integration, see [Azure AI Search Client integration](/integrations/cloud/azure/azure-ai-search/azure-ai-search-client).

## Use Azure AI Search resources in client code

After adding the `SearchIndexClient`, you can retrieve the client instance using dependency injection:

```csharp
public class ExampleService(SearchIndexClient indexClient)
{
    // Use indexClient
}
```

## Next steps

<CardGrid>
    <LinkCard Title="Azure AI Search Hosting integration"
        
        Href="/integrations/cloud/azure/azure-ai-search/azure-ai-search-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure AI Search Client integration"
        
        Href="/integrations/cloud/azure/azure-ai-search/azure-ai-search-client">Learn how to use the Azure AI Search client integration</LinkCard>
</CardGrid>
