---
title: Connect to PostgreSQL
description: Learn how to connect to PostgreSQL from any language when using Aspire to host your PostgreSQL resources.
order: 314
---



<IntegrationIcon Src="/assets/icons/postgresql-icon.png" Alt="PostgreSQL logo">

When you reference a PostgreSQL resource from the AppHost with `WithReference` (C#) or `withReference` (TypeScript), Aspire automatically injects connection information into the consuming application as environment variables. This page shows how to read those variables and connect to PostgreSQL from any language.
</IntegrationIcon>

> [!TIP]
> For .NET applications, you can use environment variables directly or use the [Client integration (.NET)](/integrations/databases/postgres/postgres-client) for automatic dependency injection, health checks, and telemetry.

## Connection properties

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `postgresdb` becomes `POSTGRESDB_URI`.

### PostgreSQL server

The PostgreSQL server resource exposes the following connection properties:

| Property Name          | Description |
| ---------------------- | ----------- |
| `Host`                 | The hostname or IP address of the PostgreSQL server |
| `Port`                 | The port number the PostgreSQL server is listening on |
| `Username`             | The username for authentication |
| `Password`             | The password for authentication |
| `Uri`                  | The connection URI in postgresql:// format, with the format `postgresql://{Username}:{Password}@{Host}:{Port}` |
| `JdbcConnectionString` | JDBC-format connection string, with the format `jdbc:postgresql://{Host}:{Port}`. User and password credentials are provided as separate `Username` and `Password` properties. |

**Example connection strings:**

```
Uri: postgresql://postgres:p%40ssw0rd1@localhost:5432
JdbcConnectionString: jdbc:postgresql://localhost:5432
```

### PostgreSQL database

The PostgreSQL database resource inherits all properties from its parent server resource and adds:

| Property Name          | Description |
| ---------------------- | ----------- |
| `Uri`                  | The connection URI with the database name, with the format `postgresql://{Username}:{Password}@{Host}:{Port}/{DatabaseName}` |
| `JdbcConnectionString` | JDBC connection string with database name, with the format `jdbc:postgresql://{Host}:{Port}/{DatabaseName}`. User and password credentials are provided as separate `Username` and `Password` properties. |
| `DatabaseName`         | The name of the database |

**Example connection strings:**

```
Uri: postgresql://postgres:p%40ssw0rd1@localhost:5432/catalog
JdbcConnectionString: jdbc:postgresql://localhost:5432/catalog
```

## Connect from your application

The following examples show how to connect to PostgreSQL from different languages. Each example assumes you have a PostgreSQL database resource named `postgresdb` referenced from your AppHost.


**JavaScript**


Install the PostgreSQL client library:

```bash title="Terminal"
npm install pg
```

Read the injected environment variables and connect:

```javascript title="JavaScript — index.js"

// Read Aspire-injected connection properties
const client = new pg.Client({
    user: process.env.POSTGRESDB_USERNAME,
    host: process.env.POSTGRESDB_HOST,
    database: process.env.POSTGRESDB_DATABASENAME,
    password: process.env.POSTGRESDB_PASSWORD,
    port: process.env.POSTGRESDB_PORT,
});

await client.connect();
```

Or use the connection URI directly:

```javascript title="JavaScript — Connect with URI"
const client = new pg.Client({
    connectionString: process.env.POSTGRESDB_URI,
});

await client.connect();
```


**Python**


Install a PostgreSQL driver. This example uses `psycopg`:

```bash title="Terminal"
pip install psycopg[binary]
```

Read the injected environment variables and connect:

```python title="Python — app.py"

await using var dataSource = NpgsqlDataSource.Create(connectionString!);
await using var conn = await dataSource.OpenConnectionAsync();
```


## Passing custom environment variables from the AppHost

If your application expects specific environment variable names, you can pass individual connection properties from the AppHost:


**C#**

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var database = postgres.AddDatabase("myDatabase");

var app = builder.AddExecutable("my-app", "node", "app.js", ".")
    .WithReference(database)
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables["POSTGRES_HOST"] = postgres.Resource.PrimaryEndpoint.Property(EndpointProperty.Host);
        context.EnvironmentVariables["POSTGRES_PORT"] = postgres.Resource.PrimaryEndpoint.Property(EndpointProperty.Port);
        context.EnvironmentVariables["POSTGRES_USER"] = postgres.Resource.UserNameParameter;
        context.EnvironmentVariables["POSTGRES_PASSWORD"] = postgres.Resource.PasswordParameter;
        context.EnvironmentVariables["POSTGRES_DATABASE"] = database.Resource.DatabaseName;
    });

builder.Build().Run();
```

**TypeScript**

```typescript title="TypeScript — apphost.ts"

const builder = await createBuilder();

const postgres = await builder.addPostgres("postgres");
const database = await postgres.addDatabase("myDatabase");

await builder.addNodeApp("my-app", "./app", "index.js")
    .withReference(database)
    .withEnvironment("POSTGRES_USER", postgres.userName)
    .withEnvironment("POSTGRES_PASSWORD", postgres.password)
    .withEnvironment("POSTGRES_DATABASE", "myDatabase");

await builder.build().run();
```


