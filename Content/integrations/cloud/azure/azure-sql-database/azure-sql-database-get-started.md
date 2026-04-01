---
title: Get started with the Azure SQL Database integration
description: Learn how to set up the Aspire Azure SQL Database hosting and client integrations simply.
order: 229
---



<IntegrationIcon Src="/assets/icons/azure-sqlserver-icon.png" Alt="Azure SQL Database logo">

[Azure SQL](https://azure.microsoft.com/products/azure-sql) is a family of relational database management systems that run in the Azure cloud. The database systems are Platform-as-a-Service (PaaS) products that enable database administrators to implement highly scalable and available databases without maintaining complex infrastructures themselves. The Aspire Azure SQL Server Hosting integration provides methods to create a new Azure Database server and databases from code in your Aspire AppHost project.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure SQL Database integrations in a simple configuration. If you already have this knowledge, see [Azure SQL Database Hosting integration](/integrations/cloud/azure/azure-sql-database/azure-sql-database-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure SQL Database Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure SQL Database resources from your Aspire hosting projects:

<InstallPackage PackageName="Aspire.Hosting.Azure.Sql" />

Next, in the AppHost project, create an Azure SQL Database resource and pass it to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var azureSql = builder.AddAzureSqlServer("azuresql")
    .AddDatabase("database");

var myService = builder.AddProject<Projects.MyService>()
    .WithReference(azureSql);

// After adding all resources, run the app...

builder.Build().Run();
```

The preceding code adds an Azure SQL server resource named `azuresql` to the AppHost project, adds a database to it, and passes the database connection information to the consuming project.

> [!CAUTION]
> When you call `AddAzureSqlServer`, it implicitly calls
>   `AddAzureProvisioning`—which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure SQL Database resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure SQL Database Hosting integration](/integrations/cloud/azure/azure-sql-database/azure-sql-database-host).

## Set up client integration

To use Azure SQL Database from your client applications, install the Aspire SQL Server client integration in your client project:

<InstallPackage PackageName="Aspire.Microsoft.Data.SqlClient" />

In the `Program.cs` file of your client-consuming project, call the `AddSqlServerClient` extension method to register a `SqlConnection` for use via the dependency injection container:

```csharp
builder.AddSqlServerClient(connectionName: "database");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the SQL database resource in the AppHost project.

### Use injected Azure SQL Database properties

In the AppHost, when you used the `WithReference` method to pass an Azure SQL Database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Host` property of a resource called `database` becomes `DATABASE_HOST`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# — Obtain configuration properties"
string sqlHost = builder.Configuration.GetValue<string>("DATABASE_HOST");
string sqlPort = builder.Configuration.GetValue<string>("DATABASE_PORT");
string databaseName = builder.Configuration.GetValue<string>("DATABASE_DATABASENAME");
```

> [!TIP]
> The full set of properties that Aspire injects depends on whether you passed an Azure SQL Server or database resource. For more information, see [Properties of the Azure SQL Database resources](/integrations/cloud/azure/azure-sql-database/azure-sql-database-client/#properties-of-the-azure-sql-database-resources).

## Add Azure SQL Database resources in client code

After adding the `SqlConnection`, you can retrieve the connection instance using dependency injection:

```csharp
public class ExampleService(SqlConnection connection)
{
    // Use connection...
}
```

For full details on using the client integration, see [Azure SQL Database Client integration](/integrations/cloud/azure/azure-sql-database/azure-sql-database-client).

## Next steps

<CardGrid>
    <LinkCard Title="Azure SQL Database Hosting integration"
        
        Href="/integrations/cloud/azure/azure-sql-database/azure-sql-database-host">Learn more about the hosting integration features and capabilities</LinkCard>
    <LinkCard Title="Azure SQL Database Client integration"
        
        Href="/integrations/cloud/azure/azure-sql-database/azure-sql-database-client">Learn how to use the Azure SQL Database client integration</LinkCard>
</CardGrid>
