---
title: What is the AppHost?
description: Learn how to define your application's architecture using Aspire's AppHost.
order: 31
---



Aspire's AppHost is the code-first place where you declare your application's services and their relationships. Instead of managing scattered configuration files, you describe the architecture in code. Aspire then handles local orchestration so you can focus on building features.

## Defining your architecture

Consider a simple three-tier architecture where the front end talks to an API, and the API talks to a database:

You can represent that architecture in an AppHost like this:

The AppHost models your distributed application declaratively. Each code sample above shows the same three-tier architecture—**PostgreSQL database**, **API service**, and **React front end**—but with different API implementations and, when available, different AppHost languages.

In all cases, the PostgreSQL database and React front end remain identical. Aspire resource references establish dependencies between resources, and the app model ensures services start in the correct order.

Aspire presents the same, consistent model regardless of the language or framework used: services, resources, and the connections between them.

### Dissecting the AppHost code

Below we highlight the key parts of a typical AppHost to explain what each step does.

The AppHost is the blueprint for your distributed application—Aspire manages the rest.

#### Adding a PostgreSQL resource

With the builder ready, define resources and services. The snippet below shows how to add a PostgreSQL server and a database:

> [!TIP]
> To also keep the container running between AppHost restarts (avoiding slow startup), add `.WithLifetime(ContainerLifetime.Persistent)`. See [Persistent container lifetimes](/docs/app-host/resource-types/persistent-containers) for details.

#### Adding an API resource and declaring a dependency

Next, register the API service and wire it to the PostgreSQL resource:

Now that the `api` service is defined, you can attach the front end.

#### Adding a front end resource

Register the front end project, declare its dependency on the API, and let the AppHost provide the API address automatically.

> [!NOTE]
> This example uses a Node.js (React) front end, but Aspire treats front ends as executable services—any language or framework works.

In short: define the backend first (DB → API), then point the UI at the API. The AppHost captures the dependency graph, connection flows, and startup order.

#### Configuration and networking

These dependencies and connections are automatically managed by Aspire. The AppHost generates configuration values like connection strings and endpoints, injecting them into services as needed. in the AppHost when you add resources, you name them (e.g., `db`, `api`, `front end`); Aspire uses these names for DNS resolution, so services can communicate using predictable addresses. Consuming services also rely on these names for configuration injection.

**How these resources communicate**


<Steps>
<Step stepNumber="1">
`pg` publishes a `ConnectionStringReference` (host, port, database, user, password)—a strongly typed bundle Aspire understands.

</Step>
<Step stepNumber="2">
`api` declares a dependency on that reference; Aspire injects the connection string into its config with a unique configuration-flow process that injects settings values, including secrets, parameters, and connection strings for both local runs and deployments.

</Step>
<Step stepNumber="3">
`api` then publishes an `EndpointReference` (its base URL) after its HTTP endpoint is allocated.

</Step>
<Step stepNumber="4">
`front end` depends on that endpoint; Aspire injects the API base URL so no hard-coded addresses are needed.

</Step>
</Steps>

> [!TIP]
> Aspire replaces ad-hoc run scripts, scattered environment variables, and fragile copy-pasted connection strings with a single, declarative source of truth. By modeling services, resources, and their dependencies in code, it delivers typed configuration (endpoints, credentials, connection strings) directly where needed—reducing setup drift, accelerating onboarding, and letting you evolve the architecture without rewriting glue.

## How the AppHost works

When you run the AppHost, Aspire performs these core responsibilities:


<Steps>
<Step stepNumber="1">
**Service discovery**: Aspire discovers services and resources declared in the AppHost.

</Step>
<Step stepNumber="2">
**Dependency resolution**: Services start in the correct order based on declared dependencies.

</Step>
<Step stepNumber="3">
**Configuration injection**: Connection strings, endpoints, and other config values are injected automatically.

</Step>
<Step stepNumber="4">
**Health monitoring**: Aspire observes service health and can restart services when necessary.

</Step>
</Steps>

## AppHost structure

The template AppHost is structured in the following ways:


  **File-based AppHost**

    **Project-based AppHost**

    **TypeScript AppHost**

    ## AppHost lifecycle events

You can hook into lifecycle events to run custom logic during startup and resource allocation.


<Steps>
<Step stepNumber="1">
`BeforeStartEvent`: Raised before the AppHost begins starting services.

</Step>
<Step stepNumber="2">
`AfterEndpointsAllocatedEvent`: Raised after endpoints are allocated for services.

</Step>
<Step stepNumber="3">
`AfterResourcesCreatedEvent`: Raised after all resources are created.

</Step>
</Steps>

## Best practices

- Keep the AppHost minimal to start; add complexity only as required.
- Define explicit dependencies with `.WithReference(...)` to make wiring obvious.
- Use separate configurations for development, testing, and production.
- Pick clear, descriptive names for resources to make debugging and logging easier.
