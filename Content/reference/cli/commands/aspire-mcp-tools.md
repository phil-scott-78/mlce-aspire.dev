---
title: aspire mcp tools command
description: Learn about the aspire mcp tools command and its usage. This command lists MCP tools exposed by running resources.
order: 139
---



## Name

`aspire mcp tools` - List MCP tools exposed by running resources.

## Synopsis

```bash title="Aspire CLI"
aspire mcp tools [options]
```

## Description

The `aspire mcp tools` command lists the MCP tools exposed by resources in a running AppHost. Use it to discover which resources expose tools and which tool names you can pass to `aspire mcp call`.

When executed without the `--apphost` option, the command:


<Steps>
<Step stepNumber="1">
Scans for all running AppHost processes.

</Step>
<Step stepNumber="2">
If multiple AppHosts are running within the current directory scope, prompts you to select which one to target.

</Step>
<Step stepNumber="3">
If only one AppHost is running in scope, connects to it directly.

</Step>
<Step stepNumber="4">
If no in-scope AppHosts are found but out-of-scope AppHosts exist, displays all running AppHosts for selection.

</Step>
</Steps>

## Options

The following options are available:

- - **`--format <Json|Table>`**

  Output format. Use `Json` for machine-readable output suitable for scripting and automation. Defaults to `Table`.

- - - - - - ## Examples

- List MCP tools for the current AppHost:

  ```bash title="Aspire CLI"
  aspire mcp tools
  ```

- Output the tool list as JSON:

  ```bash title="Aspire CLI"
  aspire mcp tools --format Json
  ```

- Target a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire mcp tools --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

## See also

- [aspire mcp](/reference/cli/commands/aspire-mcp)
- [aspire mcp call](/reference/cli/commands/aspire-mcp-call)
