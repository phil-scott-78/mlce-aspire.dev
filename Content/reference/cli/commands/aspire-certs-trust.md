---
title: aspire certs trust command
description: Learn about the aspire certs trust command and its usage. This command trusts the HTTPS development certificate, creating one if necessary.
order: 120
---



## Name

`aspire certs trust` - Trust the HTTPS development certificate, creating one if necessary.

## Synopsis

```bash title="Aspire CLI"
aspire certs trust [options]
```

## Description

The `aspire certs trust` command trusts the HTTPS development certificate used by Aspire. If a certificate doesn't already exist, Aspire creates one before trusting it.

Use this command when you want to prepare a machine for Aspire local development or re-establish trust after cleaning development certificates.

## Options

The following options are available:

- - - - - - ## Examples

- Trust the Aspire HTTPS development certificate:

  ```bash title="Aspire CLI"
  aspire certs trust
  ```

## See also

- [aspire certs](/reference/cli/commands/aspire-certs)
- [aspire certs clean](/reference/cli/commands/aspire-certs-clean)
