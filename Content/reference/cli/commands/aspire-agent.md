---
title: aspire agent command
description: Learn about the aspire agent command and its usage. This command driver is used to manage AI agent integrations and MCP (Model Context Protocol) server operations.
order: 113
---



## Name

`aspire agent` - Manage AI agent integrations.

## Synopsis

```bash title="Aspire CLI"
aspire agent [command] [options]
```

## Description

The `aspire agent` command manages AI agent integrations for Aspire. This includes starting the MCP (Model Context Protocol) server and initializing agent environment configurations. The MCP server enables AI assistants and development tools to interact with your Aspire AppHost, providing capabilities such as resource management, diagnostics, and observability.

## Options

The following options are available:

- - - - - - ## Commands

The following commands are available:

| Command                                      | Status | Function                                                             |
| -------------------------------------------- | ------ | -------------------------------------------------------------------- |
| [`aspire agent mcp`](/reference/cli/commands/aspire-agent-mcp)   | Stable | Start the MCP (Model Context Protocol) server.                       |
| [`aspire agent init`](/reference/cli/commands/aspire-agent-init) | Stable | Initialize MCP server configuration for detected agent environments. |

## See also

- [aspire agent mcp command](/reference/cli/commands/aspire-agent-mcp)
- [aspire agent init command](/reference/cli/commands/aspire-agent-init)
