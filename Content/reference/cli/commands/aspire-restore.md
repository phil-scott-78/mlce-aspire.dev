---
title: aspire restore command
description: Learn about the aspire restore command and its usage. This command restores dependencies and generates SDK code for an apphost.
order: 148
---



## Name

`aspire restore` - Restore dependencies and generate SDK code for an apphost.

## Synopsis

```bash title="Aspire CLI"
aspire restore [options]
```

## Description

The `aspire restore` command restores the AppHost project and generates the SDK code that Aspire uses for resources, integrations, and tooling. Use it when you want to make restore an explicit step in automation or validate that the AppHost can restore cleanly without starting it.

This command is useful in CI/CD pipelines, after adding or updating integrations, or before running build, publish, and deployment workflows.

## Options

The following options are available:

- - - - - - - ## Examples

- Restore the AppHost discovered from the current directory:

  ```bash title="Aspire CLI"
  aspire restore
  ```

- Restore a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire restore --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

## See also

- [aspire run](/reference/cli/commands/aspire-run)
- [aspire start](/reference/cli/commands/aspire-start)
- [aspire publish](/reference/cli/commands/aspire-publish)
