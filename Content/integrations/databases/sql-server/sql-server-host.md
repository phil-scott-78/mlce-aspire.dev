---
title: SQL Server Hosting integration reference
description: Learn how to use the Aspire SQL Server Hosting integration to create and manage SQL Server server and database resources in your Aspire projects.
order: 324
---



<IntegrationIcon Src="/assets/icons/sql-icon.png" Alt="SQL Server logo">

To get started with the Aspire SQL Server integrations, follow the [Get started with SQL Server integrations](/integrations/databases/sql-server/sql-server-get-started) guide. If you want to use SQL Server with Entity Framework Core (EF Core) in your Aspire client project, see [Get started with the SQL Server EF Core integrations](/integrations/databases/efcore/sql-server/sql-server-get-started).
</IntegrationIcon>

This article includes full details about the Aspire SQL Server Hosting integration, which models the server as the `SqlServerServerResource` type and the database as the `SqlServerDatabaseResource` type. To access these types and APIs, you need to install the SQL Server Hosting integration in your AppHost project.

## Installation

To get started with the Aspire SQL Server hosting integration, install the [📦 Aspire.Hosting.SqlServer](https://www.nuget.org/packages/Aspire.Hosting.SqlServer) NuGet package in your AppHost project:

<InstallPackage PackageName="Aspire.Hosting.SqlServer" />

## Add SQL Server resource and database resource

In your AppHost project, call `AddSqlServer` to add and return a SQL Server resource builder. Chain a call to the returned resource builder to `AddDatabase`, to add SQL Server database resource.

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("database");

builder.AddProject<Projects.ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();
```

> [!NOTE]
> The SQL Server container is slow to start, so it's best to use a _persistent_ lifetime to avoid unnecessary restarts. For more information, see [Container resource lifetime](/docs/architecture/resource-model/#built-in-resources-and-lifecycle).

When Aspire adds a container image to the AppHost, as shown in the preceding example with the `mcr.microsoft.com/mssql/server` image, it creates a new SQL Server instance on your local machine. A reference to your SQL Server resource builder (the `sql` variable) is used to add a database. The database is named `database` and then added to the `ExampleProject`.

When adding a database resource to the app model, the database is created if it doesn't already exist. The creation of the database relies on the [AppHost eventing APIs](/docs/app-host/eventing), specifically `ResourceReadyEvent`. In other words, when the `sql` resource is _ready_, the event is raised and the database resource is created.

The SQL Server resource includes default credentials with a `username` of `sa` and a random `password` generated using the `CreateDefaultPasswordParameter` method.

When the AppHost runs, the password is stored in the AppHost's secret store. It's added to the `Parameters` section, for example:

```json
{
  "Parameters:sql-password": "<THE_GENERATED_PASSWORD>"
}
```

The name of the parameter is `sql-password`, but really it's just formatting the resource name with a `-password` suffix. For more information, see [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/aspnet/core/security/app-secrets) and [Add SQL Server resource with parameters](#add-sql-server-resource-with-parameters).

The `WithReference` method configures a connection in the `ExampleProject` named `database`.

> [!TIP]
> If you'd rather connect to an existing SQL Server, call `AddConnectionString` instead. For more information, see [Reference existing resources](/docs/fundamentals/core-concepts/resources).

## Add SQL Server resource with database scripts

By default, when you add a `SqlServerDatabaseResource`, it relies on the following SQL script to create the database:

```sql title="SQL — Default database creation script"
IF
(
    NOT EXISTS
    (
        SELECT 1
        FROM sys.databases
        WHERE name = @DatabaseName
    )
)
CREATE DATABASE [<QUOTED_DATABASE_NAME>];
```

To alter the default script, chain a call to the `WithCreationScript` method on the database resource builder:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent);

var databaseName = "app-db";
var creationScript = $$"""
    IF DB_ID('{{databaseName}}') IS NULL
        CREATE DATABASE [{{databaseName}}];
    GO

    -- Use the database
    USE [{{databaseName}}];
    GO

    -- Create the todos table
    CREATE TABLE todos (
        id INT PRIMARY KEY IDENTITY(1,1),        -- Unique ID for each todo
        title VARCHAR(255) NOT NULL,             -- Short description of the task
        description TEXT,                        -- Optional detailed description
        is_completed BIT DEFAULT 0,              -- Completion status
        due_date DATE,                           -- Optional due date
        created_at DATETIME DEFAULT GETDATE()    -- Creation timestamp
    );
    GO

    """;

var db = sql.AddDatabase(databaseName)
            .WithCreationScript(creationScript);

builder.AddProject<Projects.AspireApp_ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();

```

The preceding example creates a database named `app-db` with a single `todos` table. The SQL script is executed when the database resource is created. The script is passed as a string to the `WithCreationScript` method, which is then executed in the context of the SQL Server resource.

## Add SQL Server resource with data volume

To add a data volume to the SQL Server resource, call the `WithDataVolume` method on the SQL Server resource:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithDataVolume();

var db = sql.AddDatabase("database");

builder.AddProject<Projects.AspireApp_ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();
```

The data volume is used to persist the SQL Server data outside the lifecycle of its container. The data volume is mounted at the `/var/opt/mssql` path in the SQL Server container and when a `name` parameter isn't provided, the name is generated at random. For more information on data volumes and details on why they're preferred over [bind mounts](#add-sql-server-resource-with-data-bind-mount), see [Docker docs: Volumes](https://docs.docker.com/engine/storage/volumes).

> [!CAUTION]
> The password is stored in the data volume. When using a data volume and if the password changes, it will not work until you delete the volume.

## Add SQL Server resource with data bind mount

To add a data bind mount to the SQL Server resource, call the `WithDataBindMount` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithDataBindMount(source: @"C:\SqlServer\Data");

var db = sql.AddDatabase("database");

builder.AddProject<Projects.AspireApp_ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();
```

> [!NOTE]
> Data [bind mounts](https://docs.docker.com/engine/storage/bind-mounts/) have limited functionality compared to [volumes](https://docs.docker.com/engine/storage/volumes/), which offer better performance, portability, and security, making them more suitable for production environments. However, bind mounts allow direct access and modification of files on the host system, ideal for development and testing where real-time changes are needed.

Data bind mounts rely on the host machine's filesystem to persist the SQL Server data across container restarts. The data bind mount is mounted at the `C:\SqlServer\Data` on Windows (or `/SqlServer/Data` on Unix) path on the host machine in the SQL Server container. For more information on data bind mounts, see [Docker docs: Bind mounts](https://docs.docker.com/engine/storage/bind-mounts).

## Add SQL Server resource with parameters

When you want to explicitly provide the password used by the container image, you can provide these credentials as parameters. Consider the following alternative example:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var sql = builder.AddSqlServer("sql", password);
var db = sql.AddDatabase("database");

builder.AddProject<Projects.AspireApp_ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();
```

For more information on providing parameters, see [External parameters](/docs/fundamentals/core-concepts/resources).

## Use a specific SQL Server container image tag

If you want to use a specific tag for the SQL Server container image, you can call the `WithImageTag` method to override the default tag. This is useful when you need to test against a specific version of SQL Server:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithImageTag("2025-RTM-ubuntu-24.04-preview");

var db = sql.AddDatabase("database");

builder.AddProject<Projects.AspireApp_ExampleProject>("exampleproject")
       .WithReference(db)
       .WaitFor(db);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code uses the `2025-RTM-ubuntu-24.04-preview` tag for the SQL Server container image. For a list of available tags, see [SQL Server container image tags](https://mcr.microsoft.com/en-us/artifact/mar/mssql/server/tags).

## Connect to database resources

When the Aspire AppHost runs, the server's database resources can be accessed from external tools, such as [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) or [MSSQL for Visual Studio Code](https://learn.microsoft.com/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code). The connection string for the database resource is available in the dependent resources environment variables and is accessed using the [Aspire dashboard: Resource details](/dashboard/explore/#resource-details) pane. The environment variable is named `ConnectionStrings__{name}` where `{name}` is the name of the database resource, in this example it's `database`. Use the connection string to connect to the database resource from external tools. Imagine that you have a database named `todos` with a single `dbo.Todos` table.


    **SQL Server Management Studio**
        
        To connect to the database resource from SQL Server Management Studio, follow these steps:

        <Steps>
        <Step stepNumber="1">
        Open SSMS.

        </Step>
        <Step stepNumber="2">
        In the **Connect to Server** dialog, select the **Additional Connection Parameters** tab.

        </Step>
        <Step stepNumber="3">
        Paste the connection string into the **Additional Connection Parameters** field and select **Connect**.

        ![SQL Server Management Studio: Connect to Server dialog.](/../assets/integrations/sql/ssms-new-connection.png)

        </Step>
        <Step stepNumber="4">
        If you're connected, you can see the database resource in the **Object Explorer**:

        ![SQL Server Management Studio: Connected to database.](/../assets/integrations/sql/ssms-connected.png)

        </Step>
        </Steps>

        **MSSQL for Visual Studio Code**

        To connect to the database resource from MSSQL for Visual Studio Code, follow these steps:

        <Steps>
        <Step stepNumber="1">
        Open the **SQL SERVER** extension.

        </Step>
        <Step stepNumber="2">
        Select the **Add Connection** option under **CONNECTIONS**.

        ![MSSQL for Visual Studio Code: Connections / add connection screen capture.](/../assets/integrations/sql/mssql-vscode-add-connection.png)

        </Step>
        <Step stepNumber="3">
        Change the **Input type** to **Connection string** and paste the connection string into the **Connection string** field.

        </Step>
        <Step stepNumber="4">
        Select **Connect**.

        ![MSSQL for Visual Studio Code: Connection string input details.](/../assets/integrations/sql/mssql-vscode-connection-details.png)

        </Step>
        <Step stepNumber="5">
        Once you're connected, you can see the database resource in the active tab and run queries against it:

        ![MSSQL for Visual Studio Code: Connected to database.](/../assets/integrations/sql/mssql-vscode-connected.png)

        </Step>
        </Steps>

## Hosting integration health checks

The SQL Server hosting integration automatically adds a health check for the SQL Server resource. The health check verifies that the SQL Server is running and that a connection can be established to it.

The hosting integration relies on the [📦 AspNetCore.HealthChecks.SqlServer](https://www.nuget.org/packages/AspNetCore.HealthChecks.SqlServer) NuGet package.

## Pass connection information to app resources

When you use the `WithReference` method to pass a SQL Server database resource to an app resource such as a Python or JavaScript app, Aspire automatically injects environment variables that describe the connection information. These environment variables follow the naming convention `[RESOURCE]_[PROPERTY]`, where `RESOURCE` is the uppercase name of the resource and `PROPERTY` is the name of the connection property.

For example, if you reference a SQL Server database resource named `sqldb`:

```csharp title="C# — AppHost.cs"
var sqldb = sql.AddDatabase("sqldb");

var pythonApp = builder.AddUvicornApp("api", "./api", "main:app")
    .WithReference(sqldb);
```

The following environment variables are available in the Python application:

- `SQLDB_HOST` - The hostname or IP address
- `SQLDB_PORT` - The port number
- `SQLDB_USERNAME` - The username for authentication
- `SQLDB_PASSWORD` - The password for authentication
- `SQLDB_DATABASE` - The database name
- `SQLDB_URI` - The full connection URI
- `SQLDB_JDBCCONNECTIONSTRING` - JDBC-format connection string

You can access these environment variables in your application code:

```python title="Python example"

const config = {
  server: process.env.SQLDB_HOST,
  port: parseInt(process.env.SQLDB_PORT),
  user: process.env.SQLDB_USERNAME,
  password: process.env.SQLDB_PASSWORD,
  database: process.env.SQLDB_DATABASE
};

const pool = await sql.connect(config);
```

For the complete list of properties available, see [Properties of the SQL Server resources](/integrations/databases/sql-server/sql-server-client/#properties-of-the-sql-server-resources).

## Client integration

To learn how to connect to and interact with SQL Server databases from your consuming projects, see [SQL Server Client integration](/integrations/databases/sql-server/sql-server-client).
