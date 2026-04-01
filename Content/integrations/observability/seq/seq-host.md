---
title: Seq hosting integration
description: Learn how to use the Seq hosting integration to add a Seq resource to your AppHost.
order: 362
---



<IntegrationIcon Src="/assets/icons/seq-icon.png" Alt="Seq logo">

The Seq hosting integration models a Seq resource as the `SeqResource` type. To access this type and APIs, add the [📦 Aspire.Hosting.Seq](https://www.nuget.org/packages/Aspire.Hosting.Seq) NuGet package in your AppHost project:
</IntegrationIcon>

<InstallPackage PackageName="Aspire.Hosting.Seq" />

## Add a Seq resource

In your AppHost project, call `AddSeq` to add and return a Seq resource builder:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
                 .ExcludeFromManifest()
                 .WithLifetime(ContainerLifetime.Persistent)
                 .WithEnvironment("ACCEPT_EULA", "Y");

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(seq)
                       .WaitFor(seq);
```

> [!NOTE]
> The Seq container may be slow to start, so it's best to use a _persistent_
>   lifetime to avoid unnecessary restarts. For more information, see [Container
>   resource
>   lifetime](/docs/architecture/resource-model/#built-in-resources-and-lifecycle).

### Accept the Seq End User License Agreement (EULA)

You must accept the [Seq EULA](https://datalust.co/doc/eula-current.pdf) for Seq to start. To accept the agreement in code, pass the environment variable `ACCEPT_EULA` to the Seq container, and set its value to `Y`.

### Seq in the Aspire manifest

Seq shouldn't be part of the Aspire deployment manifest, hence the chained call to `ExcludeFromManifest`. It's recommended you set up a secure production Seq server outside of Aspire for your production environment.

## Add a Seq resource with a data volume

To add a data volume to the Seq resource, call the `WithDataVolume` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
                 .WithDataVolume()
                 .ExcludeFromManifest()
                 .WithLifetime(ContainerLifetime.Persistent);

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(seq)
                       .WaitFor(seq);
```

The data volume is used to persist the Seq data outside the lifecycle of its container.


