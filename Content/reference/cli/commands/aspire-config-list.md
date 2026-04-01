---
title: aspire config list command
description: Learn about the aspire config list command and its usage. This command lists all Aspire CLI config values.
order: 124
---



## Name

`aspire config list` - List all configuration values.

## Synopsis

```bash title="Aspire CLI"
aspire config list [options]
```

## Description

The `aspire config list` command lists the currently configured Aspire CLI
values by key name.

This command lists both global and project-scoped settings. If a project-scoped
setting overrides a global value, the project-scoped value is the one you
should expect to take effect.

## Options

The following options are available:

- **`--all`**

  Show all available feature flags that can be configured, including their
  default values and descriptions.

- - - - - - ## Examples

- List the currently configured values:

  ```bash title="Aspire CLI"
  aspire config list
  ```

- List all available feature flags and their defaults:

  ```bash title="Aspire CLI"
  aspire config list --all
  ```
