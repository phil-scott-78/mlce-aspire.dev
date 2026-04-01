---
title: aspire add command
description: Learn about the aspire add command and its usage. This command adds an integration package to an Aspire AppHost project.
order: 112
---



## Name

`aspire add` - Add an integration to the Aspire project.

## Synopsis

```bash title="Aspire CLI"
aspire add [<integration>] [options]
```

## Description

The `aspire add` command searches for an integration package and adds it to the Aspire AppHost.

## Arguments

The following arguments are available:

- **`integration`**

  The name of the integration to add (for example: redis, postgres).

  If a partial name or invalid name is provided, the CLI searches NuGet for approximate matches and prints them in the terminal for the user to select. If no results are found, all packages are listed.

## Options

The following options are available:

- - **`-v, --version`**

  The version of the integration to add.

- **`-s, --source`**

  The NuGet source to use for the integration.

- - - - - - ## Examples

- Finds an AppHost project and lists all Aspire integration packages from NuGet:

  ```bash title="Aspire CLI"
  aspire add
  ```

- Finds an AppHost project and adds the **kafka** (Aspire.Hosting.Kafka) integration package:

  ```bash title="Aspire CLI"
  aspire add kafka --version 13.2.0
  ```
