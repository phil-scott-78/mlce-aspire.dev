---
title: MongoDB hosting extensions
order: 307
---



<IntegrationIcon Src="/assets/icons/mongodb-icon.png" Alt="MongoDB logo">
<Badge text="⭐ Community Toolkit" size="small" />

The Aspire Community Toolkit MongoDB hosting extensions package provides extra functionality to the [Aspire.Hosting.MongoDB](https://www.nuget.org/packages/Aspire.Hosting.MongoDB) hosting package.
</IntegrationIcon>

This package provides the following features:

- [DbGate](https://dbgate.org/) management UI

## Hosting integration

To get started with the Aspire Community Toolkit MongoDB hosting extensions, install the [CommunityToolkit.Aspire.Hosting.MongoDB.Extensions](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.MongoDB.Extensions) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.MongoDB.Extensions" />

## Add DbGate management UI

**DbGate** is a comprehensive database management tool that provides a web-based interface for managing your MongoDB databases. To add the DbGate management UI to your MongoDB resource, call the `WithDbGate` method on the `MongoDBResourceBuilder` instance:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mongodb = builder.AddMongoDB("mongodb")
                     .WithDbGate();

builder.AddProject<Projects.ExampleProject>()
       .WithReference(mongodb);

// After adding all resources, run the app...
```

This adds a new DbGate resource to the app host which is available from the Aspire dashboard. 

## See also

- [DbGate documentation](https://dbgate.org/)
- [MongoDB integration](/integrations/databases/mongodb/mongodb-get-started)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
