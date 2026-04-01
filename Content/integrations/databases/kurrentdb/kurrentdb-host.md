---
title: KurrentDB Hosting integration reference
description: Learn how to use the Aspire KurrentDB hosting integration to orchestrate and configure KurrentDB in your Aspire projects.
order: 296
---



<Badge text="⭐ Community Toolkit" size="small" />

To get started with the Aspire KurrentDB integrations, follow the [Get started with KurrentDB integrations](/integrations/databases/kurrentdb/kurrentdb-get-started) guide.

This article includes full details about the Aspire KurrentDB Hosting integration.

## Installation

To install the [📦 CommunityToolkit.Aspire.Hosting.KurrentDB](https://nuget.org/packages/CommunityToolkit.Aspire.Hosting.KurrentDB) NuGet package in the AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.KurrentDB" />

## Add KurrentDB resource

In the AppHost project, register and consume the KurrentDB integration using the `AddKurrentDB` extension method to add the KurrentDB container to the application builder.

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var kurrentdb = builder.AddKurrentDB("kurrentdb");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(kurrentdb);

// After adding all resources, run the app...
```

## Add KurrentDB resource with data volume

To add a data volume to the KurrentDB resource, call the `WithDataVolume` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var kurrentdb = builder.AddKurrentDB("kurrentdb")
                       .WithDataVolume();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(kurrentdb);

// After adding all resources, run the app...
```

The data volume is used to persist the KurrentDB data outside the lifecycle of its container. The data volume is mounted at the `/var/lib/kurrentdb` path in the KurrentDB container.

## Add KurrentDB resource with data bind mount

To add a data bind mount to the KurrentDB resource, call the `WithDataBindMount` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var kurrentdb = builder.AddKurrentDB("kurrentdb")
                       .WithDataBindMount(source: @"C:\KurrentDB\Data");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(kurrentdb);

// After adding all resources, run the app...
```

> [!NOTE]
> Data bind mounts have limited functionality compared to volumes, and when you
>   use a bind mount, a file or directory on the host machine is mounted into a
>   container.

## Hosting integration health checks

The KurrentDB hosting integration automatically adds a health check for the KurrentDB resource. The health check verifies that the KurrentDB instance is running and that a connection can be established to it.
