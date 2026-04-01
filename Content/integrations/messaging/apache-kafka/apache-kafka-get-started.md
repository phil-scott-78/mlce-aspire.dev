---
title: Get started with the Apache Kafka integration
description: Learn how to set up the Aspire Apache Kafka hosting and client integrations simply.
order: 350
---



<IntegrationIcon Src="/assets/icons/apache-kafka-icon.svg" Alt="Apache Kafka logo">

[Apache Kafka](https://kafka.apache.org/) is a distributed streaming platform that enables you to build real-time data pipelines and streaming applications. The Aspire Apache Kafka integration enables you to connect to existing Kafka instances or create new instances from Aspire with the [`confluentinc/confluent-local` container image](https://hub.docker.com/r/confluentinc/confluent-local).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Apache Kafka integrations in a simple configuration. If you already have this knowledge, see [Apache Kafka hosting integration](/integrations/messaging/apache-kafka/apache-kafka-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Kafka hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Kafka" />

Next, in the AppHost project, create instances of Kafka resources and pass them to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder.AddKafka("kafka");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(kafka);

builder.Build().Run();
```

When Aspire adds a container image to the AppHost, it creates a new Kafka server instance on your local machine.

> [!TIP]
> If you'd rather connect to an existing Kafka server, call `AddConnectionString` instead. For more information on how to use `WithReference`, see [Resource dependencies](/docs/fundamentals/core-concepts/resources) and [Configuration injection](/docs/fundamentals/core-concepts/app-host).

## Set up client integration

To get started with the Aspire Apache Kafka client integration, install the package:

<InstallPackage PackageName="Aspire.Confluent.Kafka" />

In the `Program.cs` file of your client-consuming project, call the `AddKafkaProducer` extension method to register an `IProducer<TKey, TValue>` for use via the dependency injection container:

```csharp
builder.AddKafkaProducer<string, string>("kafka");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Kafka resource in the AppHost project.

You can then retrieve the `IProducer<TKey, TValue>` instance using dependency injection:

```csharp
internal sealed class Worker(IProducer<string, string> producer) : BackgroundService
{
    // Use producer...
}
```

## See also

- [Apache Kafka hosting integration](/integrations/messaging/apache-kafka/apache-kafka-host)
- [Apache Kafka client integration](/integrations/messaging/apache-kafka/apache-kafka-client)
