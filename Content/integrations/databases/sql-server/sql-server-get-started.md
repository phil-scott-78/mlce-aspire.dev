---
title: Get started with the SQL Server integrations
description: Learn how to set up the Aspire SQL Server Hosting and Client integrations simply.
order: 323
---



<IntegrationIcon Src="/assets/icons/sql-icon.png" Alt="SQL Server logo">

[SQL Server](https://www.microsoft.com/sql-server) is a relational database management system developed by Microsoft. The Aspire SQL Server integrations enable you to connect to existing SQL Server instances or create new instances from Aspire with the [`mcr.microsoft.com/mssql/server` container image](https://hub.docker.com/_/microsoft-mssql-server).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire SQL Server integrations in a simple configuration. If you already have this knowledge, see [SQL Server Hosting integration](/integrations/databases/sql-server/sql-server-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire SQL Server Hosting integration in your Aspire AppHost project. This integration allows you to create and manage SQL Server database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.SqlServer" shortName="sqlserver" />

Next, in the AppHost project, create instances of SQL Server server and database resources, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of SQL Server resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [SQL Server Hosting integration](/integrations/databases/sql-server/sql-server-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected SQL Server properties

In the AppHost, when you used the `WithReference` method to pass a SQL Server server or database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `sqldb` becomes `SQLDB_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on whether you passed a server or database resource. For more information, see [Properties of the SQL Server resources](/integrations/databases/sql-server/sql-server-client/#properties-of-the-sql-server-resources).

### Use SQL Server resources in client code

## Next steps

Now that you have an Aspire app with SQL Server integrations up and running, you can use the following reference documents to learn how to configure and interact with the SQL Server resources:

<CardGrid>
  <LinkCard Title="Understand the SQL Server hosting integration"
    
    Href="/integrations/databases/sql-server/sql-server-host">Discover how to configure persistence, database scripts, and more.</LinkCard>
  <LinkCard Title="Understand the SQL Server client integration"
    
    Href="/integrations/databases/sql-server/sql-server-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
   <LinkCard Title="Understand the SQL Server community extensions"
    
    Href="/integrations/databases/sql-server/sql-server-extensions">Discover how to add the DbGate management user interface, and more.</LinkCard>
</CardGrid>
