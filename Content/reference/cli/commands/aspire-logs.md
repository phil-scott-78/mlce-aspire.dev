---
title: aspire logs command
description: Learn about the aspire logs command and its usage. This command displays logs from resources in a running apphost.
order: 136
---



## Name

`aspire logs` - Display logs from resources in a running apphost.

## Synopsis

```bash title="Aspire CLI"
aspire logs [<resource>] [options]
```

## Description

The `aspire logs` command displays console logs from resources in a running AppHost. You can view logs for all resources, filter to a specific resource, follow new log entries in real time, and choose table or JSON output for automation.

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

  The name of the resource to get logs for. If not specified, logs from all resources are shown.

## Options

The following options are available:

- - **`-f, --follow`**

  Stream logs in real time as they are written.

- **`--format <Json|Table>`**

  Output format. Use `Json` for machine-readable output suitable for scripting and automation. Defaults to `Table`.

- **`-n, --tail <tail>`**

  Number of lines to show from the end of the logs.

- **`-t, --timestamps`**

  Show timestamps for each log line.

- - - - - - ## Examples

- Show logs for all resources:

  ```bash title="Aspire CLI"
  aspire logs
  ```

- Show logs for a specific resource:

  ```bash title="Aspire CLI"
  aspire logs webfrontend
  ```

- Follow logs in real time:

  ```bash title="Aspire CLI"
  aspire logs --follow
  ```

- Show the last 100 log lines with timestamps:

  ```bash title="Aspire CLI"
  aspire logs apiservice --tail 100 --timestamps
  ```

- Output logs as JSON:

  ```bash title="Aspire CLI"
  aspire logs --format Json
  ```

## See also

- [aspire describe](/reference/cli/commands/aspire-describe)
- [aspire export](/reference/cli/commands/aspire-export)
- [aspire otel](/reference/cli/commands/aspire-otel)
