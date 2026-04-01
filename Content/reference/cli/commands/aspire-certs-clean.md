---
title: aspire certs clean command
description: Learn about the aspire certs clean command and its usage. This command removes all HTTPS development certificates.
order: 119
---



## Name

`aspire certs clean` - Remove all HTTPS development certificates.

## Synopsis

```bash title="Aspire CLI"
aspire certs clean [options]
```

## Description

The `aspire certs clean` command removes all HTTPS development certificates. Use it when you want to reset local certificate state before trusting a new certificate or while troubleshooting HTTPS-related local development issues.

## Options

The following options are available:

- - - - - - ## Examples

- Remove all HTTPS development certificates:

  ```bash title="Aspire CLI"
  aspire certs clean
  ```

## See also

- [aspire certs](/reference/cli/commands/aspire-certs)
- [aspire certs trust](/reference/cli/commands/aspire-certs-trust)
