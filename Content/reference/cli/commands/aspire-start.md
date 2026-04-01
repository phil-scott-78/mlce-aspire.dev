---
title: aspire start command
description: Learn about the aspire start command and its usage. This command starts an apphost in the background.
order: 156
---



## Name

`aspire start` - Start an apphost in the background.

## Synopsis

```bash title="Aspire CLI"
aspire start [options] [[--] <additional arguments>...]
```

## Description

The `aspire start` command starts an AppHost in the background and exits after the apphost is running. Use it when you want a detached apphost that you can inspect later with commands such as `aspire ps`, `aspire describe`, `aspire logs`, and `aspire stop`.

You can output detached startup details as a table or JSON, and you can pass additional arguments through to the AppHost by using the `--` delimiter.

## Options

The following options are available:

- **`--`**

  Delimits arguments to `aspire start` from arguments for the AppHost being run. All arguments after this delimiter are passed to the application.

- **`--no-build`**

  Do not build or restore the project before running. Use this option when you have already built the project and want to skip the restore and build step.

- - **`--format <Json|Table>`**

  Output format for detached apphost results. Use `Json` for machine-readable output suitable for scripting and automation.

- **`--isolated`**

  Run in isolated mode with randomized ports and isolated user secrets, allowing multiple instances of the same AppHost to run simultaneously.

- - - - - - ## Examples

- Start the AppHost discovered from the current directory:

  ```bash title="Aspire CLI"
  aspire start
  ```

- Start a specific AppHost in the background:

  ```bash title="Aspire CLI"
  aspire start --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

- Output detached startup details as JSON:

  ```bash title="Aspire CLI"
  aspire start --format Json
  ```

- Start the AppHost in isolated mode:

  ```bash title="Aspire CLI"
  aspire start --isolated
  ```

- Pass additional arguments to the AppHost:

  ```bash title="Aspire CLI"
  aspire start -- --environment Development
  ```

## See also

- [aspire run](/reference/cli/commands/aspire-run)
- [aspire ps](/reference/cli/commands/aspire-ps)
- [aspire logs](/reference/cli/commands/aspire-logs)
- [aspire stop](/reference/cli/commands/aspire-stop)
