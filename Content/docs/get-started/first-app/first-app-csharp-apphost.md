---
title: "Build your first Aspire app with a C# AppHost"
description: "Learn how to build your first Aspire solution using starter templates that generate a C# AppHost."
order: 18
---



This quickstart uses starter templates that generate a C# AppHost. Choose the app stack you want to start from, then create the solution, review the generated AppHost, and run it locally with Aspire.

## Create a new app

To create your first Aspire application, use the [Aspire CLI](/docs/get-started/setup-and-tooling/install-cli) to generate a new solution from a _starter_ template. These template include multiple projects, such as an API service, a web frontend, and an [Aspire AppHost](/docs/fundamentals/core-concepts/app-host).


<Steps>
<Step stepNumber="1">
Create a new Aspire solution from a template:

The template provides several projects, including an API service, web frontend, and AppHost.

> [!TIP] CLI flags
> The following flags are used in the command:
>
> - `-n`: specifies the name of the solution.
> - `-o`: specifies the output directory.

If prompted for additional selections, use the and keys to navigate the options. Press to confirm your selection.

</Step>
</Steps>

## Review the template code

<Steps>
<Step stepNumber="1">
Examine the created template structure. The Aspire CLI creates a new folder with the name you provided in the current directory. This folder contains the solution file and several projects, including:

This solution structure is based on the Aspire templates. If they're not installed already, the CLI will install them for you.

</Step>
<Step stepNumber="2">
Explore the AppHost code that orchestrates your app.

The [AppHost](/docs/fundamentals/core-concepts/app-host) is the heart of your Aspire application—it defines which services run, how they connect, and in what order they start. Let's look at the generated code:

> [!NOTE] Code-first orchestration
> Your application topology is defined in code, making it easy to understand, modify, and version control. Learn more about the [AppHost](/docs/fundamentals/core-concepts/app-host).

</Step>
</Steps>

## Run the app

<Steps>
<Step stepNumber="1">
Change to the _output_ directory:

</Step>
<Step stepNumber="2">
Call `aspire run` to start dev-time orchestration:

```bash title="Run dev-time orchestration"
aspire run
```

When you run this command, the Aspire CLI:

- Automatically finds the AppHost
- Builds your solution
- Launches dev-time orchestration

Once the dashboard is ready, its URL (with a login token—highlighted in the example output below) appears in your terminal. The dashboard provides a live, real-time view of your running resources and their current states.

</Step>
<Step stepNumber="3">
Explore the running distributed application. From the dashboard, open the `HTTPS` endpoint from each resource.

</Step>
</Steps>

## Stop the app

<Steps>
<Step stepNumber="1">
Stop the AppHost and close the dashboard by pressing in your terminal.

```bash title="Stop dev-time orchestration"
🛑  Stopping Aspire.
```

**Congratulations! You've created your first Aspire app.**

</Step>
</Steps>

## See also

- Having trouble? Check out our [Troubleshooting guide](/docs/get-started/troubleshooting) for solutions to common problems.
- Prefer a GUI? The [Aspire VS Code extension](/docs/get-started/setup-and-tooling/aspire-vscode-extension) lets you create, run, and debug Aspire apps from VS Code.
