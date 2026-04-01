---
title: Get started with the Ollama integrations
description: Learn how to set up the Aspire Ollama hosting and client integrations simply.
order: 166
---



<IntegrationIcon Src="/assets/icons/ollama-icon.png" Alt="Ollama logo">
<Badge text="⭐ Community Toolkit" size="small" />

[Ollama](https://ollama.com) is a powerful, open source language model that can be used to generate text based on a given prompt. The Aspire Ollama integration provides a way to host Ollama models using the [`docker.io/ollama/ollama` container image](https://hub.docker.com/r/ollama/ollama) and access them via the [OllamaSharp](https://www.nuget.org/packages/OllamaSharp) client.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Ollama integrations in a simple configuration. If you already have this knowledge, see [Ollama hosting integration](/integrations/ai/ollama/ollama-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Ollama Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Ollama model instances from your Aspire hosting projects:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.Ollama" />

Next, in the AppHost project, register and consume the Ollama integration using the `AddOllama` extension method to add the Ollama container to the application builder. You can then add models to the container, which download and run when the container starts, using the `AddModel` extension method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama");

var phi35 = ollama.AddModel("phi3.5");

var exampleProject = builder.AddProject<Projects.ExampleProject>()
                            .WithReference(phi35);

builder.Build().Run();
```

> [!TIP]
> This is the simplest implementation of Ollama resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Ollama hosting integration](/integrations/ai/ollama/ollama-host).

## Set up client integration

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

Install the Aspire OllamaSharp client integration in the client-consuming project:

<InstallPackage PackageName="CommunityToolkit.Aspire.OllamaSharp" />

In the `Program.cs` file of your client-consuming project, call the `AddOllamaApiClient` extension to register an `IOllamaApiClient` for use via the dependency injection container:

```csharp
builder.AddOllamaApiClient("llama3");
```

After adding `IOllamaApiClient` to the builder, you can get the `IOllamaApiClient` instance using dependency injection. For example, to retrieve your context object from service:

```csharp
public class ExampleService(IOllamaApiClient ollama)
{
    // Use ollama...
}
```

For full reference details, see [Ollama hosting integration](/integrations/ai/ollama/ollama-host) and [Ollama client integration](/integrations/ai/ollama/ollama-client).

## See also

- [Ollama](https://ollama.com)
- [Open WebUI](https://openwebui.com)
- [Aspire Community Toolkit GitHub repo](https://github.com/CommunityToolkit/Aspire)
- [OllamaSharp](https://github.com/awaescher/OllamaSharp)
- [Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/)
