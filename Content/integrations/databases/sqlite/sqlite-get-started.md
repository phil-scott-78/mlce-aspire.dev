---
title: Get started with the SQLite integrations
description: Learn how to set up the Aspire SQLite Hosting and Client integrations simply.
order: 327
---



<IntegrationIcon Src="/assets/icons/sqlite-icon.png" Alt="SQLite logo">
<Badge text="⭐ Community Toolkit" size="small" />

[SQLite](https://www.sqlite.org/index.html) is a lightweight, serverless, self-contained SQL database engine that is widely used for local data storage in applications. The Aspire SQLite integration provides a way to use SQLite databases within your Aspire applications, and access them via the [`Microsoft.Data.Sqlite`](https://www.nuget.org/packages/Microsoft.Data.Sqlite) client.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire SQLite integrations in a simple configuration. If you already have this knowledge, see [SQLite Hosting integration](/integrations/databases/sqlite/sqlite-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire SQLite Hosting integration in your Aspire AppHost project. This integration allows you to create and manage SQLite database instances from your Aspire hosting projects:

<InstallPackage packageName="CommunityToolkit.Aspire.Hosting.SQLite" shortName="sqlite" />

Next, in the AppHost project, create an instance of a SQLite database resource, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of SQLite resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [SQLite Hosting integration](/integrations/databases/sqlite/sqlite-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected SQLite properties

In the AppHost, when you used the `WithReference` method to pass a SQLite database resource to a consuming client project, Aspire injects the data source configuration property that you can use in the consuming project.

Aspire exposes the property as an environment variable named `[RESOURCE]_DATASOURCE`. For instance, the data source of a resource called `sqlite` becomes `SQLITE_DATASOURCE`.

> [!TIP]
> The full set of properties that Aspire injects depends on the SQLite resource configuration. For more information, see [Properties of the SQLite resources](/integrations/databases/sqlite/sqlite-client/#properties-of-the-sqlite-resources).

### Use SQLite resources in client code

## Next steps

Now, that you have an Aspire app with SQLite integrations up and running, you can use the following reference documents to learn how to configure and interact with the SQLite resources:

<CardGrid>
  <LinkCard Title="Understand the SQLite hosting integration"
    
    Href="/integrations/databases/sqlite/sqlite-host">Discover how to configure custom database locations, SQLiteWeb interface, and more.</LinkCard>
  <LinkCard Title="Understand the SQLite client integration"
    
    Href="/integrations/databases/sqlite/sqlite-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
</CardGrid>
