---
title: aspire secret list command
description: Learn about the aspire secret list command and its usage. This command lists all user secrets for an Aspire AppHost.
order: 153
---



## Name

`aspire secret list` - List all secrets.

## Synopsis

```bash title="Aspire CLI"
aspire secret list [options]
```

## Description

The `aspire secret list` command lists all user secrets stored for an Aspire AppHost. By default, secrets are displayed in a table format sorted alphabetically by key. If no secrets are configured, the command displays "No secrets configured."

## Options

The following options are available:

- **`--format <Json|Table>`**

  Output format. Use `Json` for machine-readable output suitable for scripting and automation. Defaults to `Table`.

- **`--apphost <apphost>`**

  The path to the Aspire AppHost project file.

- - - - - - ## Examples

- List all secrets for the current AppHost:

  ```bash title="Aspire CLI"
  aspire secret list
  ```

- List secrets in JSON format:

  ```bash title="Aspire CLI"
  aspire secret list --format json
  ```

- List secrets for a specific AppHost:

  ```bash title="Aspire CLI"
  aspire secret list --apphost ./src/MyApp.AppHost/MyApp.AppHost.csproj
  ```
