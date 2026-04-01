---
title: Deploy your first app — C# AppHost
description: Deploy your C# AppHost Aspire application to Docker Compose or Azure.
order: 21
---



In this tutorial, you take the app you created in the [Build your first Aspire app — C# AppHost](/docs/get-started/first-app/first-app-csharp-apphost) quickstart and deploy it. This can be broken down into several key steps:


<Steps>
<Step stepNumber="1">
[Add deployment package](#add-deployment-package) — Add the hosting package for your target.

</Step>
<Step stepNumber="2">
[Update your AppHost](#update-your-apphost) — Configure the environment API.

</Step>
<Step stepNumber="3">
[Deploy your app](#deploy-your-app) — Deploy using the Aspire CLI.

</Step>
<Step stepNumber="4">
[Verify your deployment](#verify-your-deployment) — Ensure your app is running as expected.

</Step>
<Step stepNumber="5">
[Clean up resources](#clean-up-resources) — Remove any deployed resources to avoid incurring costs.

</Step>
</Steps>

The following diagram shows the architecture of the sample app you're deploying:

## Prerequisites

Depending on where you want to deploy your Aspire app, ensure you have the following prerequisites installed and configured:


**Docker Compose**

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running.
- [Podman (alternative to Docker)](https://podman.io/getting-started/installation) installed and running. For more information, see [OCI-compatible container runtime](/docs/get-started/setup-and-tooling/prerequisites/#install-an-oci-compliant-container-runtime).

**Azure**

- An [Azure account](https://azure.microsoft.com/free/) with an active subscription.
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli) installed and configured. You should be logged in using `az login`.
        


## Add deployment package

In the root directory of your Aspire solution that you created in the previous quickstart, add the appropriate hosting deployment package by running the following command in your terminal:


**Docker Compose**

        [Docker Compose](https://docs.docker.com/compose/) is a tool for defining and running multi-container Docker applications. It allows you to use a YAML file to configure your application's services, networks, and volumes, making it easier to manage and deploy complex applications locally or in various environments.

        ```bash title="Aspire CLI — Add Docker Compose"
        aspire add docker
        ```

        The Aspire CLI is interactive, be sure to select the appropriate search result for the [📦 Aspire.Hosting.Docker](https://www.nuget.org/packages/Aspire.Hosting.Docker) version you want to add.

    
**Azure**

        ```bash title="Aspire CLI — Add Azure App Containers"
        aspire add azure-appcontainers
        ```

        The Aspire CLI is interactive, be sure to select the appropriate search result for the [📦 Aspire.Hosting.Azure.AppContainers](https://www.nuget.org/packages/Aspire.Hosting.Azure.AppContainers) version you want to add.

    

    If prompted for additional selections, use the and keys to navigate the options. Press to confirm your selection.

    ## Update your AppHost

In the AppHost, chain a call to the appropriate environment API method to configure the deployment environment for your target.


**Docker Compose**

        - `AddDockerComposeEnvironment` — Configures the Docker Compose environment for deployment. This call implicitly adds support for containerizing resources in the AppHost as part of deployment.
        - `WithExternalHttpEndpoints` — Exposes HTTP endpoints for the resource when deployed.

    
**Azure**

        - `AddAzureContainerAppEnvironment` — Configures the Azure App Container environment for deployment. This call implicitly adds support for containerizing resources in the AppHost as part of deployment.
        - `WithExternalHttpEndpoints` — Exposes HTTP endpoints for the resource when deployed.

    


> [!TIP] CLI protip
> After installing a new deployment package, you can run `aspire do diagnostics` in your terminal to see the available deploy steps. For more information, see the [aspire do diagnostics](/reference/cli/commands/aspire-do) reference docs.

## Deploy your app

Now that you've added the deployment package and updated your AppHost, you can deploy your Aspire app.


**Docker Compose**

    Deploying to Docker Compose builds the container images and starts the services locally using Docker Compose.
    
    ```bash title="Aspire CLI — Deploy your app"
    aspire deploy
    ```
    
    Consider the following example output:

**Azure**

    When deploying to Azure, the `aspire deploy` command is interactive. To avoid prompts (for example, when running in CI/CD), set the following environment variables:

    - `Azure__SubscriptionId`: Target Azure subscription ID.
    - `Azure__Location`: Azure region (for example, eastus).
    - `Azure__ResourceGroup`: Resource group name to create or reuse.

    Deploying to Azure App Containers builds the container images and deploys the services to Azure App Containers.
    
    ```bash title="Aspire CLI — Deploy your app"
    aspire deploy
    ```
    
    Consider the following example output:

    When you call `aspire deploy`, the Aspire CLI builds the container images for your resources, pushes them to the target environment (if applicable), and deploys the resources according to the configuration in your AppHost.

> [!NOTE] Common pitfall...
> If you call `aspire deploy` and you see output similar to the following, be sure that you've actually [updated your AppHost](#update-your-apphost) to include the appropriate environment API for your target. This output indicates that there are no deploy steps configured for your target environment.
>
> ```bash title="Aspire CLI - Empty deployment output"
> 14:17:26 (pipeline execution) → Starting pipeline execution...
> 14:17:26 (deploy) → Starting deploy...
> 14:17:26 (deploy) ✓ deploy completed successfully
> 14:17:26 (pipeline execution) ✓ Completed successfully
> ------------------------------------------------------------
> ✓ 2/2 steps succeeded • Total time: 0.0s
>
> Steps Summary:
> 0.0 s  ✓ pipeline execution
> 0.0 s  ✓ deploy
>
> ✓ PIPELINE SUCCEEDED
> ------------------------------------------------------------
> ```

### Post deployment output

After a deployment, the Aspire CLI writes to the provided output path (or the default output path if none is provided) a set of files based on your deployment target. This may include files such as Docker Compose files, Kubernetes manifests, or cloud provider-specific configuration files.


**Docker Compose**

**Azure**

    After deploying to Azure App Containers, the Aspire CLI saves the deployment state file to your local machine.

    > [!TIP] AppHost SHA256
> The deployment state is saved in a directory named after the SHA256 hash of your full AppHost path. This ensures that deployments for different applications or versions are kept separate.

    


## Verify your deployment


To verify that your application is running as expected after deployment, follow the instructions for your chosen deployment target below.


**Docker Compose**

        When deploying to Docker Compose, the `aspire deploy` command displays the URLs where your services are running. Look for the `print-*-summary` steps in the deployment output, which show the localhost URLs for each service that has been configured with `WithExternalHttpEndpoints`.

    **Azure**

        ## Clean up resources

After deploying your application, it's important to clean up resources to avoid incurring unnecessary costs or consuming local system resources.


**Docker Compose**
        To clean up resources after deploying with Docker Compose, you can stop and remove the running containers using the following command:

        ```bash title="Aspire CLI - Stop and remove containers"
        aspire do docker-compose-down-env
        ```
    
**Azure**
        To clean up resources after deploying to Azure, you can use the Azure CLI to delete the resource group that contains your application. This will remove all resources within the resource group.

        ```bash title="Azure CLI - Delete resource group"
        az group delete --name <RESOURCE_GROUP_NAME> --yes --no-wait
        ```
    


You've just built your first Aspire app and deployed it to production—congratulations! 🎉 Now you might be wondering: "How do I make sure all these services actually work together correctly?" That's where integration testing comes in. Aspire makes it easy to test your entire application stack, including service-to-service communication and resource dependencies. Ready to learn how? [Write your first test](/docs/testing/write-your-first-test)
