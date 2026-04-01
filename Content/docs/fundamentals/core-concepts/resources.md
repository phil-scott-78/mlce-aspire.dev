---
title: Understanding resources
description: Learn about Aspire's resource model and how to work with different types of resources.
order: 32
---



Aspire makes it easy to define everything your app needs—frontends, APIs, databases, and more—using the **AppHost**. Just describe your resources in code, and Aspire handles the connections for you.

## Types of resources

Resources can include:

- **AI Services**: Large Language Models, AI endpoints, and cognitive services.
- **Caches**: Redis, in-memory caches, and distributed caching solutions.
- **Containers**: Docker containers for databases, message brokers, and other services.
- **Databases**: SQL Server, PostgreSQL, MySQL, MongoDB, and other data stores.
- **Executables**: Console applications, scripts, and background services.
- **Frameworks**: Web applications, APIs, and microservices built with various frameworks.
- **Messaging Services**: Service Bus, RabbitMQ, Kafka, and other messaging systems.
- **Projects**: C# projects, Node.js applications, Python services, and more.
- **Storage**: Blob storage, file systems, and cloud storage services.

## Resource dependencies

Resources work together through **explicit dependencies** that you define in your AppHost. When you model these dependencies, Aspire automatically handles the wiring, startup order, and connection management—letting you focus on building your app instead of managing infrastructure.

For example, if your API service depends on a database and configures a wait strategy, Aspire can automatically inject the connection string and ensure the database is started before the API service.


<Steps>
<Step stepNumber="1">
Start the database first.

</Step>
<Step stepNumber="2">
Wait for it to become healthy.

</Step>
<Step stepNumber="3">
Inject the connection string into your API service.

</Step>
<Step stepNumber="4">
Start your API service.

</Step>
</Steps>

## Resource configuration

You configure resources through a combination of **environment variables** (for mutable per-environment values), automatically injected **connection strings** that flow to dependent services, explicit **resource parameters** controlling ports, volumes, or other knobs, and custom **health checks** that let Aspire understand readiness beyond simple process status.

## Custom resources

Under-the-hood, every integration is either a **Container** or **Executable**, meaning you can add _any_ container image, codebase, script, or cloud resource to your AppHost. Creating reusable Aspire integrations is just like creating a reusable UI component. It can be as simple or complex as you need, and is fully shareable.

## Resource lifecycle

Aspire manages the complete lifecycle of your resources during **local development**:


<Steps>
<Step stepNumber="1">
**Discovery**: Resources are discovered and their dependencies are analyzed.

</Step>
<Step stepNumber="2">
**Startup**: Resources are started in dependency order, respecting their relationships.

</Step>
<Step stepNumber="3">
**Health Monitoring**: Resource health and readiness are continuously monitored.

</Step>
<Step stepNumber="4">
**Shutdown**: Resources are gracefully stopped when the application terminates.

</Step>
</Steps>


