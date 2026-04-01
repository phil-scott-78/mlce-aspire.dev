---
title: SQLite Client integration reference
description: Learn how to use the Aspire SQLite Client integration to query SQLite databases from your Aspire projects.
order: 329
---



<IntegrationIcon Src="/assets/icons/sqlite-icon.png" Alt="SQLite logo">
<Badge text="⭐ Community Toolkit" size="small" />

To get started with the Aspire SQLite integrations, follow the [Get started with SQLite integrations](/integrations/databases/sqlite/sqlite-get-started) guide.
</IntegrationIcon>

This article includes full details about the Aspire SQLite Client integration, which allows you to connect to and interact with SQLite databases from your Aspire consuming projects.

## Installation

To get started with the Aspire SQLite client integration, install the [📦 CommunityToolkit.Aspire.Microsoft.Data.Sqlite](https://www.nuget.org/packages/CommunityToolkit.Aspire.Microsoft.Data.Sqlite) NuGet package in the client-consuming project, that is, the project for the application that uses the SQLite client. The SQLite client integration registers a [`SqliteConnection`](https://learn.microsoft.com/dotnet/api/microsoft.data.sqlite.sqliteconnection) instance that you can use to interact with SQLite.

<InstallPackage PackageName="CommunityToolkit.Aspire.Microsoft.Data.Sqlite" />

## Add SQLite client

In the `Program.cs` file of your client-consuming project, call the `AddSqliteConnection` extension method to register a `SqliteConnection` for use via the dependency injection container. The method takes a connection name parameter.

```csharp title="C# — Program.cs"
builder.AddSqliteConnection(connectionName: "sqlite");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the SQLite resource in the AppHost project. For more information, see [Add SQLite resource](/integrations/databases/sqlite/sqlite-host/#add-sqlite-resource).

After adding `SqliteConnection` to the builder, you can get the `SqliteConnection` instance using dependency injection. For example, to retrieve your connection object from an example service define it as a constructor parameter:

```csharp title="C# — ExampleService.cs"
public class ExampleService(SqliteConnection connection)
{
    // Use connection...
}
```

For more information on dependency injection, see [.NET dependency injection](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection).

## Add keyed SQLite client

There might be situations where you want to register multiple `SqliteConnection` instances with different connection names. To register keyed SQLite clients, call the `AddKeyedSqliteConnection` method:

```csharp title="C# — Program.cs"
builder.AddKeyedSqliteConnection(name: "chat");
builder.AddKeyedSqliteConnection(name: "queue");
```

> [!CAUTION]
> Your consuming app can only obtain two keyed SQLite clients like this, if the AppHost project configures two SQLite resources or named databases, one for `chat` and one for `queue`.

Then you can retrieve the `SqliteConnection` instances using dependency injection:

```csharp title="C# — ExampleService.cs"
public class ExampleService(
    [FromKeyedServices("chat")] SqliteConnection chatConnection,
    [FromKeyedServices("queue")] SqliteConnection queueConnection)
{
    // Use connections...
}
```

For more information on keyed services, see [.NET dependency injection: Keyed services](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection#keyed-services).

## Properties of the SQLite resources

When you use the `WithReference` method to pass a SQLite database resource from the AppHost project to a consuming client project, Aspire exposes the data source as an environment variable with a standardized naming convention:

| Property Name | Description | Environment Variable Format |
|---------------|-------------|----------------------------|
| `DataSource` | The data source path containing the database file path. | `C:\{Path}\{Databasefile}.db` |

> [!NOTE]
> Aspire exposes the data source as an environment variable named `{ResourceName}_DATASOURCE`. For instance, a resource called `sqlite` would have its data source string available as `SQLITE_DATASOURCE`.

## Configuration

The SQLite client integration provides multiple options to configure the connection based on the requirements and conventions of your project.

### Use a connection string

When using a connection string from the `ConnectionStrings` configuration section, you can provide the name of the connection string when calling the `AddSqliteConnection` method:

```csharp title="C# — Program.cs"
builder.AddSqliteConnection("sqlite");
```

Then the connection string will be retrieved from the `ConnectionStrings` configuration section:

```json title="JSON — appsettings.json"
{
  "ConnectionStrings": {
    "sqlite": "Data Source=C:\\Database\\Location\\my-database.db"
  }
}
```

For more information on how to format this connection string, see [Microsoft.Data.Sqlite connection strings](https://learn.microsoft.com/dotnet/standard/data/sqlite/connection-strings).

### Use configuration providers

The SQLite client integration supports `Microsoft.Extensions.Configuration`. It loads the settings from configuration using the `Aspire:Sqlite:Client` key. Example `appsettings.json` that configures some of the options:

```json title="JSON — appsettings.json"
{
  "Aspire": {
    "Sqlite": {
      "Client": {
        "ConnectionString": "Data Source=C:\\Database\\Location\\my-database.db",
        "DisableHealthCheck": true
      }
    }
  }
}
```

### Use inline delegates

You can also pass the `Action<SqliteConnectionSettings>` delegate to set up some or all the options inline:

```csharp title="C# — Program.cs"
builder.AddSqliteConnection(
    "sqlite",
    static settings => settings.DisableHealthCheck = true);
```

## Client integration health checks

By default, Aspire integrations enable [health checks](/docs/fundamentals/observability/health-checks) for all services. For more information, see [Aspire integrations overview](/integrations/overview).

The Aspire SQLite integration:

- Adds the health check when `SqliteConnectionSettings.DisableHealthCheck` is `false`, which attempts to open a connection to the SQLite database.
- Integrates with the `/health` HTTP endpoint, which specifies all registered health checks must pass for app to be considered ready to accept traffic.

## Observability and telemetry

Aspire integrations automatically set up Logging, Tracing, and Metrics configurations, which are sometimes known as *the pillars of observability*. Depending on the backing service, some integrations may only support some of these features. For example, some integrations support logging and tracing, but not metrics. Telemetry features can also be disabled using the techniques presented in the [Configuration](#configuration) section.

### Logging

The Aspire SQLite integration uses standard .NET logging mechanisms for database operations.

### Tracing

The Aspire SQLite integration will emit tracing activities for database operations using OpenTelemetry when configured.

### Metrics

The Aspire SQLite integration currently has limited metrics support compared to other database integrations due to the nature of SQLite as an embedded database engine.
