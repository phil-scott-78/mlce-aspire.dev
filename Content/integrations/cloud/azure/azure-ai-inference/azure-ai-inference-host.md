---
title: Azure AI Inference hosting integration
description: Learn how to use the Aspire Azure AI Inference hosting integration to connect to Azure AI Inference resources.
order: 185
---



<IntegrationIcon Src="/assets/icons/azure-ai-foundry-icon.png" Alt="Azure AI Inference logo">

<Badge text="🧪 Preview" variant="note" size="large" />
</IntegrationIcon>

Although the Azure AI Inference library doesn't currently offer direct hosting integration, you can still integrate it into your AppHost project. Simply add a connection string to establish a reference to an existing Microsoft Foundry resource.

For an introduction to working with the Azure AI Inference hosting integration, see [Get started with the Azure AI Inference integrations](/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-get-started).

## Connect to an existing Microsoft Foundry service

If you already have a [Microsoft Foundry](https://ai.azure.com/) service, you can easily connect to it by adding a connection string to your AppHost. This approach uses a simple, string-based configuration. To establish the connection, use the `AddConnectionString` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var aiFoundry = builder.AddConnectionString("ai-foundry");

builder.AddProject<Projects.ExampleProject>()
    .WithReference(aiFoundry);

// After adding all resources, run the app...
```

> [!NOTE]
> Connection strings are used to represent a wide range of connection information, including database connections, message brokers, endpoint URIs, and other services. In nomenclature, the term "connection string" is used to represent any kind of connection information.

The connection string is configured in the AppHost's configuration, typically under User Secrets, under the `ConnectionStrings` section:

```json
{
  "ConnectionStrings": {
    "ai-foundry": "Endpoint=https://{endpoint}/;DeploymentId={deploymentName}"
  }
}
```

For more information, see [Add existing Azure resources with connection strings](/integrations/cloud/azure/overview/#add-existing-azure-resources-with-connection-strings).
