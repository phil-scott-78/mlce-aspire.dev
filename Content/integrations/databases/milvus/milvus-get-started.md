---
title: Get started with the Milvus integrations
description: Learn how to set up the Aspire Milvus Hosting and Client integrations simply.
order: 301
---



<IntegrationIcon Src="/assets/icons/milvus-icon.png" Alt="Milvus logo">

[Milvus](https://milvus.io/) is an open-source vector database system that efficiently stores, indexes, and searches large-scale vector data. It's commonly used in machine learning, artificial intelligence, and data science applications. The Aspire Milvus integration enables you to connect to existing Milvus servers or create new servers from Aspire with the [`milvusdb/milvus` container image](https://hub.docker.com/r/milvusdb/milvus).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Milvus integrations in a simple configuration. If you already have this knowledge, see [Milvus Hosting integration](/integrations/databases/milvus/milvus-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Milvus Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Milvus vector database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.Milvus" shortName="milvus" />

Next, in the AppHost project, create instances of Milvus server and database resources, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of Milvus resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Milvus Hosting integration](/integrations/databases/milvus/milvus-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected Milvus properties

In the AppHost, when you used the `WithReference` method to pass a Milvus database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `milvusdb` becomes `MILVUSDB_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on the Milvus resource configuration. For more information, see [Properties of the Milvus resources](/integrations/databases/milvus/milvus-client/#properties-of-the-milvus-resources).

### Use Milvus resources in client code

## Next steps

Now that you have an Aspire app with Milvus integrations up and running, you can use the following reference documents to learn how to configure and interact with the Milvus resources:

<CardGrid>
  <LinkCard Title="Understand the Milvus hosting integration"
    
    Href="/integrations/databases/milvus/milvus-host">Discover how to configure data volumes, Attu GUI, and more.</LinkCard>
  <LinkCard Title="Understand the Milvus client integration"
    
    Href="/integrations/databases/milvus/milvus-client">Learn about the Milvus client integration, including connection strings and observability.</LinkCard>
</CardGrid>
