---
title: aspire secret path command
description: Learn about the aspire secret path command and its usage. This command shows the file path where user secrets are stored for an Aspire AppHost.
order: 154
---



## Name

`aspire secret path` - Show the secrets file path.

## Synopsis

```bash title="Aspire CLI"
aspire secret path [options]
```

## Description

The `aspire secret path` command displays the full file path to the user secrets file for an Aspire AppHost. This is useful when you need to directly inspect or edit the secrets file, or when troubleshooting secrets resolution.

## Options

The following options are available:

- **`--apphost <apphost>`**

  The path to the Aspire AppHost project file.

- - - - - - ## Examples

- Show the secrets file path for the current AppHost:

  ```bash title="Aspire CLI"
  aspire secret path
  ```

- Show the secrets file path for a specific AppHost:

  ```bash title="Aspire CLI"
  aspire secret path --apphost ./src/MyApp.AppHost/MyApp.AppHost.csproj
  ```
