---
title: Get started with the Azure App Service integration
description: Learn how to set up the Aspire Azure App Service Hosting integration simply.
order: 196
---



<IntegrationIcon Src="/assets/icons/azure-app-services.svg" Alt="Azure App Service logo">

<Badge text="🧪 Preview" variant="note" size="large" />
</IntegrationIcon>

[Azure App Service](https://learn.microsoft.com/azure/app-service) is a fully managed platform for building, deploying, and scaling web apps. Aspire apps often run locally during development but require scalable, production-ready hosting environments for staging and production. The Aspire integration allows you to provision or reference an existing Azure App Service environment (App Service Plan) and seamlessly publish your container, executable, and project resources as Azure App Service websites.

In this introduction, you'll see how to install and use the Aspire Azure App Service integration in a simple configuration. If you already have this knowledge, see [Azure App Service Hosting integration](/integrations/cloud/azure/azure-app-service/azure-app-service-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure App Service Hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Azure.AppService" />

Next, add an App Service environment to your AppHost project. The environment represents the hosting infrastructure (App Service Plan) where your apps will run:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var appServiceEnv = builder.AddAzureAppServiceEnvironment("app-service-env");

builder.AddProject<Projects.WebApi>("api")
    .PublishAsAzureAppServiceWebsite((infra, website) =>
    {
        // Optional: customize the Azure App Service website here
    });

builder.Build().Run();
```

> [!CAUTION]
> When you call `AddAzureAppServiceEnvironment`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

The preceding code:

- Creates an Azure App Service environment named `app-service-env`.
- Adds a project named `api` to the AppHost.
- Configures the project to be published as an Azure App Service website.
- Provides an optional callback to customize the website configuration.

During local development (when running with ), the project runs locally. When you publish your app, the project is deployed as an Azure App Service website within the provisioned environment.

## See also

<CardGrid>
  <LinkCard Title="Azure App Service Hosting integration"
    Href="/integrations/cloud/azure/azure-app-service/azure-app-service-host">Learn about Azure App Service hosting integration, including provisioning, customization, and configuration.</LinkCard>
</CardGrid>
