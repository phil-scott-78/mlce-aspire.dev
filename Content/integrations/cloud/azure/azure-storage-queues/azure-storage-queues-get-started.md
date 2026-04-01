---
title: Getting started with the Azure Queue Storage integration
description: Learn how to set up the Aspire Azure Queue Storage hosting and client integrations simply.
order: 236
---



<IntegrationIcon Src="/assets/icons/azure-storagequeue-icon.png" Alt="Azure Queue Storage logo">

[Azure Queue Storage](https://azure.microsoft.com/services/storage/queues/) is a service for storing large numbers of messages. The Aspire Azure Queue Storage Hosting integration provides methods to create Azure Queue Storage resources from code in your Aspire AppHost project.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure Queue Storage integrations in a simple configuration. If you already have this knowledge, see [Azure Queue Storage Hosting integration](/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure Storage Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure Queue Storage resources from your Aspire hosting projects:

<InstallPackage PackageName="Aspire.Hosting.Azure.Storage" />

Next, in the AppHost project, create an Azure Queue Storage resource and pass it to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var queues = builder.AddAzureStorage("storage")
    .AddQueues("queues");

var myService = builder.AddProject<Projects.MyService>()
    .WithReference(queues);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure Storage resource named `storage` to the AppHost project, adds a queue storage resource to it named `queues`, and passes the queue storage connection information to the consuming project.

> [!CAUTION]
> When you call `AddAzureStorage`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure Queue Storage resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure Queue Storage Hosting integration](/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-host).

## Set up client integration

To use Azure Queue Storage from your client applications, install the Aspire Azure Queue Storage client integration in your client project:

<InstallPackage PackageName="Aspire.Azure.Storage.Queues" />

In the `Program.cs` file of your client-consuming project, call the `AddAzureQueueServiceClient` extension method to register a `QueueServiceClient` for use via the dependency injection container:

```csharp
builder.AddAzureQueueServiceClient(connectionName: "queues");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the queue storage resource in the AppHost project.

### Use injected Azure Queue Storage properties

In the AppHost, when you used the `WithReference` method to pass an Azure Queue Storage resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `queues` becomes `QUEUES_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# — Obtain configuration properties"
string queueUri = builder.Configuration.GetValue<string>("QUEUES_URI");
```

> [!TIP]
> The full set of properties that Aspire injects depends on the Azure Queue Storage resource configuration. For more information, see [Properties of the Azure Queue Storage resources](/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-client/#properties-of-the-azure-queue-storage-resources).

## Add Azure Queue Storage resources in client code

After adding the `QueueServiceClient`, you can retrieve the connection instance using dependency injection:

```csharp
public class ExampleService(QueueServiceClient client)
{
    // Use client...
}
```

For full details on using the client integration, see [Azure Queue Storage Client integration](/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-client).

## Next steps

<CardGrid>
    <LinkCard Title="Azure Queue Storage Hosting integration"
        
        Href="/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure Queue Storage Client integration"
        
        Href="/integrations/cloud/azure/azure-storage-queues/azure-storage-queues-client">Learn how to use the Azure Queue Storage client integration</LinkCard>
</CardGrid>
