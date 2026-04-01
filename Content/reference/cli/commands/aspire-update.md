---
title: aspire update command
description: Learn about the aspire update command and its usage. This command helps you keep your Aspire projects current by automatically detecting and updating outdated packages and templates.
order: 158
---



## Name

`aspire update` - Update Aspire packages and templates in your project.

## Synopsis

```bash title="Aspire CLI"
aspire update [options]
```

## Description

The `aspire update` command helps you keep your Aspire projects current by automatically detecting and updating outdated packages and templates. It finds outdated Aspire NuGet packages while respecting channel configurations and intelligently handles complex dependency graphs.

The command performs the following:

- Detects outdated Aspire NuGet packages in your project
- Respects your configured Aspire channel (preview, stable, etc.)
- Resolves diamond dependencies to avoid duplicate updates
- Validates package compatibility before applying changes
- Provides colorized output with detailed summary of changes

> [!NOTE]
> The `aspire update` command requires an interactive terminal. The
>   `--non-interactive` flag is not currently supported for this command. In CI/CD
>   pipelines or other non-interactive environments, update Aspire packages
>   manually using `aspire add {package}` or equivalent tooling.

## Options

The following options are available:

- - **`--self`**

  Update the Aspire CLI itself to the latest version.

- **`--channel`**

  Channel to update to (`stable`, `staging`, `daily`).

- - - - - - ## Examples

- Analyze and update out-of-date Aspire packages and templates:

  ```bash title="Aspire CLI"
  aspire update
  ```

- Update a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire update --apphost './projects/apphost/orchestration.AppHost.csproj'
  ```
