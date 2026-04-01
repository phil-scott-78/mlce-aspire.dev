---
title: aspire deploy command
description: Learn about the aspire deploy command and its usage. This command invokes the deploy pipeline step and any dependent steps registered in the app model.
order: 126
---



## Name

`aspire deploy` - Deploy a codebase orchestrated with Aspire to specified targets.

## Synopsis

```bash title="Aspire CLI"
aspire deploy [options] [[--] <additional arguments>...]
```

## Description

The `aspire deploy` command invokes the `deploy` pipeline step and any dependent steps (such as the `publish` step) registered in the app model.

The command performs the following steps to deploy an app orchestrated with Aspire:

- Creates or updates the rooted `aspire.config.json` file and records the
  selected AppHost there. Legacy `.aspire/settings.json` files are still read
  during migration.
- Installs or verifies that Aspire's local hosting certificates are installed and trusted.
- Builds the AppHost project and its resources.
- Starts the AppHost and its resources.
- Executes the `deploy` pipeline step, and any dependent steps, registered in the app model.

## Options

The following options are available:

- **`--`**

  Delimits arguments to `aspire publish` from arguments for the AppHost. All arguments after this delimiter are passed to the apphost.

- - **`-o, --output-path`**

  The optional output path for deployment artifacts. Defaults to the `aspire-output` folder relative to the apphost.

- **`--log-level`**

  Set the minimum log level for pipeline logging. Valid values are: `trace`, `debug`, `information`, `warning`, `error`, `critical`. The default is `information`.

- **`-e, --environment`**

  The environment to use for the operation. The default is `Production`.

- **`--include-exception-details`**

  Include exception details (stack traces) in pipeline logs.

- **`--clear-cache`**

  Clear the deployment cache associated with the current environment and do not save deployment state.

- - - - - - ## Examples

- Search the current directory structure for AppHost projects to build, publish, and deploy:

  ```bash title="Aspire CLI"
  aspire deploy
  ```

- Publish and deploy an Aspire apphost and its dependencies:

  ```bash title="Aspire CLI"
  aspire deploy --apphost './projects/apphost/orchestration.AppHost.csproj'
  ```

- Publish and deploy an Aspire AppHost with arguments:

  ```bash title="Aspire CLI"
  aspire deploy --apphost './projects/apphost/orchestration.AppHost.csproj' -- -fast
  ```
