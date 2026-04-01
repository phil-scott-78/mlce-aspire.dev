---
title: aspire docs get command
description: Learn about the aspire docs get command and its usage. This command retrieves the full content of an Aspire documentation page by its slug.
order: 130
---



## Name

`aspire docs get` - Get the full content of a documentation page by its slug.

## Synopsis

```bash title="Aspire CLI"
aspire docs get <slug> [options]
```

## Description

The `aspire docs get` command retrieves and displays the full content of a specific documentation page from [aspire.dev](https://aspire.dev). Use the [`aspire docs list`](/reference/cli/commands/aspire-docs-list) command to browse available pages, or the [`aspire docs search`](/reference/cli/commands/aspire-docs-search) command to find relevant content by keyword.

You can optionally filter the output to a specific section of the page by providing a section heading with the `--section` option.

## Arguments

- **`<slug>`**

  The slug of the documentation page to retrieve. For example, `redis-integration` or `getting-started`. Use the [`aspire docs list`](/reference/cli/commands/aspire-docs-list) or [`aspire docs search`](/reference/cli/commands/aspire-docs-search) commands to discover available slugs.

## Options

The following options are available:

- **`--section <section>`**

  Return only the specified section of the page. Provide the section heading to filter the output to that section and its content.

- **`--format <Table|Json>`**

  Output format. Choose `Table` for a human-readable table or `Json` for machine-readable JSON output. Defaults to `Table`.

- - - - - - ## Examples

- Get the full content of the Redis integration page:

  ```bash title="Aspire CLI"
  aspire docs get redis-integration
  ```

- Get only a specific section from a documentation page:

  ```bash title="Aspire CLI"
  aspire docs get redis-integration --section "Add Redis resource"
  ```

- Get page content in JSON format:

  ```bash title="Aspire CLI"
  aspire docs get getting-started --format Json
  ```

## See also

- [aspire docs command](/reference/cli/commands/aspire-docs)
- [aspire docs list command](/reference/cli/commands/aspire-docs-list)
- [aspire docs search command](/reference/cli/commands/aspire-docs-search)
