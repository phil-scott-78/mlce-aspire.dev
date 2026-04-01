---
title: aspire agent mcp command
description: Learn about the aspire agent mcp command and its usage. This command starts the MCP (Model Context Protocol) server.
order: 115
---



## Name

`aspire agent mcp` - Start the MCP (Model Context Protocol) server.

## Synopsis

```bash title="Aspire CLI"
aspire agent mcp [options]
```

## Description

The `aspire agent mcp` command starts the MCP (Model Context Protocol) server, which enables AI assistants and development tools to interact with your Aspire AppHost. The MCP server provides a standardized interface for resource management, diagnostics, and observability operations.

When the server is running, MCP-compatible clients can connect to it to:

- List and manage Aspire resources
- View structured logs and traces
- Execute resource commands (start, stop, restart)
- Query integration information

## Options

The following options are available:

- - - - - - ## Examples

- Start the MCP server:

  ```bash title="Aspire CLI"
  aspire agent mcp
  ```

## See also

- [aspire agent command](/reference/cli/commands/aspire-agent)
- [aspire agent init command](/reference/cli/commands/aspire-agent-init)
