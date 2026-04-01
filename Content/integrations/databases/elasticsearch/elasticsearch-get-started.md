---
title: Get started with the Elasticsearch integrations
description: Learn how to set up the Aspire Elasticsearch Hosting and Client integrations simply.
order: 270
---



<IntegrationIcon Src="/assets/icons/elastic-icon.png" Alt="Elasticsearch logo">

[Elasticsearch](https://www.elastic.co/elasticsearch) is a distributed, RESTful search and analytics engine, scalable data store, and vector database capable of addressing a growing number of use cases. The Aspire Elasticsearch integration provides a way to connect to existing Elasticsearch instances, or create new instances from the [`docker.io/library/elasticsearch` container image](https://hub.docker.com/_/elasticsearch).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Elasticsearch integrations in a simple configuration. If you already have this knowledge, see [Elasticsearch Hosting integration](/integrations/databases/elasticsearch/elasticsearch-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Elasticsearch Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Elasticsearch instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.Elasticsearch" shortName="elasticsearch" />

Next, in the AppHost project, create an instance of an Elasticsearch resource, then pass it to the consuming client projects:

> [!TIP]
> This is the simplest implementation of Elasticsearch resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Elasticsearch Hosting integration](/integrations/databases/elasticsearch/elasticsearch-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected Elasticsearch properties

In the AppHost, when you used the `WithReference` method to pass an Elasticsearch resource to a consuming client project, Aspire injects a connection string property that you can use in the consuming project. Aspire exposes the connection string as an environment variable named `ConnectionStrings__elasticsearch`.

### Use Elasticsearch resources in client code

## Next steps

Now, that you have an Aspire app with Elasticsearch integrations up and running, you can use the following reference documents to learn how to configure and interact with the Elasticsearch resources:

<CardGrid>
  <LinkCard Title="Understand the Elasticsearch hosting integration"
    
    Href="/integrations/databases/elasticsearch/elasticsearch-host">Discover how to configure persistence, passwords, and more.</LinkCard>
  <LinkCard Title="Understand the Elasticsearch client integration"
    
    Href="/integrations/databases/elasticsearch/elasticsearch-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
</CardGrid>
