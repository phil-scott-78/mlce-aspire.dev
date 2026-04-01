---
title: Aspire CLI overview
order: 107
---



The Aspire CLI (`aspire` command) is a cross-platform tool that provides command-line functionality to create, manage, run, and publish multi-language Aspire projects. Use the Aspire CLI to streamline development workflows and coordinate services for distributed applications.

> [!NOTE]
> Starting with Aspire 13.0, the Aspire CLI requires .NET SDK version 10.0.100
>   or later. Ensure that you have the appropriate .NET SDK installed to use the
>   Aspire CLI.

The Aspire CLI is an interactive-first experience.

<CardGrid>
  <LinkCard Href="/get-started/install-cli"
    Title="Install the Aspire CLI"
   >Get started by installing the Aspire CLI on your machine.</LinkCard>
  <LinkCard Href="../install-script/"
    Title="Review install script reference"
   >Explore the details of the install script with reference.</LinkCard>
</CardGrid>

## Use templates

The `aspire new` command is an interactive-first CLI experience, and is used to create one or more Aspire projects. As part of creating a project, Aspire CLI ensures that the latest Aspire project templates are installed into the `dotnet` system.

Use the `aspire new` command to create an Aspire project from a list of templates. Once a template is selected, the name of the project is set, and the output folder is chosen, `aspire` downloads the latest templates and generates one or more projects.

### Example usage


The following example shows the Aspire CLI welcome banner, which gives the command overview page a more branded first impression before you drill into commands like `aspire new`:

While command-line parameters can be used to automate the creation of an Aspire project, the Aspire CLI is an interactive-first experience.

## Initialize existing solutions

The `aspire init` command adds Aspire support to an existing .NET solution or creates a single-file AppHost. This is useful when you want to add Aspire orchestration capabilities to an existing application without creating an entirely new solution structure.

The command analyzes your existing solution and adds the necessary Aspire AppHost project or creates a minimal single-file AppHost, making it easy to adopt Aspire incrementally.

## Start the Aspire AppHost

The `aspire run` command runs the AppHost project in development mode, which configures the Aspire environment, builds and starts resources defined by the AppHost, launches the web dashboard, and prints a list of endpoints.

When `aspire run` starts, it searches the current directory for an AppHost project. If a project isn't found, the subdirectories are searched until one is found. If no AppHost project is found, Aspire stops. Once a project is found, Aspire CLI takes the following steps:


<Steps>
<Step stepNumber="1">
Installs or verifies that Aspire's local hosting certificates are installed and trusted.

</Step>
<Step stepNumber="2">
Builds the AppHost project and its resources.

</Step>
<Step stepNumber="3">
Starts the AppHost and its resources.

</Step>
<Step stepNumber="4">
Starts the dashboard.

</Step>
</Steps>

The following snippet is an example of the output displayed by the `aspire run` command:

```bash title="Aspire CLI"
Dashboard:  https://localhost:17244/login?t=9db79f2885dae24ee06c6ef10290b8b2

    Logs:  /home/vscode/.aspire/cli/logs/apphost-5932-2025-08-25-18-37-31.log

        Press CTRL+C to stop the apphost and exit.
```

## Add integrations

The `aspire add` command is an easy way to add official integration packages to your AppHost project. Use this as an alternative to a NuGet search through your IDE. You can run `aspire add <name|id>` if you know the name or NuGet ID of the integration package. If you omit a name or ID, the tool provides a list of packages to choose from. If you provide a partial name or ID, the tool filters the list of packages with items that match the provided value.

## Update Aspire packages

The `aspire update` command helps you keep your Aspire projects current by automatically detecting and updating outdated packages and templates. It finds outdated Aspire NuGet packages while respecting channel configurations and intelligently handles complex dependency graphs.

The command validates package compatibility before applying changes and provides detailed output showing which packages were updated.

## Publish Aspire applications

The `aspire publish` command publishes resources by serializing them to disk. When this command is run, Aspire executes the publish pipeline step, and any dependent steps, registered in the app model. These steps serialize resources so that they can be consumed by deployment tools.

Some integrations automatically register publishing pipeline steps for you, for example:

- `AzureEnvironmentResource` generates Bicep assets
- `DockerComposeEnvironmentResource` generates docker-compose YAML
- `KubernetesEnvironmentResource` generates Kubernetes Helm charts

## Deploy Aspire solutions

The `aspire deploy` command invokes the `deploy` pipeline step and any dependent steps (such as the `publish` step) registered in the app model.

> [!TIP]
> Consider this a good way to deploy your Aspire solution to a staging or
>   testing environment.

## Execute pipeline steps

The `aspire do` command executes a specific pipeline step and its dependencies in your Aspire AppHost. This provides fine-grained control over the orchestration pipeline, allowing you to run individual steps of the deployment or build process without executing the entire pipeline.

Use this command to test individual pipeline stages during development or to execute only the dependencies needed for a particular step.

## Restore AppHost dependencies

The `aspire restore` command restores dependencies and generates the SDK code your AppHost needs before you build, run, or automate the application. Use it as an explicit restore step in CI/CD pipelines or after changing integrations and resources.

## Start the AppHost in the background

The `aspire start` command starts an AppHost in the background and returns once it is running. Use it when you want a detached apphost without staying attached to the interactive `aspire run` session.

## Stop a running AppHost

The `aspire stop` command stops a running Aspire AppHost process. When multiple AppHosts are running, the command prompts you to select which one to stop, or you can use `--all` to stop them all at once.

## List running AppHosts

The `aspire ps` command lists all running Aspire AppHost processes. The output includes the AppHost project path, process IDs, and dashboard URLs for each running instance. Use `--format Json` for machine-readable output suitable for scripting and automation.

## Wait for resource readiness

The `aspire wait` command blocks until a named resource within a running AppHost reaches a target status such as `healthy`, `up`, or `down`. This is useful for CI/CD pipelines and automation workflows where you need to wait for resources to be ready after starting an AppHost in the background.

## Run resource commands

The `aspire resource` command executes a command exposed by a resource in a running AppHost, such as `start`, `stop`, or `restart`. Use it when you need to manage an individual resource directly from the terminal.

## Inspect runtime data

Use `aspire describe`, `aspire logs`, and `aspire export` to inspect resources, stream logs, and package telemetry and resource data from a running AppHost. Use `aspire otel` to view OpenTelemetry data—structured logs, distributed trace spans, and trace summaries—collected by the Aspire Dashboard directly from the terminal. Together, these commands give you a CLI-first workflow for monitoring and collecting diagnostics.

## Diagnose and prepare the environment

Use `aspire doctor` to verify that your environment is ready for Aspire development, and `aspire certs` to manage the HTTPS development certificates used by local resources and dashboards.

## Work with MCP tools

The `aspire mcp` command lets you list and call MCP tools exposed by running resources. If you need to start the MCP server used by compatible AI agents, use `aspire agent mcp`.

## Manage CLI configuration

The `aspire config` command lets you manage Aspire CLI configuration settings. Use it to `list`, `get`, `set`, or `delete` configuration values that control CLI behavior. This command is also used to toggle features on or off.

## Manage secrets

The `aspire secret` command manages user secrets for an Aspire AppHost project. User secrets provide a safe way to store sensitive configuration values—such as passwords, API keys, and connection strings—outside of your project files during local development.

Aspire uses user secrets to persist values that resources need across restarts, such as auto-generated passwords for database containers.

## Browse documentation

The `aspire docs` command lets you browse and search the official Aspire documentation directly from the terminal. You can list all available pages, search for specific topics by keyword, or retrieve the full content of a page by its slug—all without leaving your development workflow.

## Manage CLI cache

The `aspire cache` command manages the disk cache used by the Aspire CLI. The CLI caches data such as downloaded templates, NuGet package information, and other temporary files to improve performance and reduce network requests.

Use `aspire cache clear` to remove all cached data. This is useful when troubleshooting CLI issues, freeing up disk space, or ensuring fresh data after updating the CLI.

