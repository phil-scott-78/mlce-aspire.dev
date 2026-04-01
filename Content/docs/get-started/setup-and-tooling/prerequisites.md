---
title: Prerequisites
description: Learn about essential tooling concepts for Aspire.
order: 12
---



Ready to dive into Aspire? Before you begin, make sure your development environment is set up with a few essential tools. This guide walks you through everything you need to start building and running Aspire solutions with confidence.

<Steps>
<Step stepNumber="1">
#### Install your language runtime

</Step>
<Step stepNumber="2">
#### Install an OCI-compliant container runtime

Aspire can run containers using several OCI-compatible runtimes, including Docker Desktop and Podman.

- <a href="https://www.docker.com/products/docker-desktop" target="_blank">Docker Desktop</a> is the most popular container runtime among Aspire developers, offering a familiar and widely supported environment for building and running containers.

- <a href="https://podman.io/" target="_blank">Podman</a> is an open-source, daemonless alternative to Docker. It supports building and running Open Container Initiative (OCI) containers, making it a flexible choice for developers who prefer a lightweight solution.

  - <a href="https://rancherdesktop.io/" target="_blank">Rancher Desktop</a> has been reported by users as a successful alternative—particularly when configured to use the Docker CLI. However, Rancher Desktop is not an officially supported or regularly tested scenario for Aspire. If you encounter issues with Rancher Desktop, please let us know, but fixes may not be prioritized.

</Step>
<Step stepNumber="3">
#### Install an integrated development environment (IDE)

Aspire supports multiple IDEs and code editors. You can choose the one that best fits your workflow:

> [!NOTE] Visual Studio Code
> We recommend [Visual Studio Code](https://code.visualstudio.com/) for the best experience—a lightweight, cross-platform code editor with excellent Aspire support. Install the following extensions to get started:
>
> <Steps>
> <Step stepNumber="1">
> [Aspire extension](/docs/get-started/setup-and-tooling/aspire-vscode-extension) for Aspire-specific commands and features.
>
> </Step>
> </Steps>

</Step>
<Step stepNumber="4">
#### Consider alternatives to local installation

If you prefer not to install the prerequisites on your local machine, you can develop Aspire solutions using cloud-based options like [GitHub Codespaces](/docs/get-started/setup-and-tooling/github-codespaces) or [Dev Containers](/docs/get-started/setup-and-tooling/dev-containers). These options allow you to work in a cloud-based environment, eliminating the need for local installations, but may not provide the same performance as local installations.

The Aspire team maintains a GitHub Codespaces (with preconfigured Dev Container) configuration to help you get started quickly:

</Step>
</Steps>
