---
title: Get started with the MongoDB integrations
description: Learn how to set up the Aspire MongoDB Hosting and Client integrations simply.
order: 304
---



<IntegrationIcon Src="/assets/icons/mongodb-icon.png" Alt="MongoDB logo">

[MongoDB](https://www.mongodb.com) is a NoSQL database that provides high performance, high availability, and easy scalability. The MongoDB integration enables you to connect to existing MongoDB instances (including [MongoDB Atlas](https://mdb.link/atlas)) or create new instances from Aspire with the `docker.io/library/mongo` container image.
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire MongoDB integrations in a simple configuration. If you already have this knowledge, see [MongoDB Hosting integration](/integrations/databases/mongodb/mongodb-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire MongoDB Hosting integration in your Aspire AppHost project. This integration allows you to create and manage MongoDB database instances from your Aspire hosting projects:

<InstallPackage packageName="Aspire.Hosting.MongoDB" shortName="mongodb" />

Next, in the AppHost project, create instances of MongoDB server and database resources, then pass the database to the consuming client projects:

> [!TIP]
> This is the simplest implementation of MongoDB resources in the AppHost. There are many more options you can choose from to address your requirements. For full details, see [MongoDB Hosting integration](/integrations/databases/mongodb/mongodb-host).

## Use the integration in client projects

Now that the hosting integration is ready, the next step is to install and configure the client integration in any projects that need to use it.

### Set up client projects

### Use injected MongoDB properties

In the AppHost, when you used the `WithReference` method to pass a MongoDB database resource to a consuming client project, Aspire injects several configuration properties that you can use in the consuming project.

Aspire exposes each property as an environment variable named `[RESOURCE]_[PROPERTY]`. For instance, the `Uri` property of a resource called `mongodb` becomes `MONGODB_URI`.

> [!TIP]
> The full set of properties that Aspire injects depends on the MongoDB resource configuration. For more information, see [Properties of the MongoDB resources](/integrations/databases/mongodb/mongodb-client/#properties-of-the-mongodb-resources).

### Use MongoDB resources in client code

## Next steps

Now, that you have an Aspire app with MongoDB integrations up and running, you can use the following reference documents to learn how to configure and interact with the MongoDB resources:

<CardGrid>
  <LinkCard Title="Understand the MongoDB hosting integration"
    
    Href="/integrations/databases/mongodb/mongodb-host">Discover how to configure data volumes, MongoDB Express, and more.</LinkCard>
  <LinkCard Title="Understand the MongoDB client integration"
    
    Href="/integrations/databases/mongodb/mongodb-client">Learn about the MongoDB client integration, including connection strings and observability.</LinkCard>
  <LinkCard Title="Community extensions"
    
    Href="/integrations/databases/mongodb/mongodb-extensions">Explore community-contributed extensions for MongoDB, including DbGate management UI.</LinkCard>
</CardGrid>
