---
title: Get started with the Azure PostgreSQL EF Core integrations
description: Learn how to set up the Aspire Azure PostgreSQL Hosting and EF Core Client integrations simply.
order: 279
---



<IntegrationIcon Src="/assets/icons/azure-postgresql-icon.png" Alt="Azure Database for PostgreSQL logo">

[Azure Database for PostgreSQL](https://learn.microsoft.com/azure/postgresql/) Flexible Server is a relational database service based on the open-source Postgres database engine. It's a fully managed database-as-a-service that can handle mission-critical workloads with predictable performance, security, high availability, and dynamic scalability. The Aspire Azure PostgreSQL integration provides a way to connect to existing Azure PostgreSQL databases, or create new instances from .NET with the [`docker.io/library/postgres` container image](https://hub.docker.com/_/postgres). This client integration uses Entity Framework Core (EF Core) to provide a robust, type-safe way to interact with your Azure PostgreSQL databases using .NET objects.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure PostgreSQL integrations in a simple configuration. If you already have this knowledge, see [Azure PostgreSQL Hosting integration](/integrations/cloud/azure/azure-postgresql/azure-postgresql-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure PostgreSQL Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure PostgreSQL flexible server instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.Azure.PostgreSQL" shortName="azure-postgresql" />

Next, in the AppHost project, create instances of Azure PostgreSQL flexible server and database resources, then pass the database to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddAzurePostgresFlexibleServer("postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

var exampleProject = builder.AddProject<Projects.ExampleProject>("apiservice")
                            .WaitFor(postgresdb)
                            .WithReference(postgresdb);
```

> [!TIP]
> This is the simplest implementation of Azure PostgreSQL resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure PostgreSQL Hosting integration](/integrations/cloud/azure/azure-postgresql/azure-postgresql-host).

> [!CAUTION]
> When you call `AddAzurePostgresFlexibleServer`, it implicitly calls
>   `AddAzureProvisioning`, which adds support for generating Azure resources
>   dynamically during app startup. The app must configure the appropriate
>   subscription and location. For more information, see [Local provisioning:
>   Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

In each of these consuming client projects, install the Aspire Azure PostgreSQL Entity Framework Core client integration:

<InstallPackage PackageName="Aspire.Azure.Npgsql.EntityFrameworkCore.PostgreSQL" />

In the `Program.cs` file of your client-consuming project, call the `AddAzureNpgsqlDbContext` extension method on any `IHostApplicationBuilder` to register a `DbContext` for use via the dependency injection container. The method takes a connection name parameter.

```csharp title="C# — Program.cs"
builder.AddAzureNpgsqlDbContext<ExampleDbContext>(connectionName: "postgresdb");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Azure PostgreSQL database resource in the AppHost project. For more information, see [Set up hosting integration](#set-up-hosting-integration).

### Use injected Azure PostgreSQL properties

In the AppHost, when you used the `WithReference` method to pass an Azure PostgreSQL database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `postgresdb` becomes `POSTGRESDB_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# - Obtain configuration properties"
string postgresHost = builder.Configuration.GetValue<string>("POSTGRESDB_HOST");
string postgresPort = builder.Configuration.GetValue<string>("POSTGRESDB_PORT");
string postgresJDBCConnectionString = builder.Configuration.GetValue<string>("POSTGRESDB_JDBCCONNECTIONSTRING");
```

> [!TIP]
> The full set of properties that Aspire injects depends on the Azure PostgreSQL resource configuration. For more information, see [Properties of the Azure PostgreSQL resources](/integrations/databases/efcore/azure-postgresql/azure-postgresql-client/#properties-of-the-azure-postgresql-resources).

### Use Azure PostgreSQL resources in client code

Now that you've added `DbContext` to the builder in the consuming project, you can use the Azure PostgreSQL database to get and store data. Get the `DbContext` instance using dependency injection. For example, to retrieve your context object from an example service define it as a constructor parameter and ensure the `ExampleService` class is registered with the dependency injection container:

```csharp title="C# — ExampleService.cs"
public class ExampleService(ExampleDbContext context)
{
    // Use database context...
}
```

Having obtained the context, you can work with the Azure PostgreSQL database using Entity Framework Core as you would in any other C# application.

## Next steps

Now that you have an Aspire app with Azure PostgreSQL integrations up and running, you can use the following reference documents to learn how to configure and interact with the Azure PostgreSQL resources:

<CardGrid>
  <LinkCard Title="Understand the Azure PostgreSQL hosting integration"
    
    Href="/integrations/cloud/azure/azure-postgresql/azure-postgresql-host">Discover how to configure Azure resources, run as a container, and more.</LinkCard>
  <LinkCard Title="Understand the Azure PostgreSQL client integration"
    
    Href="/integrations/databases/efcore/azure-postgresql/azure-postgresql-client">Learn about the Azure PostgreSQL client integration, including connection strings and observability.</LinkCard>
</CardGrid>
