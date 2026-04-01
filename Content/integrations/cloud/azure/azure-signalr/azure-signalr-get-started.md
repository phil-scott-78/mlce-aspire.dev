---
title: Get started with the Azure SignalR Service integration
description: Learn how to set up the Aspire Azure SignalR Service Hosting and Hub host integrations simply.
order: 226
---



<IntegrationIcon Src="/assets/icons/azure-signalr-icon.png" Alt="Azure SignalR Service icon">

[Azure SignalR Service](https://learn.microsoft.com/azure-signalr) is a fully managed real-time messaging service that simplifies adding real-time web functionality to applications. The Aspire Azure SignalR Service integration enables you to connect to Azure SignalR instances from your applications.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure SignalR Service integrations in a simple configuration. If you already have this knowledge, see [Azure SignalR Service Hosting integration](/integrations/cloud/azure/azure-signalr/azure-signalr-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure SignalR Service Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure SignalR Service resources from your Aspire hosting projects:

<InstallPackage PackageName="Aspire.Hosting.Azure.SignalR" />

Next, in the AppHost project, create an Azure SignalR Service resource and pass it to the consuming projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var signalR = builder.AddAzureSignalR("signalr");

var api = builder.AddProject<Projects.ApiService>("api")
    .WithReference(signalR)
    .WaitFor(signalR);

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(api)
    .WaitFor(api);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure SignalR Service resource named `signalr` to the AppHost project. The `api` project references the SignalR service and the `webapp` project references the API.

> [!CAUTION]
> When you call `AddAzureSignalR`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure SignalR Service resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure SignalR Service Hosting integration](/integrations/cloud/azure/azure-signalr/azure-signalr-host).

## Set up hub host integration

To use Azure SignalR Service from your hub host applications, install the SignalR package in your project:

<InstallPackage PackageName="Microsoft.Azure.SignalR" />

In the `Program.cs` file of your hub host project, configure Azure SignalR Service by chaining calls to `.AddSignalR().AddNamedAzureSignalR("name")`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR()
                .AddNamedAzureSignalR("signalr");

var app = builder.Build();

app.MapHub<ChatHub>("/chat");

app.Run();
```

> [!TIP]
> The name parameter must match the name used when adding the Azure SignalR Service resource in the AppHost project.

For full details on using the hub host integration, see [Azure SignalR Service Hub host integration](/integrations/cloud/azure/azure-signalr/azure-signalr-client).

## Next steps

<CardGrid>
    <LinkCard Title="Azure SignalR Service Hosting integration"
        
        Href="/integrations/cloud/azure/azure-signalr/azure-signalr-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure SignalR Service Hub host integration"
        
        Href="/integrations/cloud/azure/azure-signalr/azure-signalr-client">Learn how to use the Azure SignalR Service hub host integration</LinkCard>
</CardGrid>
