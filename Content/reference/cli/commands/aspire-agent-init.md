---
title: aspire agent init command
description: Learn about the aspire agent init command and its usage. This command initializes MCP server configuration for detected agent environments.
order: 114
---



## Name

`aspire agent init` - Initialize MCP server configuration for detected agent environments.

## Synopsis

```bash title="Aspire CLI"
aspire agent init [options]
```

## Description

The `aspire agent init` command initializes the MCP (Model Context Protocol) server configuration for your development environment. It detects agent environments (such as VS Code with GitHub Copilot, or other MCP-compatible tools) and creates the necessary configuration files to enable MCP integration.

The command performs the following:

- Detects supported agent environments in the current workspace
- Creates or updates MCP configuration files for each detected environment
- Sets up the connection between the agent and the Aspire MCP server

## Options

The following options are available:

- - - - - - ## Examples

- Initialize MCP configuration for detected agent environments:

  ```bash title="Aspire CLI"
  aspire agent init
  ```

## See also

- [aspire agent command](/reference/cli/commands/aspire-agent)
- [aspire agent mcp command](/reference/cli/commands/aspire-agent-mcp)
