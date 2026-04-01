---
title: Azure SignalR Service hub host integration
description: Learn how to use the Azure SignalR Service to host SignalR hubs.
order: 228
---



<IntegrationIcon Src="/assets/icons/azure-signalr-icon.png" Alt="Azure SignalR Service icon">

There isn't an official Aspire Azure SignalR _client integration_. However, there is limited support for similar experiences. In these scenarios, the Azure SignalR Service acts as a proxy between the server (where the `Hub` or `Hub<T>` are hosted) and the client (where the SignalR client is hosted). The Azure SignalR Service routes traffic between the server and client, allowing for real-time communication.
</IntegrationIcon>

For an introduction to working with the Azure SignalR Service, see [Get started with the Azure SignalR Service integration](/integrations/cloud/azure/azure-signalr/azure-signalr-get-started).

> [!IMPORTANT]
> It's important to disambiguate between Aspire client integrations and the .NET SignalR client. SignalR exposes hubs—which act as a server-side concept—and SignalR clients connect to those hubs. The .NET projects that host SignalR hubs are where you integrate with Aspire. The SignalR client is a separate library that connects to those hubs, in a different project.

There are two packages available for, each with addressing specific scenarios such as managing the client connection to Azure SignalR Service, and hooking up to the Azure SignalR Service resource. To get started, install the [📦 Microsoft.Azure.SignalR](https://www.nuget.org/packages/Microsoft.Azure.SignalR) NuGet package in the project hosting your SignalR hub.

<InstallPackage PackageName="Microsoft.Azure.SignalR" />

### Configure named Azure SignalR Service in Default mode

In _Default_ mode, your consuming project needs to rely on a named Azure SignalR Service resource. Consider the following diagram that illustrates the architecture of Azure SignalR Service in _Default_ mode:

For more information on _Default_ mode, see [Azure SignalR Service: Default mode](https://learn.microsoft.com/azure/azure-signalr/concept-service-mode#default-mode).

In your SignalR hub host project, configure Azure SignalR Service by chaining calls to `.AddSignalR().AddNamedAzureSignalR("name")`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR()
                .AddNamedAzureSignalR("signalr");

var app = builder.Build();

app.MapHub<ChatHub>("/chat");

app.Run();
```

The `AddNamedAzureSignalR` method configures the project to use the Azure SignalR Service resource named `signalr`. The connection string is read from the configuration key `ConnectionStrings:signalr`, and additional settings are loaded from the `Azure:SignalR:signalr` configuration section.

> [!NOTE]
> If you're using the Azure SignalR emulator, you cannot use the `AddNamedAzureSignalR` method.

### Configure Azure SignalR Service in Serverless mode

If you're AppHost is using the Azure SignalR emulator, you'll also need to install the [📦 Microsoft.Azure.SignalR.Management](https://www.nuget.org/packages/Microsoft.Azure.SignalR.Management) NuGet package.

<InstallPackage PackageName="Microsoft.Azure.SignalR.Management" />

Azure SignalR _Serverless_ mode doesn't require a hub server to be running. The Azure SignalR Service is responsible for maintaining client connections. Additionally, in this mode, you cannot use traditional SignalR Hubs, such as `Microsoft.AspNetCore.SignalR.Hub`, `Microsoft.AspNetCore.SignalR.Hub<T>`, or `Microsoft.AspNetCore.SignalR.IHubContext<THub>`. Instead, [configure an upstream endpoint which is usually an Azure Function SignalR trigger](https://learn.microsoft.com/azure/azure-signalr/concept-upstream). Consider the following diagram that illustrates the architecture of Azure SignalR Service in _Serverless_ mode:

For more information on _Serverless_ mode, see [Azure SignalR Service: Serverless mode](https://learn.microsoft.com/azure/azure-signalr/concept-service-mode#serverless-mode).

In a project that's intended to communicate with the Azure SignalR Service, register the appropriate services by calling `AddSignalR` and then registering the `ServiceManager` using the `signalr` connection string and add a `/negotiate` endpoint:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
{
   return new ServiceManagerBuilder()
       .WithOptions(options =>
       {
           options.ConnectionString = builder.Configuration.GetConnectionString("signalr");
       })
       .BuildServiceManager();
});

var app = builder.Build();

app.MapPost("/negotiate", async (string? userId, ServiceManager sm, CancellationToken token) =>
{
    // The creation of the ServiceHubContext is expensive, so it's recommended to
    // only create it once per named context / per app run if possible.
    var context = await sm.CreateHubContextAsync("messages", token);

    var negotiateResponse = await context.NegotiateAsync(new NegotiationOptions
    {
        UserId = userId
    }, token);

    // The JSON serializer options need to be set to ignore null values, otherwise the
    // response will contain null values for the properties that are not set.
    // The .NET SignalR client will not be able to parse the response if the null values are present.
    // For more information, see https://github.com/dotnet/aspnetcore/issues/60935.
    return Results.Json(negotiateResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    });
});

app.Run();
```

The preceding code configures the Azure SignalR Service using the `ServiceManagerBuilder` class, but doesn't call `AddSignalR` or `MapHub`. These two extensions aren't required with _Serverless_ mode. The connection string is read from the configuration key `ConnectionStrings:signalr`. When using the emulator, only the HTTP endpoint is available. Within the app, you can use the `ServiceManager` instance to create a `ServiceHubContext`. The `ServiceHubContext` is used to broadcast messages and manage connections to clients.

The `/negotiate` endpoint is required to establish a connection between the connecting client and the Azure SignalR Service. The `ServiceHubContext` is created using the `ServiceManager.CreateHubContextAsync` method, which takes the hub name as a parameter. The `NegotiateAsync` method is called to negotiate the connection with the Azure SignalR Service, which returns an access token and the URL for the client to connect to.

For more information, see [Use Azure SignalR Management SDK](https://learn.microsoft.com/azure/azure-signalr/signalr-howto-use-management-sdk).
