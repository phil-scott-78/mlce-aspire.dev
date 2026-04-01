---
title: aspire secret delete command
description: Learn about the aspire secret delete command and its usage. This command deletes a user secret for an Aspire AppHost.
order: 151
---



## Name

`aspire secret delete` - Delete a secret.

## Synopsis

```bash title="Aspire CLI"
aspire secret delete <key> [options]
```

## Description

The `aspire secret delete` command deletes a user secret from the secrets store for an Aspire AppHost.

The command returns the following exit codes:

- `0`—The command succeeded.
- `3`—Failed to find the AppHost project.
- `4`—The specified secret key was not found.

## Arguments

The following arguments are available:

- **`key`**

  The secret key to delete.

## Options

The following options are available:

- **`--apphost <apphost>`**

  The path to the Aspire AppHost project file.

- - - - - - ## Examples

- Delete a secret:

  ```bash title="Aspire CLI"
  aspire secret delete Parameters:postgres-password
  ```

- Delete a secret for a specific AppHost:

  ```bash title="Aspire CLI"
  aspire secret delete Parameters:rabbitmq-password --apphost ./src/MyApp.AppHost/MyApp.AppHost.csproj
  ```
