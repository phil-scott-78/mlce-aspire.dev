---
title: 'Aspireify an existing app with a C# AppHost'
description: 'Learn how to add Aspire orchestration to an existing application by using a C# AppHost.'
order: 24
---



This guide shows you how to add Aspire orchestration to an existing application while using a C# AppHost (`AppHost.cs` or `apphost.cs`). Choose the app stack you're bringing to Aspire, then initialize the AppHost, register your resources, and run everything locally with Aspire.

> [!NOTE] Definition
> **Aspireify** (verb): To transform an existing application into a distributed, observable, and orchestrated system by adding Aspire—no cape required, just a few commands!

## Why add Aspire to an existing app?

As distributed applications grow, coordinating multiple services becomes a tangled web of configuration files, hard-coded URLs, and fragile startup scripts. You're juggling connection strings across environments, manually wiring service dependencies, and struggling to trace issues across your microservices. Development setup becomes a ritual of precision—start the database, then the cache, then service A before service B—and any misstep sends you back to square one.

Aspire cuts through this complexity with a unified orchestration layer that treats your entire application as a cohesive system. Define your services and their relationships once in code ([the AppHost](/docs/fundamentals/core-concepts/app-host)), and Aspire handles service discovery, injects configuration automatically, and provides a dashboard with logs, traces, and metrics out of the box. Whether you're orchestrating C#, Python, or JavaScript services—or all three together—you get the same consistent experience from development through deployment.

The best part? You can adopt Aspire incrementally. Start with orchestration, add observability when you're ready, integrate external services as needed. Your existing codebase stays largely unchanged, and you can reverse course if Aspire isn't the right fit.

## Prerequisites

Before you begin, ensure you have the following prerequisites installed based on your application's language:

### Common requirements

- [.NET SDK 10.0 or later](/docs/get-started/setup-and-tooling/prerequisites) — Required for the Aspire AppHost, regardless of your application's language.
- [Aspire CLI installed](/docs/get-started/setup-and-tooling/install-cli) — For orchestration and deployment commands.
- An existing application to add Aspire to.

### Language-specific requirements

> [!TIP] Multi-language applications
> If your application uses multiple languages, ensure you have all the relevant
>   prerequisites installed. The Aspire AppHost can orchestrate services across
>   different languages simultaneously.

## Overview of the process

Adding Aspire to an existing application follows these key steps:


<Steps>
<Step stepNumber="1">
**Initialize Aspire support** — Use `aspire init` to add the AppHost (orchestration layer)

</Step>
<Step stepNumber="2">
**Add your applications** — Register C#, Python, and JavaScript applications in the AppHost

</Step>
<Step stepNumber="3">
**Configure telemetry (optional)** — Add OpenTelemetry instrumentation for observability

</Step>
<Step stepNumber="4">
**Add integrations (optional)** — Connect to databases, caches, and message queues

</Step>
<Step stepNumber="5">
**Run and verify** — Test your application with Aspire orchestration

</Step>
</Steps>

## Initialize Aspire support

The `aspire init` command is the starting point for adding Aspire to your existing application. This command analyzes your project structure and adds an AppHost that orchestrates your services.


<Steps>
<Step stepNumber="1">
Navigate to your project directory:

</Step>
<Step stepNumber="2">
Run `aspire init` to initialize Aspire support:

```bash title="Initialize Aspire"
aspire init
```

The `aspire init` command runs in **interactive mode** by default. It will:

</Step>
</Steps>

### What does `aspire init` create?

This is your starting point. In the next section, you'll add your applications and configure their relationships.

## Add your applications to the AppHost

Once you have an AppHost, you need to register your existing applications. First, install the appropriate hosting packages for your application types, then add your resources to the AppHost.

### Install hosting packages

### Add C# project references

### Model your resources in the AppHost

> [!TIP]
> You can mix and match languages! The examples above show single-language
>   scenarios, but Aspire excels at orchestrating multi-language applications. Add
>   C#, Python, and JavaScript resources to the same AppHost to create truly
>   cross-language solutions.

### Connect services

The `WithReference` calls establish service dependencies and enable service discovery:

When you call `WithReference`, you're declaring a dependency between resources. Aspire handles the rest—automatically injecting configuration at runtime and during deployment so your services can communicate seamlessly, whether running locally or in production.

## Add telemetry configuration (optional)

## Add integrations (optional)

Aspire provides integration libraries that simplify working with common services like Redis, PostgreSQL, RabbitMQ, and more. These integrations handle configuration, health checks, and telemetry automatically.

When you add a hosting integration (like Redis) to your AppHost and reference it from a dependent resource, Aspire automatically injects the necessary configuration. This includes connection strings, URLs, environment variables, and other service-specific settings—eliminating manual connection string management across your services.


<Steps>
<Step stepNumber="1">
Identify which services in your application could benefit from Aspire integrations. Common candidates include:
- Databases (PostgreSQL, SQL Server, MongoDB)
- Caching (Redis, Valkey)
- Messaging (RabbitMQ, Azure Service Bus, Kafka)
- Storage (Azure Blob Storage, AWS S3)

</Step>
<Step stepNumber="2">
Use the `aspire add` command to add integration packages to your AppHost:

```bash title="Add a Redis integration"
aspire add redis
```

This command adds the necessary NuGet packages and helps you configure the integration.

</Step>
<Step stepNumber="3">
Update your AppHost to reference the integration and share it across all your services:

```csharp title="apphost.cs — Add Redis integration for multi-language apps"
var builder = DistributedApplication.CreateBuilder(args);

// Add Redis resource
var cache = builder.AddRedis("cache");

// Share Redis with .NET API
var api = builder.AddProject<Projects.YourApi>("api")
    .WithReference(cache)
    .WithHttpHealthCheck("/health");

// Share Redis with Python worker
var pythonWorker = builder.AddPythonApp("worker", "../python-worker", "worker.py")
    .WithReference(cache);  // Python gets CACHE_HOST, CACHE_PORT env vars

// Share Redis with Node.js service
var nodeService = builder.AddNodeApp("service", "../node-service", "index.js")
    .WithReference(cache);  // Node.js gets CACHE_HOST, CACHE_PORT env vars

builder.Build().Run();
```

</Step>
<Step stepNumber="4">
Configure the integration in each language:

</Step>
</Steps>

## Run and verify

Now that you've added Aspire to your application, it's time to run it and see the orchestration in action.


<Steps>
<Step stepNumber="1">
From your solution directory, run the application using the Aspire CLI:

```bash title="Run your application with Aspire"
aspire run
```

The Aspire CLI will:
- Find your AppHost (file-based `apphost.cs`)
- Build your solution
- Launch all orchestrated services
- Start the Aspire dashboard

</Step>
<Step stepNumber="2">
The dashboard URL will appear in your terminal output:

```bash title="Example output" mark={4}
🔍  Finding apphosts...
apphost.cs

Dashboard:  https://localhost:17068/login?t=ea559845d54cea66b837dc0ff33c3bd3

     Logs:  ~/.aspire/cli/logs/apphost-13024-2025-11-19-12-00-00.log

            Press CTRL+C to stop the apphost and exit.
```

</Step>
<Step stepNumber="3">
Open the dashboard in your browser using the provided URL. You'll see:
- All your orchestrated resources and their status
- Real-time logs from each service
- Traces and metrics for observability
- Environment variables and configuration

</Step>
<Step stepNumber="4">
Verify that your services are running correctly by:
- Checking the **Resources** page for service health
- Accessing your application endpoints
- Reviewing logs and traces in the dashboard

</Step>
<Step stepNumber="5">
Stop the application by pressing in your terminal.

</Step>
</Steps>


> [!TIP]
> The [Aspire dashboard](/dashboard/explore) provides valuable insights even if
>   you haven't added ServiceDefaults or integrations yet. You'll see basic
>   information about your services, their startup order, and their endpoints.

### Bonus content

If you're currently using Docker Compose to orchestrate your services, Aspire can replace it while providing additional benefits:


**Docker Compose approach**
```yaml title="docker-compose.yml" showLineNumbers
services:
   postgres:
      image: postgres:latest
      environment:
         - POSTGRES_PASSWORD=postgres
         - POSTGRES_DB=mydb
      ports:
         - "5432:5432"
   
   api:
      build: ./api
      ports:
         - "8080:8080"
      environment:
         - DATABASE_URL=postgres://postgres:postgres@postgres:5432/mydb
      depends_on:
         - postgres
   
   web:
      build: ./web
      ports:
         - "3000:3000"
      environment:
         - API_URL=http://api:8080
      depends_on:
         - api
```

**Aspire's approach**

```csharp title="apphost.cs" showLineNumbers
var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("postgres")
    .AddDatabase("mydb");

var api = builder.AddProject<Projects.Api>("api")
    .WithReference(db);

var web = builder.AddProject<Projects.Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();

```


**Key advantages of Aspire over Docker Compose:**

- **Literally, less verbose** — Yet, more expressive and powerful.
- **No manual URL configuration** — Services discover each other automatically.
- **Type-safe references** — Compile-time checking for service dependencies.
- **Built-in dashboard** — Observability without additional tools like Prometheus/Grafana.
- **Development and deployment** — Same orchestration code works locally and can generate Docker Compose or deploy anywhere.
- **Integration libraries** — Pre-built support for databases, caches, message queues with best practices.
- **Language-agnostic** — Works with C#, Python, JavaScript, many more, and containerized services.

> [!NOTE]
> Aspire can even generate Docker Compose files for you! When you deploy with `aspire deploy`, you can target Docker Compose as a deployment option. See [Deploy your first Aspire app](/docs/get-started/deploy-first-app/deploy-first-app-csharp) for more details.

## Next steps

Congratulations! You've successfully added Aspire to your existing application. Here are some recommended next steps:

- **Add more integrations** — Explore [Aspire integrations](/integrations/gallery) to simplify working with databases, caches, and message queues
- **Configure observability** — Enhance your application's telemetry:
  - **Deploy your app** — Follow the [Deploy your first app](/docs/get-started/deploy-first-app/deploy-first-app-csharp) tutorial to deploy your Aspire application
- **Use the VS Code extension** — Run, debug, and manage integrations from within VS Code with the [Aspire extension](/docs/get-started/setup-and-tooling/aspire-vscode-extension)
- **Explore the dashboard** — Learn more about the [Aspire dashboard](/dashboard/overview) and its features
- **Learn about multi-language features**:
  - [Python support in Aspire](/docs/whats-new/aspire-13/#python-as-a-first-class-citizen)
  - [JavaScript support in Aspire](/docs/whats-new/aspire-13/#javascript-as-a-first-class-citizen)
