---
title: aspire command
description: Learn about the aspire command (the generic driver for the Aspire CLI) and its usage.
order: 111
---



## Name

`aspire` - The generic driver for the Aspire CLI.

## Synopsis

To get information about the available commands and the environment:

```bash title="Aspire CLI"
aspire <command> [options]
```

## Description

The `aspire` command provides commands for working with Aspire projects. For example, `aspire run` runs your Aspire AppHost.

## Options

The following options are available when `aspire` is used by itself, without specifying a command. For example, `aspire --version`.

- **`-h, /h`**

  Show help and usage information.

- - - - - - ## Commands

The following commands are available:

| Command                                    | Status  | Function                                                              |
| ------------------------------------------ | ------- | --------------------------------------------------------------------- |
| [`aspire add`](/reference/cli/commands/aspire-add)             | Stable  | Add a hosting integration to the apphost.                             |
| [`aspire init`](/reference/cli/commands/aspire-init)           | Stable  | Initialize Aspire in an existing codebase.                            |
| [`aspire new`](/reference/cli/commands/aspire-new)             | Stable  | Create a new app from an Aspire starter template.                     |
| [`aspire ps`](/reference/cli/commands/aspire-ps)               | Stable  | List running apphosts.                                                |
| [`aspire restore`](/reference/cli/commands/aspire-restore)     | Stable  | Restore dependencies and generate SDK code for an apphost.            |
| [`aspire run`](/reference/cli/commands/aspire-run)             | Stable  | Run an apphost interactively for development.                         |
| [`aspire start`](/reference/cli/commands/aspire-start)         | Stable  | Start an apphost in the background.                                   |
| [`aspire stop`](/reference/cli/commands/aspire-stop)           | Stable  | Stop a running apphost.                                               |
| [`aspire update`](/reference/cli/commands/aspire-update)       | Preview | Update integrations in the Aspire project.                            |
| [`aspire resource`](/reference/cli/commands/aspire-resource)   | Stable  | Execute a command on a resource.                                      |
| [`aspire wait`](/reference/cli/commands/aspire-wait)           | Stable  | Wait for a resource to reach a target status.                         |
| [`aspire describe`](/reference/cli/commands/aspire-describe)   | Stable  | Describe resources in a running apphost.                              |
| [`aspire export`](/reference/cli/commands/aspire-export)       | Stable  | Export telemetry and resource data to a zip file.                     |
| [`aspire logs`](/reference/cli/commands/aspire-logs)           | Stable  | Display logs from resources in a running apphost.                     |
| [`aspire otel`](/reference/cli/commands/aspire-otel)           | Preview | View OpenTelemetry data from a running apphost.                       |
| [`aspire deploy`](/reference/cli/commands/aspire-deploy)       | Preview | Deploy an apphost to its deployment targets.                          |
| [`aspire do`](/reference/cli/commands/aspire-do)               | Preview | Execute a specific pipeline step and its dependencies.                |
| [`aspire publish`](/reference/cli/commands/aspire-publish)     | Preview | Generate deployment artifacts for an apphost.                         |
| [`aspire agent`](/reference/cli/commands/aspire-agent)         | Stable  | Manage AI agent environment configuration.                            |
| [`aspire cache`](/reference/cli/commands/aspire-cache)         | Stable  | Manage disk cache for CLI operations.                                 |
| [`aspire certs`](/reference/cli/commands/aspire-certs)         | Stable  | Manage HTTPS development certificates.                                |
| [`aspire config`](/reference/cli/commands/aspire-config)       | Stable  | Manage CLI configuration including feature flags.                     |
| [`aspire docs`](/reference/cli/commands/aspire-docs)           | Stable  | Browse and search Aspire documentation from aspire.dev.               |
| [`aspire doctor`](/reference/cli/commands/aspire-doctor)       | Stable  | Diagnose Aspire environment issues and verify setup.                  |
| [`aspire mcp`](/reference/cli/commands/aspire-mcp)             | Stable  | Interact with MCP tools exposed by Aspire resources.                  |
| [`aspire secret`](/reference/cli/commands/aspire-secret)       | Stable  | Manage apphost user secrets.                                          |

## Examples

- Create an Aspire solution from the template:

  ```bash title="Aspire CLI"
  aspire new aspire-starter
  ```

- Run an Aspire AppHost:

  ```bash title="Aspire CLI"
  aspire run
  ```
