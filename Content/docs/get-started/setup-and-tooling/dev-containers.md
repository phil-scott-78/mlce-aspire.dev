---
title: Dev Containers in Visual Studio Code
description: Learn how to use Aspire with Dev Containers in Visual Studio Code for containerized development environments.
order: 15
---



The [Dev Containers Visual Studio Code extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) provides a way for development teams to work inside a containerized environment where dependencies are defined as part of the repository. Aspire automatically configures forwarded ports in Dev Containers so the dashboard and resource endpoints are easier to open from Visual Studio Code.

## Dev Containers vs. GitHub Codespaces

Using Dev Containers in Visual Studio Code is similar to using GitHub Codespaces. Although the experiences are similar, there are some differences. For more information on using Aspire with GitHub Codespaces, see [Aspire and GitHub Codespaces](/docs/get-started/setup-and-tooling/github-codespaces).

## Quick start using template repository

To configure Dev Containers in Visual Studio Code, use the `.devcontainer/devcontainer.json` file in your repository. The simplest way to get started is by creating a new repository from the [Aspire Dev Container template](https://github.com/dotnet/aspire-devcontainer). Consider the following steps:


<Steps>
<Step stepNumber="1">
[Create a new repository](https://github.com/new?template_name=aspire-devcontainer&template_owner=dotnet) using our template.

Once you provide the details and select **Create repository**, the repository is created and shown in GitHub.

</Step>
<Step stepNumber="2">
Clone the repository to your local developer workstation using the following command:

```bash
git clone https://github.com/<org>/<username>/<repository>
```

</Step>
<Step stepNumber="3">
Open the repository in Visual Studio Code. After a few moments Visual Studio Code detects the `.devcontainer/devcontainer.json` file and prompts to open the repository inside a container. Select whichever option is most appropriate for your workflow.

![Screenshot showing VS Code prompt to open the repository inside a container.](/assets/get-started/reopen-in-container.png)

After a few moments, the files become visible and the Dev Container finishes setting up. The template installs the Aspire CLI during setup, so you can create an app immediately without manually installing `Aspire.ProjectTemplates`.

![Screenshot showing the dev container build completed notification in VS Code.](/assets/get-started/devcontainer-build-completed.png)

</Step>
<Step stepNumber="4">
Open a new terminal window in Visual Studio Code () and create a new Aspire project using the Aspire CLI.

```bash title="Aspire CLI — Create an Aspire starter app"
aspire new aspire-starter --name HelloAspire
```

After a few moments, the project will be created and initial dependencies restored.

</Step>
<Step stepNumber="5">
Open the `HelloAspire.AppHost/AppHost.cs` file in the editor and select the run button on the top right corner of the editor window. This run button is provided by the [Aspire VS Code extension](/docs/get-started/setup-and-tooling/aspire-vscode-extension) or [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit).

![Screenshot showing the run button in the VS Code editor toolbar.](/assets/get-started/vscode-run-button.png)

Visual Studio Code builds and starts the Aspire AppHost and automatically opens the Aspire Dashboard. Because the endpoints hosted in the container are using a self-signed certificate the first time, you access an endpoint for a specific Dev Container you're presented with a certificate error.

![Screenshot showing the browser certificate error when accessing the dashboard.](/assets/get-started/browser-certificate-error.png)

The certificate error is expected. Once you've confirmed that the URL being requested corresponds to the dashboard in the Dev Container you can ignore this warning.

![Screenshot of the Aspire dashboard running successfully in the Dev Container.](/assets/get-started/aspire-dashboard-in-devcontainer.png)

Aspire automatically configures forwarded ports so that when you select the endpoints in the Aspire dashboard they're tunneled to processes and nested containers within the Dev Container.

</Step>
<Step stepNumber="6">
Commit changes to the GitHub repository.

After successfully creating the Aspire project and verifying that it launches and you can access the dashboard, it's a good idea to commit the changes to the repository.

</Step>
</Steps>

## Manually configuring devcontainer.json

The preceding walkthrough demonstrates the streamlined process of creating a Dev Container using the Aspire Dev Container template. If you already have an existing repository and want to use Dev Containers with Aspire, add a `devcontainer.json` file to the `.devcontainer` folder within your repository:

The [template repository](https://github.com/dotnet/aspire-devcontainer) contains a copy of the `devcontainer.json` file that you can use as a starting point, which should be sufficient for Aspire.

> [!NOTE]
> Install the Aspire CLI during container creation so `aspire new` is ready to use as soon as the container finishes building.

## Dev Container scenarios

The basic Aspire Dev Container template works well for simple scenarios, but you might need additional configuration depending on your specific requirements. The following sections provide examples for various common scenarios.

### Stateless .NET apps only

For simple Aspire projects that only use .NET project resources without external containers or complex orchestration, you can use a minimal Dev Container configuration:

```json title="JSON — .devcontainer/devcontainer.json"
{
  "name": "Aspire - Simple",
  "image": "mcr.microsoft.com/devcontainers/dotnet:dev-10.0-noble",
  "onCreateCommand": "curl -sSL https://aspire.dev/install.sh | bash",
  "postStartCommand": "dotnet dev-certs https --trust",
  "customizations": {
    "vscode": {
      "extensions": ["ms-dotnettools.csdevkit"]
    }
  }
}
```

This minimal configuration is suitable for Aspire apps that orchestrate only .NET services without external dependencies.

### Adding Node.js resources

If your Aspire app includes Node.js resources, add the Node.js feature to your Dev Container:

```json title="JSON — .devcontainer/devcontainer.json"
{
  "name": "Aspire with Node.js",
  "image": "mcr.microsoft.com/devcontainers/dotnet:dev-10.0-noble",
  "features": {
    "ghcr.io/devcontainers/features/node:1": {
      "version": "lts"
    }
  },
  "onCreateCommand": "curl -sSL https://aspire.dev/install.sh | bash",
  "postStartCommand": "dotnet dev-certs https --trust",
  "customizations": {
    "vscode": {
      "extensions": [
        "ms-dotnettools.csdevkit",
        "ms-vscode.vscode-typescript-next"
      ]
    }
  }
}
```

This configuration provides both .NET and Node.js development capabilities within the same container environment.

### Container orchestration with Docker-in-Docker

When your Aspire app orchestrates container resources, you need Docker-in-Docker (DinD) support. Here's a basic configuration:

```json title="JSON — .devcontainer/devcontainer.json"
{
  "name": "Aspire with Containers",
  "image": "mcr.microsoft.com/devcontainers/dotnet:dev-10.0-noble",
  "features": {
    "ghcr.io/devcontainers/features/docker-in-docker:2": {
      "version": "latest",
      "enableNonRootDocker": true,
      "moby": true
    }
  },
  "hostRequirements": {
    "cpus": 4,
    "memory": "16gb",
    "storage": "32gb"
  },
  "onCreateCommand": "curl -sSL https://aspire.dev/install.sh | bash",
  "postStartCommand": "dotnet dev-certs https --trust",
  "customizations": {
    "vscode": {
      "extensions": ["ms-dotnettools.csdevkit", "ms-azuretools.vscode-docker"]
    }
  }
}
```

#### Advanced container networking

If you encounter networking issues between containers or need IPv6 support, you can add additional network configuration:

```json title="JSON — .devcontainer/devcontainer.json"
{
  "name": "Aspire with Advanced Networking",
  "image": "mcr.microsoft.com/devcontainers/dotnet:dev-10.0-noble",
  "features": {
    "ghcr.io/devcontainers/features/docker-in-docker:2": {
      "version": "latest",
      "enableNonRootDocker": true,
      "moby": true
    }
  },
  "runArgs": [
    "--sysctl",
    "net.ipv6.conf.all.disable_ipv6=0",
    "--sysctl",
    "net.ipv6.conf.default.forwarding=1",
    "--sysctl",
    "net.ipv6.conf.all.forwarding=1"
  ],
  "hostRequirements": {
    "cpus": 8,
    "memory": "32gb",
    "storage": "64gb"
  },
  "onCreateCommand": "curl -sSL https://aspire.dev/install.sh | bash",
  "postStartCommand": "dotnet dev-certs https --trust",
  "customizations": {
    "vscode": {
      "extensions": ["ms-dotnettools.csdevkit", "ms-azuretools.vscode-docker"]
    }
  }
}
```
