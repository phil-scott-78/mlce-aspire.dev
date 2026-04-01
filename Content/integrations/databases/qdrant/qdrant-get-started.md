---
title: Get started with the Qdrant integrations
description: Learn how to set up the Aspire Qdrant Hosting and Client integrations simply.
order: 317
---



<IntegrationIcon Src="/assets/icons/qdrant-icon.svg" Alt="Qdrant logo">

[Qdrant](https://qdrant.tech/) is an open-source vector similarity search engine that efficiently stores, indexes, and searches large-scale vector data. It's commonly used in machine learning, artificial intelligence, and data science applications. The Aspire Qdrant integration enables you to connect to existing Qdrant servers or create new servers from Aspire with the [`qdrant/qdrant` container image](https://hub.docker.com/r/qdrant/qdrant).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Qdrant integrations in a simple configuration. If you already have this knowledge, see [Qdrant Hosting integration](/integrations/databases/qdrant/qdrant-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Qdrant Hosting integration in your Aspire AppHost project. This integration allows you to create and manage Qdrant vector database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.Qdrant" shortName="qdrant" />

Next, in the AppHost project, create instances of Qdrant server resources, then pass the server to the consuming client projects:

> [!TIP]
> This is the simplest implementation of Qdrant resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [Qdrant Hosting integration](/integrations/databases/qdrant/qdrant-host).

> [!TIP]
> The `qdrant/qdrant` container image includes a web UI that you can use to explore your vectors and administer the database. To access this tool, start your Aspire solution and then, in the Aspire dashboard, select the endpoint for the Qdrant resource. In your browser's address bar, append **/dashboard** and press Enter.

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected Qdrant properties

In the AppHost, when you used the `WithReference` method to pass a Qdrant resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `qdrant` becomes `QDRANT_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on the Qdrant resource configuration. For more information, see [Properties of the Qdrant resources](/integrations/databases/qdrant/qdrant-client/#properties-of-the-qdrant-resources).

### Use Qdrant resources in client code

## Next steps

Now that you have an Aspire app with Qdrant integrations up and running, you can use the following reference documents to learn how to configure and interact with the Qdrant resources:

<CardGrid>
  <LinkCard Title="Understand the Qdrant hosting integration"
    
    Href="/integrations/databases/qdrant/qdrant-host">Discover how to configure data volumes, API keys, and more.</LinkCard>
  <LinkCard Title="Understand the Qdrant client integration"
    
    Href="/integrations/databases/qdrant/qdrant-client">Learn about the Qdrant client integration, including connection strings and observability.</LinkCard>
</CardGrid>
