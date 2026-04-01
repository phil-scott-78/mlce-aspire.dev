---
title: aspire certs command
description: Learn about the aspire certs command and its usage. This command manages HTTPS development certificates.
order: 118
---



## Name

`aspire certs` - Manage HTTPS development certificates.

## Synopsis

```bash title="Aspire CLI"
aspire certs [command] [options]
```

## Description

The `aspire certs` command manages the HTTPS development certificates used by Aspire during local development. Use it when you want to trust the development certificate explicitly or remove existing development certificates while troubleshooting HTTPS issues.

## Options

The following options are available:

- - - - - - ## Commands

The following commands are available:

| Command                                           | Function                                                         |
| ------------------------------------------------- | ---------------------------------------------------------------- |
| [`aspire certs clean`](/reference/cli/commands/aspire-certs-clean)    | Remove all HTTPS development certificates.                       |
| [`aspire certs trust`](/reference/cli/commands/aspire-certs-trust)    | Trust the HTTPS development certificate, creating one if needed. |

## Examples

- Trust the Aspire HTTPS development certificate:

  ```bash title="Aspire CLI"
  aspire certs trust
  ```

- Remove all local HTTPS development certificates:

  ```bash title="Aspire CLI"
  aspire certs clean
  ```

## See also

- [aspire doctor](/reference/cli/commands/aspire-doctor)
- [aspire run](/reference/cli/commands/aspire-run)
