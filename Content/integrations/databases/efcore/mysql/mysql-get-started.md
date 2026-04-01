---
title: Get started with the MySQL Pomelo Entity Framework Core integrations
description: Learn how to set up the Aspire MySQL Pomelo Entity Framework Core Hosting and Client integrations simply.
order: 284
---



<IntegrationIcon Src="/assets/icons/mysqlconnector-icon.png" Alt="MySQL logo">

[MySQL](https://www.mysql.com/) is an open-source Relational Database Management System (RDBMS) that uses Structured Query Language (SQL) to manage and manipulate data. It's employed in many different environments, from small projects to large-scale enterprise systems and it's a popular choice to host data that underpins microservices in a cloud-native application. The Aspire Pomelo MySQL Entity Framework Core integration enables you to connect to existing MySQL databases or create new instances from .NET with the [`mysql` container image](https://hub.docker.com/_/mysql). Entity Framework Core (EF Core) is an object-database mapper that simplifies data access by allowing developers to work with databases using .NET objects.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire MySQL Pomelo Entity Framework Core integrations in a simple configuration. If you already have this knowledge, see [MySQL Hosting integration](/integrations/databases/mysql/mysql-host) and [MySQL Pomelo Entity Framework Core Client integration reference](/integrations/databases/efcore/mysql/mysql-client) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire MySQL Hosting integration in your Aspire AppHost project. This integration allows you to create and manage MySQL database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.MySql" shortName="mysql" />

Next, in the AppHost project, create instances of MySQL server and database resources, then pass the database to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
                   .WithLifetime(ContainerLifetime.Persistent);

var mysqldb = mysql.AddDatabase("mysqldb");

var exampleProject = builder.AddProject<Projects.ExampleProject>("apiservice")
                            .WaitFor(mysqldb)
                            .WithReference(mysqldb);
```

> [!TIP]
> This is the simplest implementation of MySQL resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [MySQL Hosting integration](/integrations/databases/mysql/mysql-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

In each of these consuming client projects, install the Aspire MySQL Pomelo EF Core client integration:

<InstallPackage PackageName="Aspire.Pomelo.EntityFrameworkCore.MySql" />

In the `Program.cs` file of your client-consuming project, call the `AddMySqlDbContext` extension method on any `IHostApplicationBuilder` to register a `DbContext` for use through the dependency injection container. The method takes a connection name parameter.

```csharp title="C# — Program.cs"
builder.AddMySqlDbContext<MyDbContext>("mysqldb");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the MySQL database resource in the AppHost project. For more information, see [Set up hosting integration](#set-up-hosting-integration).

### Use injected MySQL properties

In the AppHost, when you used the `WithReference` method to pass a MySQL database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `mysqldb` becomes `MYSQLDB_URI`.

Use the `GetValue()` method to obtain these environment variables in consuming projects:

```csharp title="C# - Obtain configuration properties"
var connectionUri = builder.Configuration.GetConnectionString("mysqldb");
var host = builder.Configuration.GetValue<string>("MYSQLDB_HOST");
var databaseName = builder.Configuration.GetValue<string>("MYSQLDB_DATABASENAME");
```

> [!TIP]
> The full set of properties that Aspire injects depends on the MySQL resource configuration. For more information, see [Properties of the MySQL resources](/integrations/databases/efcore/mysql/mysql-client/#properties-of-the-mysql-resources).

### Use MySQL resources in client code

Now that you've added the `DbContext` to the builder in the consuming project, you can use the MySQL database. Get the `DbContext` instance using dependency injection. For example, to retrieve your data source object from an example service, define it as a constructor parameter and ensure the `ExampleService` class is registered with the dependency injection container:

```csharp title="C# — ExampleService.cs"
public class ExampleService(MyDbContext context)
{
    // Use MySQL DB Context...
}
```

Having obtained the data context, you can work with the MySQL database as you would in any other C# application using EF Core.

## Next steps

Now that you have an Aspire app with MySQL Pomelo EF Core integrations up and running, you can use the following reference documents to learn how to configure and interact with the MySQL resources:

<CardGrid>
  <LinkCard Title="Understand the MySQL hosting integration"
    
    Href="/integrations/databases/mysql/mysql-host">Discover how to configure data volumes, phpMyAdmin, and more.</LinkCard>
  <LinkCard Title="Understand the MySQL client integration"
    
    Href="/integrations/databases/efcore/mysql/mysql-client">Learn about the MySQL client integration, including connection strings and observability.</LinkCard>
</CardGrid>
