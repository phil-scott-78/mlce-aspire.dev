---
title: Get started with the SQL Server Entity Framework Core integrations
description: Learn how to set up the Aspire SQL Server EF Core Hosting and Client integrations simply.
order: 293
---



<IntegrationIcon Src="/assets/icons/sql-icon.png" Alt="SQL Server logo">

[SQL Server](https://www.microsoft.com/sql-server) is a relational database management system developed by Microsoft. The Aspire SQL Server Entity Framework Core (EF Core) integration enables you to connect to existing SQL Server instances or create new instances from .NET with the [`mcr.microsoft.com/mssql/server` container image](https://hub.docker.com/_/microsoft-mssql-server).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire SQL Server EF Core integrations in a simple configuration. If you already have this knowledge, see the links below for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire SQL Server Hosting integration in your Aspire AppHost project. This integration allows you to create and manage SQL Server database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.SqlServer" shortName="sqlserver" />

> [!NOTE]
> The [Aspire SQL Server Hosting integration](/integrations/databases/sql-server/sql-server-host) is used in the App Host with both the EF Core and non-EF Core client integrations.

Next, in the AppHost project, create instances of SQL Server server and database resources, then pass the database to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");
var sqldb = sql.AddDatabase("sqldb");

var exampleProject = builder.AddProject<Projects.ExampleProject>("exampleproject")
                            .WaitFor(sqldb)
                            .WithReference(sqldb);

builder.Build().Run();
```

> [!TIP]
> This is the simplest implementation of SQL Server resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [SQL Server Hosting integration](/integrations/databases/sql-server/sql-server-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the Aspire SQL Server EF Core client integration in any projects that need to use it.

### Set up client projects

In each of these consuming client projects, install the Aspire SQL Server EF Core client integration:

<InstallPackage PackageName="Aspire.Microsoft.EntityFrameworkCore.SqlServer" />

In the `Program.cs` file of your client-consuming project, call the `AddSqlServerDbContext` extension method on any `IHostApplicationBuilder` to register a `DbContext` for use via the dependency injection container. The method takes a connection name parameter.

```csharp title="C# — Program.cs"
builder.AddSqlServerDbContext<ExampleDbContext>(connectionName: "sqldb");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the SQL Server database resource in the AppHost project. For more information, see [Set up hosting integration](#set-up-hosting-integration).

### Use injected SQL Server properties

In the AppHost, when you used the `WithReference` method to pass a SQL Server database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `sqldb` becomes `SQLDB_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# - Obtain configuration properties"
string sqlHost = builder.Configuration.GetValue<string>("SQLDB_HOST");
string sqlPort = builder.Configuration.GetValue<string>("SQLDB_PORT");
string sqlJDBCConnectionString = builder.Configuration.GetValue<string>("SQLDB_JDBCCONNECTIONSTRING");
```

> [!TIP]
> The full set of properties that Aspire injects depends on the SQL Server resource configuration. For more information, see [Properties of the SQL Server resources](/integrations/databases/efcore/sql-server/sql-server-client/#properties-of-the-sql-server-resources).

### Use SQL Server resources in client code

You can now retrieve the `ExampleDbContext` instance using dependency injection. For example, to retrieve the context from a service:

```csharp title="C# — ExampleService.cs"
public class ExampleService(ExampleDbContext context)
{
    // Use context...
}
```

For more information on dependency injection, see [.NET dependency injection](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection).

## Next steps

<CardGrid>
    <LinkCard Title="SQL Server Hosting integration"
        
        Href="/integrations/databases/sql-server/sql-server-host">Learn about the SQL Server hosting integration features and capabilities.</LinkCard>
    <LinkCard Title="SQL Server Client integration"
        
        Href="/integrations/databases/efcore/sql-server/sql-server-client">Learn about the SQL Server EF Core client integration features and capabilities.</LinkCard>
</CardGrid>
