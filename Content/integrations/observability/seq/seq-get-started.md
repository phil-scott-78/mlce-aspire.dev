---
title: Get started with the Seq integration
description: Learn how to set up the Aspire Seq hosting and client integrations simply.
order: 361
---



<IntegrationIcon Src="/assets/icons/seq-icon.png" Alt="Seq logo">

[Seq](https://datalust.co/seq) is the intelligent search, analysis, and alerting server built for structured log data. The Aspire Seq integration enables you to connect to existing Seq instances or create new instances from Aspire with the [`datalust/seq` container image](https://hub.docker.com/r/datalust/seq).
</IntegrationIcon>

In this introduction, you'll see how to install and use the Aspire Seq integrations in a simple configuration. If you already have this knowledge, see [Seq hosting integration](/integrations/observability/seq/seq-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Seq hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="Aspire.Hosting.Seq" />

Next, in the AppHost project, create instances of Seq resources and pass them to the consuming client projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
                 .ExcludeFromManifest()
                 .WithLifetime(ContainerLifetime.Persistent)
                 .WithEnvironment("ACCEPT_EULA", "Y");

var myService = builder.AddProject<Projects.ExampleProject>()
                       .WithReference(seq)
                       .WaitFor(seq);

builder.Build().Run();
```

> [!NOTE]
> The Seq container may be slow to start, so it's best to use a _persistent_
>   lifetime to avoid unnecessary restarts. For more information, see [Container
>   resource
>   lifetime](/docs/architecture/resource-model/#built-in-resources-and-lifecycle).

> [!NOTE]
> You must accept the [Seq EULA](https://datalust.co/doc/eula-current.pdf) for Seq to start. To accept the agreement in code, pass the environment variable `ACCEPT_EULA` to the Seq container, and set its value to `Y`.

## Set up client integration

To get started with the Seq client integration, install the package:

<InstallPackage PackageName="Aspire.Seq" />

In the `Program.cs` file of your client-consuming project, call the `AddSeqEndpoint` extension method to register OpenTelemetry Protocol exporters to send logs and traces to Seq:

```csharp
builder.AddSeqEndpoint(connectionName: "seq");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the Seq
>   resource in the AppHost project.

## See also

- [Seq hosting integration](/integrations/observability/seq/seq-host)
- [Seq client integration](/integrations/observability/seq/seq-client)
- [Seq documentation](https://docs.datalust.co/)
