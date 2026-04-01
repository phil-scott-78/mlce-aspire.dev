---
title: Customize Azure Container Apps
description: Learn how to customize Azure Container Apps deployments for your Aspire applications.
order: 90
---



When you deploy to [Azure Container Apps](https://learn.microsoft.com/azure/container-apps/overview) using `aspire deploy`, Aspire provisions infrastructure automatically based on the resources defined in your AppHost. While the defaults work for many scenarios, you often need to customize the generated infrastructure—scaling rules, custom domains, resource groups, role assignments, and more.

This page covers the most common customization options for Azure Container Apps deployments. For hosting-side configuration of the Container Apps _environment_, see [Configure Azure Container Apps](/integrations/cloud/azure/configure-container-apps).

## Customize a Container App with `PublishAsAzureContainerApp`

Use `PublishAsAzureContainerApp` to customize the generated Container App resource for any project, container, or executable in your AppHost. The callback gives you access to the `AzureResourceInfrastructure` and the `ContainerApp` construct, allowing you to modify any property of the generated resource.


**C#**


```csharp title="AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureContainerAppEnvironment("env");

builder.AddProject<Projects.Api>("api")
    .PublishAsAzureContainerApp((infrastructure, app) =>
    {
        // Customize the Container App
        app.Template.Scale.MinReplicas = 1;
        app.Template.Scale.MaxReplicas = 10;
    });

builder.Build().Run();
```


**TypeScript**


```typescript title="apphost.ts"

const builder = await createBuilder();

await builder.addAzureContainerAppEnvironment("env");

const api = await builder.addProject("api", "../Api/Api.csproj", "http");
await api.publishAsAzureContainerApp(async (infrastructure, app) => {
    // Customize the Container App
    // Note: Scaling properties (minReplicas, maxReplicas) are
    // configured through the ContainerAppHandle API
});

await builder.build().run();
```


The `infrastructure` parameter provides access to the overall Azure resource infrastructure, while the `app` parameter represents the `ContainerApp` construct. You can modify scaling, ingress, environment variables, and other settings directly.

## Custom domains

Use `ConfigureCustomDomain` to set up custom domains on Container Apps. This lets you bind a custom DNS name and a managed certificate to your Container App.


**C#**


```csharp title="AppHost.cs"
var customDomain = builder.AddParameter("customDomain");
var certificateName = builder.AddParameter("certificateName");

#pragma warning disable ASPIREACADOMAINS001
builder.AddProject<Projects.Api>("api")
    .PublishAsAzureContainerApp((infrastructure, app) =>
    {
        ContainerAppExtensions.ConfigureCustomDomain(
            app, customDomain, certificateName);
    });
#pragma warning restore ASPIREACADOMAINS001
```


**TypeScript**


> [!NOTE]
> The `ConfigureCustomDomain` API is not yet available in the TypeScript AppHost SDK.


> [!CAUTION]
> The `ConfigureCustomDomain` API is experimental and may change or be removed in future updates. Suppress the compiler warning with `#pragma warning disable ASPIREACADOMAINS001`. For more information, see [ASPIREACADOMAINS001](/docs/diagnostics/aspireacadomains001).

## Existing Azure resources

When you have Azure resources that already exist—such as a shared Service Bus namespace or a database server—you can reference them instead of having Aspire provision new ones. Aspire provides three APIs for this:

- `AsExisting` — use the existing resource in both local development and deployment.
- `RunAsExisting` — use the existing resource during local development only.
- `PublishAsExisting` — use the existing resource during deployment only.


**C#**


```csharp title="AppHost.cs"
var existingBusName = builder.AddParameter("existingServiceBusName");
var existingBusRg = builder.AddParameter("existingServiceBusResourceGroup");

var serviceBus = builder.AddAzureServiceBus("messaging")
    .PublishAsExisting(existingBusName, existingBusRg);
```


**TypeScript**


```typescript title="apphost.ts"
const existingBusName = builder.addParameter("existingServiceBusName");
const existingBusRg = builder.addParameter("existingServiceBusResourceGroup");

const serviceBus = await builder.addAzureServiceBus("messaging");
await serviceBus.publishAsExistingFromParameters(
    existingBusName,
    existingBusRg
);
```


When you reference an existing resource, Aspire skips provisioning for that resource and instead configures your application to connect to the specified instance.

> [!NOTE]
> Cross-subscription resource references are not yet supported. The existing resource must be in the same Azure subscription as your deployment target.

## Resource groups

By default, all Azure resources deploy to the same resource group created by the environment. Use `WithResourceGroup` on the `AzureEnvironmentResource` to set the resource group for the deployment:


**C#**


```csharp title="AppHost.cs"
builder.AddAzureEnvironment()
    .WithResourceGroup(builder.AddParameter("resourceGroup"));
```


**TypeScript**


> [!NOTE]
> The `WithResourceGroup` API is not yet available in the TypeScript AppHost SDK.


> [!NOTE]
> `WithResourceGroup` is configured on the Azure environment resource, not on individual resources. All resources in the environment share the same resource group.

## Role assignments

Aspire automatically assigns Azure RBAC roles for resources based on how they're referenced. For example, a project that references an Azure Service Bus resource is automatically granted the **Azure Service Bus Data Sender** role.

To remove the default role assignments for a resource, use `ClearDefaultRoleAssignments`:


**C#**


```csharp title="AppHost.cs"
builder.AddAzureServiceBus("messaging")
    .ClearDefaultRoleAssignments();
```


**TypeScript**


```typescript title="apphost.ts"
const serviceBus = await builder.addAzureServiceBus("messaging");
await serviceBus.clearDefaultRoleAssignments();
```


Custom role assignments can be configured via `ConfigureInfrastructure`.

## Service discovery on Azure Container Apps

Services deployed to Azure Container Apps use internal DNS for service discovery. HTTP and HTTPS endpoints are both available by default for internal communication between container apps.

To expose an endpoint externally (outside the Container Apps environment), use `WithExternalHttpEndpoints()`:


**C#**


```csharp title="AppHost.cs"
builder.AddProject<Projects.Api>("api")
    .WithExternalHttpEndpoints();
```


**TypeScript**


```typescript title="apphost.ts"
const api = await builder.addProject("api", "../Api/Api.csproj", "http");
await api.withExternalHttpEndpoints();
```


## See also

- [Configure Azure Container Apps](/integrations/cloud/azure/configure-container-apps)
- [Deploy using the Aspire CLI](/deployment/azure/aca-deployment-aspire-cli)
- [Manage Azure role assignments](/integrations/cloud/azure/role-assignments)
- [Azure security best practices](/deployment/azure/azure-security-best-practices)
