---
title: aspire publish command
description: Learn about the aspire publish command and its usage. This command invokes resource publishers declared by the apphost to serialize resources to disk.
order: 146
---



## Name

`aspire publish` - Generates deployment artifacts for an Aspire AppHost project.

## Synopsis

```bash title="Aspire CLI"
aspire publish [options] [[--] <additional arguments>...]
```

## Description

The `aspire publish` command publishes resources by serializing them to disk. When this command is run, Aspire executes publishing pipeline steps that have been registered in the app model (for example, by environment resources). These steps serialize resources so that they can be consumed by deployment tools.

The command performs the following steps to run an Aspire AppHost:

- Creates or updates the rooted `aspire.config.json` file and records the
  selected AppHost there. Legacy `.aspire/settings.json` files are still read
  during migration.
- Installs or verifies that Aspire's local hosting certificates are installed and trusted.
- Builds the AppHost project and its resources.
- Starts the AppHost and its resources.
- Executes all publishing pipeline steps registered in the app model.

## Options

The following options are available:

- **`--`**

  Delimits arguments to `aspire publish` from arguments for the AppHost. All arguments after this delimiter are passed to the AppHost.

- - **`-o, --output-path`**

  The output path for the generated artifacts. Defaults to `../aspire-output` if not specified.

- **`--log-level`**

  Set the minimum log level for pipeline logging. Valid values are: `trace`, `debug`, `information`, `warning`, `error`, `critical`. The default is `information`.

- **`-e, --environment`**

  The environment to use for the operation. The default is `Production`.

- **`--include-exception-details`**

  Include exception details (stack traces) in pipeline logs.

- - - - - - ## Examples

- Search the current directory structure for AppHost projects to build and publish:

  ```bash title="Aspire CLI"
  aspire publish
  ```

- Publish a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire publish --apphost './projects/apphost/orchestration.AppHost.csproj'
  ```

- Publish a specific AppHost project with arguments:

  ```bash title="Aspire CLI"
  aspire publish --apphost './projects/apphost/orchestration.AppHost.csproj' -- -fast
  ```
