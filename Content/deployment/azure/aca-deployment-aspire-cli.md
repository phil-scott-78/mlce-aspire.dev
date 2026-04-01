---
title: Deploy using the Aspire CLI
description: Learn how to deploy Aspire applications to Azure Container Apps using the aspire deploy command.
order: 89
---



When you deploy Aspire applications to Azure Container Apps using the `aspire deploy` command, the platform provides several deployment options and configurations. This guide explains how to use the Aspire CLI to deploy your applications to Azure Container Apps. For general information about deploying Aspire applications, see [Aspire deployment overview](/deployment/overview).

## Prerequisites

- [Aspire prerequisites](/docs/get-started/setup-and-tooling/prerequisites)
- [Aspire CLI](/docs/get-started/setup-and-tooling/install-cli) installed
- An active Azure account and subscription
    - [Create one for free](https://azure.microsoft.com/free/)
    - [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli) installed and available on your `PATH`

## Authenticate with Azure

Before deploying, you must authenticate with Azure CLI. Run the following command:

```bash title="Authenticate with Azure CLI"
az login
```

This command opens a web browser for you to sign in with your Azure credentials. The `aspire deploy` command automatically validates your Azure CLI authentication before proceeding with deployment. For more information, see:

- [Sign in with Azure CLI](https://learn.microsoft.com/cli/azure/authenticate-azure-cli)
- [The `aspire deploy` command reference](/reference/cli/commands/aspire-deploy)

## Create an Aspire project

If you don't already have an Aspire project, create one using the Aspire CLI:

<Steps>
<Step stepNumber="1">
Open a terminal or command prompt.

</Step>
<Step stepNumber="2">
Run the following commands to create a new Aspire project:

```bash title="Create new Aspire project"
aspire new aspire-starter --name AspireDeploy
```

</Step>
<Step stepNumber="3">
Navigate to the project directory:

```bash title="Navigate to project directory"
cd AspireDeploy
```

</Step>
</Steps>

For more details on creating projects, see [Build your first Aspire project](/docs/get-started/first-app).

## Configure for Azure Container Apps

To deploy to Azure Container Apps, add a package reference to your AppHost project that includes the [📦 Aspire.Hosting.Azure.AppContainers](https://www.nuget.org/packages/Aspire.Hosting.Azure.AppContainers) NuGet package:

<InstallPackage PackageName="Aspire.Hosting.Azure.AppContainers" />

In your AppHost project's _AppHost.cs_ file, add the Container Apps environment:

```csharp title="AppHost.cs" {3-4}
var builder = DistributedApplication.CreateBuilder(args);

// Add Azure Container Apps environment
builder.AddAzureContainerAppEnvironment("aspire-env");

var apiService = builder.AddProject<Projects.AspireDeploy_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireDeploy_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
```

For more information, see [Configure Azure Container Apps environments](/integrations/cloud/azure/configure-container-apps).

### Resource naming

Azure Container Apps have specific naming requirements that Aspire handles automatically during deployment. Resource names must be lowercase, alphanumeric, and between 2-32 characters. When you define resources in your AppHost, Aspire generates valid Azure resource names based on your provided names.

## Deploy with aspire deploy

Once your project is configured, deploy it using the `aspire deploy` command:

```bash title="Deploy to Azure Container Apps"
aspire deploy
```

> [!TIP]
> The `aspire deploy` command is interactive by default. To deploy without prompts, set these environment variables first:
> 
>   - `Azure__SubscriptionId`: Target Azure subscription ID.
>   - `Azure__Location`: Azure region (for example, `eastus`).
>   - `Azure__ResourceGroup`: Resource group name to create or reuse.

## Deployment process

The `aspire deploy` command performs the following steps automatically. First, the command validates that you're authenticated with Azure CLI. If you're not logged in, you'll see an error message prompting you to run `az login`.

### Interactive prompts

The deployment process prompts you interactively to select your Azure tenant, subscription, and resource group. Use the and keys to navigate the options, or press the corresponding number key (, , etc.) to jump directly to an option. Press to confirm your selection.

```txt title="Tenant and subscription selection"
(validate-azure-login) ✓ Azure CLI authentication validated successfully
(fetch-tenant) → Fetching available tenants
(fetch-tenant) ✓ Found 3 available tenant(s)
Select your Azure tenant:
Tenant ID:  Default Directory (yourname.onmicrosoft.com) — xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
(fetch-subscription) → Fetching available subscriptions
(fetch-subscription) ✓ Found 1 available subscription(s)
Select your Azure subscription:
Subscription ID:  Visual Studio Enterprise (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)
(fetch-resource-groups) → Fetching resource groups
(fetch-resource-groups) ✓ Found 2 resource group(s)
Select your Azure resource group or enter a new name:
Resource group:  my-resource-group
```

### Build and provision

After selections are made, Aspire builds container images and provisions Azure infrastructure in parallel:

```txt title="Building and provisioning"
(build-apiservice) → Starting build-apiservice...
(build-webfrontend) → Starting build-webfrontend...
(build-apiservice) i [INF] Building container image for resource apiservice
(build-webfrontend) i [INF] Building container image for resource webfrontend
(build-webfrontend) i [INF] Building image for webfrontend completed
(build-webfrontend) ✓ build-webfrontend completed successfully
(build-apiservice) i [INF] Building image for apiservice completed
(build-apiservice) ✓ build-apiservice completed successfully
(provision-aspire-env-acr) → Deploying aspire-env-acr
(provision-aspire-env-acr) ✓ Successfully provisioned aspire-env-acr (15.2s)
(provision-aspire-env) → Deploying aspire-env
```

### Push to Container Registry

Once images are built and the Azure Container Registry is provisioned, the CLI authenticates and pushes your images:

```txt title="Pushing images"
(login-to-acr-aspire-env-acr) → Logging in to aspireenvacrabcd1234xyz
(login-to-acr-aspire-env-acr) ✓ Successfully logged in to aspireenvacrabcd1234xyz.azurecr.io (4.6s)
(push-webfrontend) → Pushing webfrontend to aspireenvacrabcd1234xyz
(push-apiservice) → Pushing apiservice to aspireenvacrabcd1234xyz
(push-webfrontend) ✓ Successfully pushed webfrontend to aspireenvacrabcd1234xyz.azurecr.io/webfrontend:aspire-deploy-20260128154830 (78.5s)
(push-apiservice) ✓ Successfully pushed apiservice to aspireenvacrabcd1234xyz.azurecr.io/apiservice:aspire-deploy-20260128154830 (79.7s)
```

### Deployment complete

Upon successful deployment, you'll see a summary of all steps completed:

```txt title="Pipeline success"
------------------------------------------------------------
✓ 24/24 steps succeeded • Total time: 4m 31s

Steps Summary:
      4m 31s  ✓ pipeline-execution
      3m 24s  ✓ provision-aspire-env
      1m 20s  ✓ push-apiservice
      1m 19s  ✓ push-webfrontend
      27.14s  ✓ create-provisioning-context
      22.62s  ✓ provision-webfrontend-containerapp
      22.29s  ✓ provision-apiservice-containerapp
      15.18s  ✓ provision-aspire-env-acr
      13.18s  ✓ build-apiservice
      13.18s  ✓ build-webfrontend
       4.60s  ✓ login-to-acr-aspire-env-acr
       3.08s  ✓ fetch-tenant
       2.01s  ✓ validate-azure-login
       1.47s  ✓ fetch-resource-groups
       1.08s  ✓ fetch-subscription
      ...

✓ PIPELINE SUCCEEDED
------------------------------------------------------------
```

> [!NOTE]
> This is example output; actual resource names and deployment times will vary based on your environment.

## Monitor deployment

During deployment, you can monitor progress through:

<Steps>
<Step stepNumber="1">
**Console output**: Real-time progress updates and status messages.

</Step>
<Step stepNumber="2">
**Azure portal**: View resources being created in your resource group.

</Step>
<Step stepNumber="3">
**Azure CLI**: Use `az containerapp list` to see deployed applications.

</Step>
</Steps>

## Troubleshooting deployment

When using the `aspire deploy` command, you may encounter various issues during deployment. Use this section to learn common problems and tips for troubleshooting, helping you quickly identify and fix errors.

### Authentication issues

If you encounter authentication errors:

```plaintext title="Authentication error"
❌ Azure CLI authentication failed. Please run 'az login' to authenticate before deploying.
```

Run `az login` and ensure you have the necessary permissions in your Azure subscription.

### Resource naming conflicts

If resource names conflict with existing Azure resources, modify your resource names in the AppHost:

```csharp title="Unique environment name" '"my-unique-env-name"'
// Change the environment name to a unique value
builder.AddAzureContainerAppEnvironment("my-unique-env-name");
```

### Container build failures

If container builds fail, ensure:

- Docker is running and accessible.
- Your project builds successfully locally: `dotnet build`.
- Container runtime is properly configured.

## Access your deployed application

After successful deployment:

<Steps>
<Step stepNumber="1">
**Aspire Dashboard**: Access the dashboard URL provided in the deployment output.

</Step>
<Step stepNumber="2">
**Application endpoints**: Find your application URLs in the Azure portal under Container Apps.

</Step>
<Step stepNumber="3">
**Azure CLI**: List endpoints using:

</Step>
</Steps>

## Clean up resources

To remove deployed resources, delete the resource group:

```bash title="Delete resource group"
az group delete --name <resource-group-name>
```

> [!DANGER]
> This command deletes all resources in the resource group, including any
>   resources not created by the Aspire CLI.

- [Aspire deployment overview](/deployment/overview)
- [Configure Azure Container Apps environments](/integrations/cloud/azure/configure-container-apps)
- [`aspire deploy` command reference](/reference/cli/commands/aspire-deploy)
