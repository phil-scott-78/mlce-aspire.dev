---
title: Get started with the Azure Functions integration
description: Learn how to set up the Aspire Azure Functions Hosting integration simply.
order: 213
---



<IntegrationIcon Src="/assets/icons/azure-functionapps-icon.png" Alt="Azure Functions logo">

[Azure Functions](https://learn.microsoft.com/azure-functions) is a serverless compute service that lets you run event-triggered code without having to explicitly provision or manage infrastructure. The Aspire Azure Functions integration enables you to model Azure Functions projects as part of your distributed application.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure Functions integration in a simple configuration. If you already have this knowledge, see [Azure Functions Hosting integration](/integrations/cloud/azure/azure-functions/azure-functions-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

It's expected that you've installed the required Azure tooling:

## Supported scenarios

The Aspire Azure Functions integration has several key supported scenarios.

### Supported triggers

The following table lists the supported triggers for Azure Functions in the Aspire integration:

| Trigger | Attribute | Details |
|--|--|--|
| Azure Event Hubs trigger | `EventHubTrigger` | [📦 Aspire.Hosting.Azure.EventHubs](https://www.nuget.org/packages/Aspire.Hosting.Azure.EventHubs) |
| Azure Service Bus trigger | `ServiceBusTrigger` | [📦 Aspire.Hosting.Azure.ServiceBus](https://www.nuget.org/packages/Aspire.Hosting.Azure.ServiceBus) |
| Azure Storage Blobs trigger | `BlobTrigger` | [📦 Aspire.Hosting.Azure.Storage](https://www.nuget.org/packages/Aspire.Hosting.Azure.Storage) |
| Azure Storage Queues trigger | `QueueTrigger` | [📦 Aspire.Hosting.Azure.Storage](https://www.nuget.org/packages/Aspire.Hosting.Azure.Storage) |
| Azure CosmosDB trigger | `CosmosDbTrigger` | [📦 Aspire.Hosting.Azure.CosmosDB](https://www.nuget.org/packages/Aspire.Hosting.Azure.CosmosDB) |
| HTTP trigger | `HttpTrigger` | Supported without any additional resource dependencies. |
| Timer trigger | `TimerTrigger` | Supported without any additional resource dependencies—relies on implicit host storage. |

> [!IMPORTANT]
> Other Azure Functions triggers and bindings aren't currently supported in the Aspire Azure Functions integration.

For more information about the Azure Functions integration with Aspire, see [Azure Functions and Aspire integration](https://learn.microsoft.com/azure/azure-functions/dotnet-aspire-integration).

## Set up hosting integration

To begin, install the Aspire Azure Functions Hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Azure.Functions" />

Next, in the AppHost project, add an Azure Functions resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var functions = builder.AddAzureFunctionsProject<Projects.ExampleFunctions>("functions")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.ExampleProject>()
    .WithReference(functions)
    .WaitFor(functions);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure Functions project resource named `functions` and makes it available to other projects in your distributed application.

## Continue learning

For more information on how the Azure Functions integration works, see:

<CardGrid>
    <LinkCard Href="/integrations/cloud/azure/azure-functions/azure-functions-host"
        Title="Hosting integration">Learn more about the Hosting integration</LinkCard>
</CardGrid>
