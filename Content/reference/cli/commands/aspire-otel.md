---
title: aspire otel command
description: Learn about the aspire otel command and its usage. This command is used to view OpenTelemetry data (logs, spans, traces) from a running apphost.
order: 141
---



## Name

`aspire otel` - View OpenTelemetry data (logs, spans, traces) from a running apphost.

## Synopsis

```bash title="Aspire CLI"
aspire otel [command] [options]
```

## Description

The `aspire otel` command provides subcommands for viewing OpenTelemetry data collected by the Aspire Dashboard from a running AppHost. You can view structured logs, distributed trace spans, and trace summaries directly from the terminal.

The data is retrieved from the Dashboard's telemetry API, so an AppHost must be running with the Dashboard enabled.

## Options

The following options are available:

- - - - - - ## Commands

The following commands are available:

| Command                                        | Function                                               |
| ---------------------------------------------- | ------------------------------------------------------ |
| [`aspire otel logs`](/reference/cli/commands/aspire-otel-logs)     | View structured logs from the Dashboard telemetry API. |
| [`aspire otel spans`](/reference/cli/commands/aspire-otel-spans)   | View spans from the Dashboard telemetry API.           |
| [`aspire otel traces`](/reference/cli/commands/aspire-otel-traces) | View traces from the Dashboard telemetry API.          |

## See also

- [aspire otel logs command](/reference/cli/commands/aspire-otel-logs)
- [aspire otel spans command](/reference/cli/commands/aspire-otel-spans)
- [aspire otel traces command](/reference/cli/commands/aspire-otel-traces)
