---
title: Get started with the Azure AI Inference integrations
description: Learn how to set up the Aspire Azure AI Inference hosting and client integrations simply.
order: 184
---



<IntegrationIcon Src="/assets/icons/azure-ai-foundry-icon.png" Alt="Azure AI Inference logo">

<Badge text="🧪 Preview" variant="note" size="large" />
</IntegrationIcon>

[Azure AI Inference](https://learn.microsoft.com/azure/ai-foundry/how-to/deploy-models-serverless?view=foundry-classic) provides serverless API endpoints for deploying and using AI models. The Aspire Azure AI Inference integration enables you to connect to Azure AI Inference services from your applications, making it easy to call models for chat, completions, embeddings, and more.

In this introduction, you'll see how to install and use the Aspire Azure AI Inference integrations in a simple configuration. If you already have this knowledge, see [Azure AI Inference Hosting integration](/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

Although the Azure AI Inference library doesn't currently offer direct hosting integration, you can still integrate it into your AppHost project. Simply add a connection string to establish a reference to an existing Microsoft Foundry resource.

If you already have a [Microsoft Foundry](https://ai.azure.com/) service, you can easily connect to it by adding a connection string to your AppHost:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var aiFoundry = builder.AddConnectionString("ai-foundry");

builder.AddProject<Projects.ExampleProject>()
    .WithReference(aiFoundry);

// After adding all resources, run the app...

builder.Build().Run();
```

The connection string is configured in the AppHost's configuration, typically under User Secrets, under the `ConnectionStrings` section:

```json
{
  "ConnectionStrings": {
    "ai-foundry": "Endpoint=https://{endpoint}/;DeploymentId={deploymentName}"
  }
}
```

> [!TIP]
> This is the simplest implementation of Azure AI Inference resources in the AppHost. For full details, see [Azure AI Inference Hosting integration](/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-host).

## Set up client integration

To use Azure AI Inference from your client applications, install the [📦 Aspire.Azure.AI.Inference](https://www.nuget.org/packages/Aspire.Azure.AI.Inference) NuGet package in the client-consuming project:

<InstallPackage PackageName="Aspire.Azure.AI.Inference" />

In the _Program.cs_ file of your client-consuming project, add the Azure AI Inference Chat Completions client:

```csharp
builder.AddAzureChatCompletionsClient(connectionName: "ai-foundry")
    .AddChatClient("deploymentName");
```

After adding the `IChatClient`, you can retrieve the client instance using dependency injection:

```csharp
public class ExampleService(IChatClient chatClient)
{
    public async Task<string> GetResponseAsync(string userMessage)
    {
        var response = await chatClient.CompleteAsync(userMessage);
        return response.Message.Text ?? string.Empty;
    }
}
```

For more information on using the client integration, see [Azure AI Inference Client integration](/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-client).

## Next steps

<CardGrid>
    <LinkCard Title="Azure AI Inference Hosting integration"
        
        Href="/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure AI Inference Client integration"
        
        Href="/integrations/cloud/azure/azure-ai-inference/azure-ai-inference-client">Learn how to use the Azure AI Inference client integration</LinkCard>
</CardGrid>
