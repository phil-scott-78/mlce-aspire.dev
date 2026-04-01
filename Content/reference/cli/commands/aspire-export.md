---
title: aspire export command
description: Learn about the aspire export command and its usage. This command exports telemetry and resource data to a zip file.
order: 134
---



## Name

`aspire export` - Export telemetry and resource data to a zip file.

## Synopsis

```bash title="Aspire CLI"
aspire export [<resource>] [options]
```

## Description

The `aspire export` command packages telemetry and resource data from a running AppHost into a zip file. Use it when you need to collect diagnostics for troubleshooting, share runtime state with a teammate, or preserve data from a running app for later analysis.

When a resource name is provided, the export is limited to data for that resource.

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

  Export data only for the specified resource.

## Options

The following options are available:

- - **`-o, --output <output>`**

  The output file path for the export zip file.

- - - - - - ## Examples

- Export diagnostics for the current AppHost:

  ```bash title="Aspire CLI"
  aspire export
  ```

- Export data for a specific resource:

  ```bash title="Aspire CLI"
  aspire export webfrontend
  ```

- Write the export to a specific file:

  ```bash title="Aspire CLI"
  aspire export --output '.\\artifacts\\aspire-export.zip'
  ```

- Export data from a specific AppHost project:

  ```bash title="Aspire CLI"
  aspire export --apphost './src/MyApp.AppHost/MyApp.AppHost.csproj'
  ```

## See also

- [aspire describe](/reference/cli/commands/aspire-describe)
- [aspire logs](/reference/cli/commands/aspire-logs)
- [aspire otel](/reference/cli/commands/aspire-otel)
