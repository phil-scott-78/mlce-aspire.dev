---
title: Get started with the MySQL integrations
description: Learn how to set up the Aspire MySQL Hosting and Client integrations simply.
order: 308
---



<IntegrationIcon Src="/assets/icons/mysqlconnector-icon.png" Alt="MySQL logo">

[MySQL](https://www.mysql.com/) is an open-source Relational Database Management System (RDBMS) that uses Structured Query Language (SQL) to manage and manipulate data. It's employed in many different environments, from small projects to large-scale enterprise systems and it's a popular choice to host data that underpins microservices in a cloud-native application. The Aspire MySQL integration provides a way to connect to existing MySQL databases, or create new instances from the [`mysql` container image](https://hub.docker.com/_/mysql).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire MySQL integrations in a simple configuration. If you already have this knowledge, see [MySQL Hosting integration](/integrations/databases/mysql/mysql-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire MySQL Hosting integration in your Aspire AppHost project. This integration allows you to create and manage MySQL database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.MySql" shortName="mysql" />

Next, in the AppHost project, create instances of MySQL server and database resources, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of MySQL resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [MySQL Hosting integration](/integrations/databases/mysql/mysql-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected MySQL properties

In the AppHost, when you used the `WithReference` method to pass a MySQL server or database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `mysqldb` becomes `MYSQLDB_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on whether you passed a MySQL server or database resource. For more information, see [Properties of the MySQL resources](/integrations/databases/mysql/mysql-client/#properties-of-the-mysql-resources).

### Use MySQL resources in client code

## Next steps

Now, that you have an Aspire app with MySQL integrations up and running, you can use the following reference documents to learn how to configure and interact with the MySQL resources:

<CardGrid>
  <LinkCard Title="Understand the MySQL hosting integration"
    
    Href="/integrations/databases/mysql/mysql-host">Discover how to configure persistence, phpMyAdmin resources, and more.</LinkCard>
  <LinkCard Title="Understand the MySQL client integration"
    
    Href="/integrations/databases/mysql/mysql-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
   <LinkCard Title="Understand the MySQL community extensions"
    
    Href="/integrations/databases/mysql/mysql-extensions">Discover how to add the DbGate management user interface, and more.</LinkCard>
</CardGrid>
