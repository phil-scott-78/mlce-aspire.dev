---
title: Get started with the PostgreSQL Entity Framework Core integrations
description: Learn how to set up the Aspire PostgreSQL Hosting and Client integrations simply.
order: 291
---



<IntegrationIcon Src="/assets/icons/postgresql-icon.png" Alt="PostgreSQL logo">

[PostgreSQL](https://www.postgresql.org/) is a powerful, open source object-relational database system with many years of active development that has earned it a strong reputation for reliability, feature robustness, and performance. The Aspire PostgreSQL Entity Framework Core (EF Core) integration provides a way to connect to existing PostgreSQL databases, or create new instances from the [`docker.io/library/postgres` container image](https://hub.docker.com/_/postgres).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire PostgreSQL Entity Framework Core integrations in a simple configuration. The same hosting integration is used with both the EF Core and the non-EF Core client integrations. If you already have this knowledge, see [PostgreSQL Hosting integration](/integrations/databases/postgres/postgres-host) and [PostgreSQL EF Core client integration](/integrations/databases/efcore/postgres/postgresql-client) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire PostgreSQL Hosting integration in your Aspire AppHost project. This integration allows you to create and manage PostgreSQL database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.PostgreSQL" shortName="postgresql" />

Next, in the AppHost project, create instances of PostgreSQL server and database resources, then pass the database to the consuming client projects:

```csharp title="AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("postgresdb");

var exampleProject = builder.AddProject<Projects.ExampleProject>("apiservice")
                            .WaitFor(postgresdb)
                            .WithReference(postgresdb);
```

> [!TIP]
> This is the simplest implementation of PostgreSQL resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [PostgreSQL Hosting integration](/integrations/databases/postgres/postgres-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the EF Core client integration in any projects that need to use it.

### Set up client projects

In each of these consuming client projects, install the Aspire PostgreSQL EF Core client integration:

<InstallPackage PackageName="Aspire.Npgsql.EntityFrameworkCore.PostgreSQL" />

In the `Program.cs` file of your client-consuming project, call the `AddNpgsqlDbContext` extension method on any `IHostApplicationBuilder` to register your `DbContext` subclass for use through the dependency injection container. The method takes a connection name parameter.

```csharp title="C# — Program.cs"
builder.AddNpgsqlDbContext<MyDbContext>(connectionName: "postgresdb");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the PostgreSQL database resource in the AppHost project. For more information, see [Set up hosting integration](#set-up-hosting-integration).

### Use injected PostgreSQL properties

In the AppHost, when you used the `WithReference` method to pass a PostgreSQL database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Host` property of a resource called `postgresdb` becomes `POSTGRESDB_HOST`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# - Obtain configuration properties"
string hostname = builder.Configuration.GetValue<string>("POSTGRESDB_HOST");
string databasePort = builder.Configuration.GetValue<string>("POSTGRESDB_PORT");
string databaseName = builder.Configuration.GetValue<string>("POSTGRESDB_DATABASENAME");
```

> [!TIP]
> For the full set of properties that Aspire injects, see [Properties of the PostgreSQL resources](/integrations/databases/efcore/postgres/postgresql-client/#properties-of-the-postgresql-resources).

### Use PostgreSQL resources in client code

Now that you've added the `DbContext` to the builder in the consuming project, you can use the PostgreSQL resource to get and store data. Get the `DbContext` instance using dependency injection. For example, to retrieve your database context object from an example service define it as a constructor parameter and ensure the `ExampleService` class is registered with the dependency injection container:

```csharp title="C# — ExampleService.cs"
public class ExampleService(MyDbContext context)
{
    // Use context to interact with the database...
}
```

Having obtained the database context, you can work with the PostgreSQL database as you would in any other C# application using EF Core.

## Next steps

Now that you have an Aspire app with PostgreSQL EF Core integrations up and running, you can use the following reference documents to learn how to configure and interact with the PostgreSQL resources:

<CardGrid>
  <LinkCard Title="Understand the PostgreSQL hosting integration"
    
    Href="/integrations/databases/postgres/postgres-host">Discover how to configure persistence, pgAdmin resources, and more.</LinkCard>
  <LinkCard Title="Understand the PostgreSQL EF Core client integration"
    
    Href="/integrations/databases/efcore/postgres/postgresql-client">Discover how to enrich DbContext, configure settings, and more.</LinkCard>
</CardGrid>
