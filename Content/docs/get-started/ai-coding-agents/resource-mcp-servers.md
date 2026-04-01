---
title: Resource MCP servers
description: Learn how to expose MCP tools from Aspire resources and interact with them using the CLI.
order: 28
---



Aspire resources can expose their own MCP (Model Context Protocol) servers, enabling AI coding agents to interact directly with databases, APIs, and other services. For example, a PostgreSQL resource can expose SQL query tools, allowing an agent to run queries without leaving the conversation.

## How it works

When a resource is annotated with `WithMcpServer()` in the AppHost, Aspire discovers the MCP endpoint and makes its tools available in two ways:

- **Through the Aspire MCP server** — resource tools are automatically proxied alongside the built-in [Aspire MCP server](/docs/get-started/ai-coding-agents/aspire-mcp-server) tools. AI agents see them in their tool list without any extra configuration.
- **Through the CLI** — use `aspire mcp tools` and `aspire mcp call` to discover and invoke resource tools directly from the terminal.

## Add an MCP server to a resource

Use the `WithMcpServer()` extension method to declare that a resource hosts an MCP server:


**C#**


```csharp title="AppHost.cs"
#pragma warning disable ASPIREMCP001

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db")
    .AddDatabase("appdata");

// Expose PostgreSQL MCP tools (SQL queries, schema inspection, etc.)
db.WithPostgresMcp();

builder.Build().Run();
```


**TypeScript**


```typescript title="apphost.ts"

const builder = await createBuilder();

const db = await builder
    .addPostgres("db")
    .addDatabase("appdata");

// Expose PostgreSQL MCP tools (SQL queries, schema inspection, etc.)
await db.withPostgresMcp();

await builder.build().run();
```


> [!NOTE]
> `WithMcpServer()` and `WithPostgresMcp()` are experimental APIs. Suppress the `ASPIREMCP001` or `ASPIREPOSTGRES001` diagnostic to use them.

## Discover available tools

Use `aspire mcp tools` to list all MCP tools exposed by running resources:

```bash title="Aspire CLI"
aspire mcp tools
```

For machine-readable output including input schemas:

```bash title="Aspire CLI — JSON output"
aspire mcp tools --format Json
```

## Call a tool

Use `aspire mcp call` to invoke a specific tool on a resource:

```bash title="Aspire CLI"
aspire mcp call <resource> <tool> --input '{"key": "value"}'
```

For example, to run a SQL query on a PostgreSQL resource:

```bash title="Aspire CLI — Query example"
aspire mcp call appdata query --input '{"sql": "SELECT * FROM users LIMIT 5"}'
```

## Custom MCP server resources

You can add any container that implements the MCP protocol as an MCP-enabled resource. Use `WithMcpServer()` to tell Aspire where the MCP endpoint lives:


**C#**


```csharp title="AppHost.cs"
#pragma warning disable ASPIREMCP001

var builder = DistributedApplication.CreateBuilder(args);

// Add a custom MCP server container
var myMcpServer = builder.AddContainer("my-mcp", "myregistry/my-mcp-server")
    .WithHttpEndpoint(targetPort: 8080)
    .WithMcpServer("/mcp");

builder.Build().Run();
```


**TypeScript**


```typescript title="apphost.ts"

const builder = await createBuilder();

// Add a custom MCP server container
const myMcpServer = await builder
    .addContainer("my-mcp", "myregistry/my-mcp-server")
    .withHttpEndpoint({ targetPort: 8080 })
    .withMcpServer("/mcp");

await builder.build().run();
```


The `WithMcpServer()` method accepts an optional path and endpoint name:

- `WithMcpServer()` — uses the default HTTP endpoint at the root path
- `WithMcpServer("/mcp")` — uses the default HTTP endpoint at `/mcp`
- `WithMcpServer("/sse", endpointName: "https")` — uses a named endpoint at `/sse`

## See also

- [Use AI coding agents](/docs/get-started/ai-coding-agents) — set up your project for AI agents
- [Aspire MCP server](/docs/get-started/ai-coding-agents/aspire-mcp-server) — the built-in MCP server tools
- [ASPIREMCP001 diagnostic](/docs/diagnostics/aspiremcp001) — experimental MCP server API warning
- [ASPIREPOSTGRES001 diagnostic](/docs/diagnostics/aspirepostgres001) — experimental PostgreSQL MCP warning
