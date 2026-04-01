---
title: aspire mcp command
description: Learn about the aspire mcp command and its usage. This command interacts with MCP tools exposed by Aspire resources.
order: 137
---



## Name

`aspire mcp` - Interact with MCP tools exposed by Aspire resources.

## Synopsis

```bash title="Aspire CLI"
aspire mcp [command] [options]
```

## Description

The `aspire mcp` command lets you discover and call MCP (Model Context Protocol) tools exposed by resources in a running AppHost. Use it to inspect available tools and invoke them from the terminal.

> [!NOTE]
> The `aspire mcp` command interacts with tools exposed by running resources. To start the MCP server used by compatible AI agents, use [`aspire agent mcp`](/reference/cli/commands/aspire-agent-mcp).

## Options

The following options are available:

- - - - - - ## Commands

The following commands are available:

| Command                                     | Function                                       |
| ------------------------------------------- | ---------------------------------------------- |
| [`aspire mcp tools`](/reference/cli/commands/aspire-mcp-tools)  | List MCP tools exposed by running resources.   |
| [`aspire mcp call`](/reference/cli/commands/aspire-mcp-call)    | Call an MCP tool on a running resource.        |

## Examples

- List MCP tools exposed by the current AppHost:

  ```bash title="Aspire CLI"
  aspire mcp tools
  ```

- Call a tool on a running resource:

  ```bash title="Aspire CLI"
  aspire mcp call myresource mytool
  ```

## See also

- [aspire mcp tools](/reference/cli/commands/aspire-mcp-tools)
- [aspire mcp call](/reference/cli/commands/aspire-mcp-call)
- [aspire agent mcp](/reference/cli/commands/aspire-agent-mcp)
