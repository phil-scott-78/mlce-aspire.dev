---
title: What is Aspire?
description: Learn how Aspire helps you compose, debug, and deploy any distributed app with a code-first, agent-ready workflow.
order: 30
---



Aspire is an agent-ready, code-first tool to compose, debug, and deploy any distributed app.

Picture your app as a set of services, databases, queues, caches, and frontends. In production they work together as one system, but during development they often need to be started separately, configured by hand, and debugged across multiple terminals and dashboards. Aspire gives you one place to define that system, one command to run it, and one toolchain to observe it.

You describe your architecture in an AppHost, run it locally with `aspire run`, inspect it in the Aspire Dashboard, and deploy the same application model to your target environment. Aspire also works well with AI coding agents through the Aspire CLI, skill files, and MCP server, so agents can understand and operate against the same app model you do.

## Why Aspire?

Building modern applications means coordinating multiple resources, runtimes, and workflows. Here's what that looks like **without** Aspire:

### The pain of distributed development

| Problem | Without Aspire | With Aspire |
|---------|----------------|-------------|
| **Starting services** | Open 5 terminals, run 5 different commands, hope they start in the right order | Run `aspire run` — everything starts automatically. See [AppHost overview](/docs/fundamentals/core-concepts/app-host). |
| **Connection strings** | Hardcode `localhost:5432` everywhere, break production deploys | Service discovery handles it — same code works locally and in production. See [Service discovery](/docs/fundamentals/services-and-networking/service-discovery). |
| **"Works on my machine"** | Different team members have different ports, different configs | One AppHost definition, everyone runs the same setup. See [First app tutorial](/docs/get-started/first-app). |
| **Debugging** | Attach debugger to each service individually | Debug your entire stack with a single . |
| **Finding logs** | Check 5 different terminal windows | Aspire Dashboard shows all logs in one place. See [Dashboard overview](/dashboard/overview). |
| **Adding a database** | Install locally, configure connection, hope versions match | `builder.AddPostgres("db")` — containerized, versioned, consistent. See [Integrations](/integrations/overview). |
| **Working across languages** | Stitch together separate scripts and conventions for C#, Node.js, Python, and more | Model the same distributed app in a single AppHost, including multi-language stacks. See [Multi-language architecture](/docs/architecture/multi-language-architecture). |
| **Using AI coding agents** | Agents only see fragments of your setup and guess at runtime state | `aspire agent init` gives agents structured access to your app model, logs, traces, and resources. See [Use AI coding agents](/docs/get-started/ai-coding-agents). |

### Before and after

**Without Aspire**, your startup routine might look like:


<Steps>
<Step stepNumber="1">
Start the database container

```bash frame="terminal"
docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=secret postgres:15
```

</Step>
<Step stepNumber="2">
Start the C# API service

```bash frame="terminal"
cd api && dotnet run
```

</Step>
<Step stepNumber="3">
Start the Python worker

```bash frame="terminal"
cd worker && source .venv/bin/activate && python main.py
```

</Step>
<Step stepNumber="4">
Start the JavaScript frontend

```bash frame="terminal"
cd frontend && npm run dev
```

</Step>
<Step stepNumber="5">
Manually verify they can all connect...

Frustration ensues. 🙁

</Step>
</Steps>

**With Aspire**, it's just:

```bash
aspire run
```

And your AppHost defines the whole system in code, even for multi-language stacks:


**C# AppHost**


```csharp title="AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db");

var api = builder.AddProject<Projects.Api>("api")
    .WithReference(db);

var worker = builder.AddPythonApp("worker", "../worker", "main.py")
    .WithReference(db);

builder.AddNpmApp("frontend", "../frontend")
    .WithReference(api);

builder.Build().Run();
```


**TypeScript AppHost**


```typescript title="apphost.ts"

const builder = await createBuilder();

const db = await builder.addPostgres("db");

const api = await builder
    .addNodeApp("api", "../api", "server.js")
    .withReference(db);

const worker = await builder
    .addPythonApp("worker", "../worker", "main.py")
    .withReference(db);

await builder
    .addViteApp("frontend", "../frontend")
    .withReference(api);

await builder.build().run();
```


> [!TIP] The real win
> Your AppHost becomes the single source of truth for your application's architecture. New team members can understand your entire system by reading one file.

## Key benefits

- **Compose in code**: Define resources, references, and dependencies in an AppHost instead of spreading architecture across scripts and config files.
- **Debug the whole system**: Launch your app with a single command, view logs and traces in one place, and debug the full stack with the Aspire Dashboard.
- **Deploy the same model**: Use the same architecture definition for local development and deployment targets such as Kubernetes, cloud platforms, or your own infrastructure.
- **Work across languages**: Use the same distributed application model whether your services are written in C#, Node.js, Python, or other supported languages.
- **Enable AI agents**: Give coding agents structured access to your application through Aspire CLI workflows, skill files, and MCP.
- **Extend what you run**: Add databases, caches, messaging systems, and cloud services through Aspire integrations.

## How Aspire works

Aspire uses a **code-first approach** to define your application's architecture. Instead of managing complex configuration files, you describe services, databases, frontends, and dependencies directly in code.

That AppHost becomes the model for how your distributed app is composed:

- **Resources**: Add projects, executables, containers, databases, caches, and cloud services.
- **Relationships**: Express references, dependency order, and service discovery connections.
- **Execution**: Let Aspire orchestrate local startup, environment wiring, health checks, and diagnostics.
- **Operations**: Use the same model through the CLI, Dashboard, and AI-agent tooling.

Aspire keeps that model consistent even when the AppHost language changes. A TypeScript AppHost uses a guest/host architecture to access the same orchestration engine, integrations, service discovery, and diagnostics as a C# AppHost.

This approach provides several advantages:

- **Type Safety**: Catch configuration errors at compile time.
- **Statement Completion Support**: Get code completion and documentation while defining your architecture.
- **Version Control**: Your infrastructure definition lives alongside your code.
- **Refactoring**: Use familiar development tools to restructure your application architecture.
- **Shared Tooling**: Developers and AI agents can inspect the same resources, logs, and traces.

> [!NOTE] Same model, different AppHost language
> Whether you write your AppHost in C# or TypeScript, Aspire uses the same underlying orchestration model for resources, service discovery, diagnostics, and deployment.

## Development vs production

Aspire bridges the gap between development and production environments:

- **Development**: Run services locally with automatic dependency management, service discovery, and centralized diagnostics.
- **Production**: Deploy the same architecture definition to cloud platforms, Kubernetes, or your own environment.
- **Consistency**: Keep your local topology aligned with what you intend to deploy.
- **Automation**: Let human developers and AI agents work from the same source of truth.

Aspire doesn't replace your existing deployment workflows—it enhances them by providing a consistent way to define and manage your application architecture across environments.

> [!TIP] Working with agents and multi-language stacks
> For AI-assisted workflows, run `aspire agent init`. For guest-language AppHosts, see [Multi-language architecture](/docs/architecture/multi-language-architecture).
