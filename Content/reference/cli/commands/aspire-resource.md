---
title: aspire resource command
description: Learn about the aspire resource command and its usage. This command executes a command on a resource in a running apphost.
order: 147
---



## Name

`aspire resource` - Execute a command on a resource.

## Synopsis

```bash title="Aspire CLI"
aspire resource <resource> <command> [options]
```

## Description

The `aspire resource` command executes a command exposed by a resource in a running AppHost. Use it to trigger actions such as `start`, `stop`, or `restart` without opening the dashboard.

The available command names depend on the selected resource. If a resource doesn't expose the command you specify, Aspire returns an error.

> [!NOTE]
> `aspire resource` doesn't expose a fixed set of CLI subcommands in `--help`.
> Instead, `<command>` is a positional argument whose valid values come from the
> selected resource at runtime. This is why the CLI reference documents
> `aspire resource` as a single command page instead of separate pages such as
> `aspire resource start`.

When executed without the `--apphost` option, the command:


<Steps>
<Step stepNumber="1">
Scans for all running AppHost processes.

</Step>
<Step stepNumber="2">
If multiple AppHosts are running within the current directory scope, prompts you to select which one to target.

</Step>
<Step stepNumber="3">
If only one AppHost is running in scope, connects to it directly.

</Step>
<Step stepNumber="4">
If no in-scope AppHosts are found but out-of-scope AppHosts exist, displays all running AppHosts for selection.

</Step>
</Steps>

## Arguments

- **`<resource>`**

  The name of the resource to execute the command on.

- **`<command>`**

  The name of the resource command to execute, such as `start`, `stop`, or `restart`.

## Options

The following options are available:

- - - - - - - ## Examples

- Restart a resource in the current AppHost:

  ```bash title="Aspire CLI"
  aspire resource cache restart
  ```

- Stop a specific resource:

  ```bash title="Aspire CLI"
  aspire resource worker stop
  ```

- Target a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire resource api restart --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

## See also

- [aspire describe](/reference/cli/commands/aspire-describe)
- [aspire logs](/reference/cli/commands/aspire-logs)
- [aspire wait](/reference/cli/commands/aspire-wait)
