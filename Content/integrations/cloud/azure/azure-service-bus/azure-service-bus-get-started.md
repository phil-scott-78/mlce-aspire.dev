---
title: Get started with the Azure Service Bus integration
description: Learn how to set up the Aspire Azure Service Bus hosting and client integrations simply.
order: 223
---



<IntegrationIcon Src="/assets/icons/azure-servicebus-icon.png" Alt="Azure Service Bus logo">

[Azure Service Bus](https://learn.microsoft.com/azure/service-bus-messaging) is a fully managed enterprise message broker with message queues and publish-subscribe topics. The Aspire Azure Service Bus integration enables you to connect to Azure Service Bus instances from your applications.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure Service Bus integrations in a simple configuration. If you already have this knowledge, see [Azure Service Bus Hosting integration](/integrations/cloud/azure/azure-service-bus/azure-service-bus-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure Service Bus Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure Service Bus resources from your Aspire hosting projects:

<InstallPackage PackageName="Aspire.Hosting.Azure.ServiceBus" />

Next, in the AppHost project, create an Azure Service Bus resource and pass it to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var serviceBus = builder.AddAzureServiceBus("messaging");
var queue = serviceBus.AddServiceBusQueue("queue");

builder.AddProject<Projects.ExampleProject>()
    .WithReference(queue);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure Service Bus resource named `messaging` to the AppHost project, adds a queue to it, and passes the queue connection information to the `ExampleProject` project.

> [!CAUTION]
> When you call `AddAzureServiceBus`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure Service Bus resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure Service Bus Hosting integration](/integrations/cloud/azure/azure-service-bus/azure-service-bus-host).

## Set up client integration

To use Azure Service Bus from your client applications, install the Aspire Azure Service Bus client integration in your client project:

<InstallPackage PackageName="Aspire.Azure.Messaging.ServiceBus" />

In the `Program.cs` file of your client-consuming project, call the `AddAzureServiceBusClient` extension method to register a `ServiceBusClient` for use via the dependency injection container:

```csharp
builder.AddAzureServiceBusClient(connectionName: "queue");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Service Bus queue (or topic) in the AppHost project.

### Use injected Azure Service Bus properties

In the AppHost, when you used the `WithReference` method to pass an Azure Service Bus resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `queue` becomes `QUEUE_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# — Obtain configuration properties"
string serviceBusHost = builder.Configuration.GetValue<string>("QUEUE_HOST");
string serviceBusUri = builder.Configuration.GetValue<string>("QUEUE_URI");
string queueName = builder.Configuration.GetValue<string>("QUEUE_QUEUENAME");
```

> [!TIP]
> The full set of properties that Aspire injects depends on whether you passed an Azure Service Bus namespace, queue, topic, or subscription resource. For more information, see [Properties of the Azure Service Bus resources](/integrations/cloud/azure/azure-service-bus/azure-service-bus-client/#properties-of-the-azure-service-bus-resources).

## Use Azure Service Bus resources in client code

After adding the `ServiceBusClient`, you can retrieve the client instance using dependency injection:

```csharp
public class ExampleService(ServiceBusClient client)
{
    // Use client...
}
```

For full details on using the client integration, see [Azure Service Bus Client integration](/integrations/cloud/azure/azure-service-bus/azure-service-bus-client).

## Next steps

<CardGrid>
    <LinkCard Title="Azure Service Bus Hosting integration"
        
        Href="/integrations/cloud/azure/azure-service-bus/azure-service-bus-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure Service Bus Client integration"
        
        Href="/integrations/cloud/azure/azure-service-bus/azure-service-bus-client">Learn how to use the Azure Service Bus client integration</LinkCard>
</CardGrid>
