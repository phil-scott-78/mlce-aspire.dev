---
title: GO Feature Flag Client integration reference
description: Learn how to use the Aspire GO Feature Flag client integration to interact with GO Feature Flag from your Aspire projects.
order: 372
---



<Badge text="⭐ Community Toolkit" size="small" />

To get started with the Aspire GO Feature Flag integrations, follow the [Get started with GO Feature Flag integrations](/integrations/devtools/goff/goff-get-started) guide.

This article includes full details about the Aspire GO Feature Flag Client integration.

## Installation

To access the types and APIs for the GO Feature Flag client integration, install the [📦 CommunityToolkit.Aspire.GoFeatureFlag](https://nuget.org/packages/CommunityToolkit.Aspire.GoFeatureFlag) NuGet package in the client-consuming project:

<InstallPackage PackageName="CommunityToolkit.Aspire.GoFeatureFlag" />

## Add GO Feature Flag client

In the `Program.cs` file of your client-consuming project, call the `AddGoFeatureFlagClient` extension method to register a `GoFeatureFlagProvider` for use via the dependency injection container. The method takes a connection name parameter.

```csharp
builder.AddGoFeatureFlagClient(connectionName: "goff");
```

> [!TIP]
> The `connectionName` parameter must match the name used when adding the goff resource in the AppHost project.

You can then retrieve the `GoFeatureFlagProvider` instance using dependency injection:

```csharp
public class ExampleService(GoFeatureFlagProvider provider)
{
    // Use provider...
}
```

## Add keyed GO Feature Flag client

There might be situations where you want to register multiple `GoFeatureFlagProvider` instances with different connection names. To register keyed goff clients, call the `AddKeyedGoFeatureFlagClient` method:

```csharp
builder.AddKeyedGoFeatureFlagClient(name: "technical");
builder.AddKeyedGoFeatureFlagClient(name: "business");
```

Then you can retrieve the `GoFeatureFlagProvider` instances using dependency injection:

```csharp
public class ExampleService(
    [FromKeyedServices("technical")] GoFeatureFlagProvider technicalProvider,
    [FromKeyedServices("business")] GoFeatureFlagProvider businessProvider)
{
    // Use providers...
}
```

## Client integration health checks

The Aspire goff integration uses the configured client to perform a health check.
