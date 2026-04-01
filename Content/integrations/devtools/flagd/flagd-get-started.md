---
title: Get started with the flagd integrations
description: Learn how to set up the Aspire flagd hosting and client integrations simply.
order: 367
---



<Badge text="⭐ Community Toolkit" size="small" />

[flagd](https://flagd.dev) is a feature flag evaluation engine that provides an OpenFeature-compliant backend system for managing and evaluating feature flags in real-time. The Aspire flagd integration enables you to create new container instances from Aspire with the `ghcr.io/open-feature/flagd` container image.

flagd is designed to be a flexible, open-source feature flag evaluation engine. It enables real-time flag modifications, supports various flag types (boolean, string, number, JSON), uses context-sensitive rules for targeting, and performs pseudorandom assignments for experimentation. You can aggregate flag definitions from multiple sources and expose them through gRPC or OFREP services.

In this introduction, you'll see how to install and use the Aspire flagd integrations in a simple configuration. If you already have this knowledge, see [flagd hosting integration](/integrations/devtools/flagd/flagd-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire flagd hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.Flagd" />

In your app host project, call `AddFlagd` on the builder instance to add a flagd container resource, and use `WithBindFileSync` to configure file-based flag synchronization:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var flagd = builder.AddFlagd("flagd")
                   .WithBindFileSync("./flags/");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(flagd);

// After adding all resources, run the app...
```

Flag definitions are stored in JSON files. Create a `flags` folder in your project root and add a `flagd.json` file with your flag configurations. For full details on the hosting integration, see [flagd hosting integration](/integrations/devtools/flagd/flagd-host).

## Set up client integration

To connect to flagd from a consuming project, install the OpenFeature flagd provider package:

```powershell
dotnet add package OpenFeature.Contrib.Providers.Flagd
```

In the `Program.cs` file of your client-consuming project, configure the OpenFeature SDK to use the flagd provider:

```csharp
using OpenFeature;
using OpenFeature.Contrib.Providers.Flagd;

var connectionString = builder.Configuration.GetConnectionString("flagd");

await OpenFeature.Api.Instance.SetProviderAsync(
    new FlagdProvider(new Uri(connectionString)));

var flagClient = OpenFeature.Api.Instance.GetClient();
```

You can then use the `flagClient` to evaluate feature flags:

```csharp
var welcomeBanner = await flagClient.GetBooleanValueAsync(
    "welcome-banner",
    defaultValue: false);

var backgroundColor = await flagClient.GetStringValueAsync(
    "background-color",
    defaultValue: "#000000");
```

For full reference details, see [flagd hosting integration](/integrations/devtools/flagd/flagd-host) and [flagd client integration](/integrations/devtools/flagd/flagd-client).

## See also

- [flagd documentation](https://flagd.dev)
- [OpenFeature documentation](https://openfeature.dev)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
