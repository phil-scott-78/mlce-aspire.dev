---
title: Get started with the Azure PostgreSQL integrations
description: Learn how to set up the Aspire Azure PostgreSQL Hosting and Client integrations simply.
order: 220
---



<IntegrationIcon Src="/assets/icons/azure-postgresql-icon.png" Alt="Azure Database for PostgreSQL logo">

[Azure Database for PostgreSQL](https://learn.microsoft.com/azure/postgresql/)—Flexible Server is a relational database service based on the open-source Postgres database engine. It's a fully managed database-as-a-service that can handle mission-critical workloads with predictable performance, security, high availability, and dynamic scalability. The Aspire Azure PostgreSQL integration provides a way to connect to existing Azure PostgreSQL databases, or create new instances from .NET with the [`docker.io/library/postgres` container image](https://hub.docker.com/_/postgres).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Azure PostgreSQL integrations in a simple configuration. If you already have this knowledge, see [Azure PostgreSQL Hosting integration](/integrations/cloud/azure/azure-postgresql/azure-postgresql-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Azure PostgreSQL Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Azure PostgreSQL database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.Azure.PostgreSQL" shortName="azure-postgresql" />

Next, in the AppHost project, create instances of Azure PostgreSQL server and database resources, then pass the database to the consuming client projects:

> [!CAUTION]
> When you call `AddAzurePostgresFlexibleServer`, it implicitly calls `AddAzureProvisioning`—which adds support for generating Azure resources dynamically during app startup. The app must configure the appropriate subscription and location. For more information, see [Local provisioning: Configuration](/integrations/cloud/azure/local-provisioning/#configuration).

> [!TIP]
> This is the simplest implementation of Azure PostgreSQL resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Azure PostgreSQL Hosting integration](/integrations/cloud/azure/azure-postgresql/azure-postgresql-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected Azure PostgreSQL properties

In the AppHost, when you used the `WithReference` method to pass an Azure PostgreSQL server or database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `postgresdb` becomes `POSTGRESDB_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on whether you passed an Azure PostgreSQL server or database resource. For more information, see [Properties of the Azure PostgreSQL resources](/integrations/cloud/azure/azure-postgresql/azure-postgresql-client/#properties-of-the-azure-postgresql-resources).

### Use Azure PostgreSQL resources in client code

## Next steps

Now, that you have an Aspire app with Azure PostgreSQL integrations up and running, you can use the following reference documents to learn how to configure and interact with the Azure PostgreSQL resources:

<CardGrid>
  <LinkCard Title="Understand the Azure PostgreSQL hosting integration"
    
    Href="/integrations/cloud/azure/azure-postgresql/azure-postgresql-host">Discover how to configure password authentication, run as container, and more.</LinkCard>
  <LinkCard Title="Understand the Azure PostgreSQL client integration"
    
    Href="/integrations/cloud/azure/azure-postgresql/azure-postgresql-client">Discover how to configure keyed clients, observability, and more.</LinkCard>
</CardGrid>
