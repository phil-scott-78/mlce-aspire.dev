---
title: aspire config delete command
description: Learn about the aspire config delete command and its usage. This command deletes an Aspire CLI config value by key name.
order: 122
---



## Name

`aspire config delete` - Delete a configuration value.

## Synopsis

```bash title="Aspire CLI"
aspire config delete <key> [options]
```

## Description

The `aspire config delete` command deletes a config value by key name.

The command returns the following exit codes:

- `0`&mdash;The command succeeded.
- `1`&mdash;The supplied key doesn't exist in the config file, or the config file is missing.

## Arguments

The following arguments are available:

- **`key`**

  The configuration key to delete.

## Options

The following options are available:

- **`-g, --global`**

  Delete the configuration value from user-scoped global Aspire configuration
  instead of the rooted `aspire.config.json` file.

- - - - - - 