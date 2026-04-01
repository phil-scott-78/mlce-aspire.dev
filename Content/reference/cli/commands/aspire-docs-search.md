---
title: aspire docs search command
description: Learn about the aspire docs search command and its usage. This command searches Aspire documentation by keywords.
order: 132
---



## Name

`aspire docs search` - Search Aspire documentation by keywords.

## Synopsis

```bash title="Aspire CLI"
aspire docs search <query> [options]
```

## Description

The `aspire docs search` command performs a keyword-based search across the Aspire documentation on [aspire.dev](https://aspire.dev). Results are ranked by relevance and include the page title, slug, matching section, and a relevance score.

The slugs returned in search results can be passed to the [`aspire docs get`](/reference/cli/commands/aspire-docs-get) command to retrieve the full content of a matching page.

## Arguments

- **`<query>`**

  The search query. Use keywords to describe what you're looking for. For best results, include relevant terms such as API names, configuration keys, or feature names.

## Options

The following options are available:

- **`--format <Table|Json>`**

  Output format. Choose `Table` for a human-readable table or `Json` for machine-readable JSON output. Defaults to `Table`.

- **`-n, --limit <limit>`**

  Maximum number of search results to return. Defaults to `5`, with a maximum of `10`.

- - - - - - ## Examples

- Search for documentation about Redis:

  ```bash title="Aspire CLI"
  aspire docs search "redis"
  ```

- Search with a custom result limit:

  ```bash title="Aspire CLI"
  aspire docs search "service defaults" --limit 3
  ```

- Search and output results in JSON format:

  ```bash title="Aspire CLI"
  aspire docs search "deployment" --format Json
  ```

## See also

- [aspire docs command](/reference/cli/commands/aspire-docs)
- [aspire docs list command](/reference/cli/commands/aspire-docs-list)
- [aspire docs get command](/reference/cli/commands/aspire-docs-get)
