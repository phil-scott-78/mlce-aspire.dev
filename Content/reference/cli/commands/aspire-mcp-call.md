---
title: aspire mcp call command
description: Learn about the aspire mcp call command and its usage. This command calls an MCP tool on a running resource.
order: 138
---



## Name

`aspire mcp call` - Call an MCP tool on a running resource.

## Synopsis

```bash title="Aspire CLI"
aspire mcp call <resource> <tool> [options]
```

## Description

The `aspire mcp call` command calls an MCP tool exposed by a resource in a running AppHost. Use `aspire mcp tools` first if you need to discover the available resources and tool names.

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

## Arguments

- **`<resource>`**

  The name of the resource that exposes the MCP tool.

- **`<tool>`**

  The name of the MCP tool to call.

## Options

The following options are available:

- **`-i, --input <input>`**

  JSON input to pass to the tool.

- - - - - - - ## Examples

- Call a tool on a running resource:

  ```bash title="Aspire CLI"
  aspire mcp call myresource mytool
  ```

- Pass JSON input to the tool call:

  ```bash title="Aspire CLI"
  aspire mcp call myresource mytool --input '{"message":"hello"}'
  ```

- Target a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire mcp call myresource mytool --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

## See also

- [aspire mcp](/reference/cli/commands/aspire-mcp)
- [aspire mcp tools](/reference/cli/commands/aspire-mcp-tools)
