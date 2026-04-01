---
title: Get started with the ClickHouse integrations
description: Learn how to set up the Aspire ClickHouse Hosting and Client integrations simply.
order: 267
---



<IntegrationIcon Src="/assets/icons/clickhouse-icon.png" Alt="ClickHouse logo">

[ClickHouse](https://clickhouse.com) is a high-performance, column-oriented SQL database management system (DBMS) for online analytical processing (OLAP). The Aspire ClickHouse integration provides a way to connect to existing ClickHouse instances, or create new instances from the [`clickhouse/clickhouse-server` container image](https://hub.docker.com/r/clickhouse/clickhouse-server).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire ClickHouse integrations in a simple configuration. If you already have this knowledge, see [ClickHouse Hosting integration](/integrations/databases/clickhouse/clickhouse-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire ClickHouse Hosting integration in your Aspire AppHost project. This integration allows you to create and manage ClickHouse instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.ClickHouse" shortName="clickhouse" />

Next, in the AppHost project, create instances of ClickHouse server and database resources, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of ClickHouse resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [ClickHouse Hosting integration](/integrations/databases/clickhouse/clickhouse-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected ClickHouse properties

In the AppHost, when you used the `WithReference` method to pass a ClickHouse database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Host` property of a resource called `clickhousedb` becomes `CLICKHOUSEDB_HOST`.

> [!TIP]
> The full set of properties that Aspire injects depends on the ClickHouse resource configuration. For more information, see [Properties of the ClickHouse resources](/integrations/databases/clickhouse/clickhouse-client/#properties-of-the-clickhouse-resources).

### Use ClickHouse resources in client code

## Next steps

Now, that you have an Aspire app with ClickHouse integrations up and running, you can use the following reference documents to learn how to configure and interact with the ClickHouse resources:

<CardGrid>
  <LinkCard Title="Understand the ClickHouse hosting integration"
    
    Href="/integrations/databases/clickhouse/clickhouse-host">Discover how to configure data volumes, parameters, and more.</LinkCard>
  <LinkCard Title="Understand the ClickHouse client integration"
    
    Href="/integrations/databases/clickhouse/clickhouse-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
</CardGrid>
