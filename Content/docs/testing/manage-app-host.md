---
title: Manage the AppHost in tests
description: Learn how to manage the AppHost in Aspire tests.
order: 63
---



When writing functional or integration tests with Aspire, managing the [AppHost](/docs/fundamentals/core-concepts/app-host) instance efficiently is crucial. The AppHost represents the full application environment and can be costly to create and tear down. This article explains how to manage the AppHost instance in your Aspire tests.

For writing tests with Aspire, you use the [Aspire.Hosting.Testing](https://www.nuget.org/packages/Aspire.Hosting.Testing) NuGet package which contains some helper classes to manage the AppHost instance in your tests.

### Use the `DistributedApplicationTestingBuilder` class

In the [tutorial on writing your first test](/docs/testing/write-your-first-test), you were introduced to the `DistributedApplicationTestingBuilder` class which can be used to create the AppHost instance:

```csharp
var appHost = await DistributedApplicationTestingBuilder
    .CreateAsync<Projects.AspireApp_AppHost>();
```

The `DistributedApplicationTestingBuilder.CreateAsync<T>` method takes the AppHost project type as a generic parameter to create the AppHost instance. While this method is executed at the start of each test, it's more efficient to create the AppHost instance once and share it across tests as the test suite grows.

By capturing the AppHost in a field when the test run is started, you can access it in each test without the need to recreate it, decreasing the time it takes to run the tests. Then, when the test run completes, the AppHost is disposed, which cleans up any resources that were created during the test run, such as containers.

### Pass arguments to your AppHost

You can access the arguments from your AppHost with the `args` parameter. Arguments are also passed to [.NET's configuration system](https://learn.microsoft.com/dotnet/core/extensions/configuration), so you can override many configuration settings this way. In the following example, you override the [environment](https://learn.microsoft.com/aspnet/core/fundamentals/environments) by specifying it as a command line option:

```csharp
var builder = await DistributedApplicationTestingBuilder
    .CreateAsync<Projects.MyAppHost>(
    [
        "--environment=Testing"
    ]);
```

Other arguments can be passed to your AppHost `Program` and made available in your AppHost. In the next example, you pass an argument to the AppHost and use it to control whether you add data volumes to a Postgres instance.

In the AppHost `Program`, you use configuration to support enabling or disabling volumes:

```csharp
var postgres = builder.AddPostgres("postgres1");
if (builder.Configuration.GetValue("UseVolumes", true))
{
    postgres.WithDataVolume();
}
```

In test code, you pass `"UseVolumes=false"` in the `args` to the AppHost:

```csharp
public async Task DisableVolumesFromTest()
{
    // Disable volumes in the test builder via arguments:
    using var builder = await DistributedApplicationTestingBuilder
        .CreateAsync<Projects.TestingAppHost1_AppHost>(
        [
            "UseVolumes=false"
        ]);

    // The container will have no volume annotation since we disabled volumes by passing UseVolumes=false
    var postgres = builder.Resources.Single(r => r.Name == "postgres1");

    Assert.Empty(postgres.Annotations.OfType<ContainerMountAnnotation>());
}
```

### Use the `DistributedApplicationFactory` class

While the `DistributedApplicationTestingBuilder` class is useful for many scenarios, there might be situations where you want more control over starting the AppHost, such as executing code before the builder is created or after the AppHost is built. In these cases, you implement your own version of the `DistributedApplicationFactory` class. This is what the `DistributedApplicationTestingBuilder` uses internally.

```csharp
public class TestingAspireAppHost()
    : DistributedApplicationFactory(typeof(Projects.AspireApp_AppHost))
{
    // override methods here
}
```

The constructor requires the type of the AppHost project reference as a parameter. Optionally, you can provide arguments to the underlying host application builder. These arguments control how the AppHost starts and provide values to the args variable used by the _AppHost.cs_ file to start the AppHost instance.

### Lifecycle methods

The `DistributionApplicationFactory` class provides several lifecycle methods that can be overridden to provide custom behavior throughout the preparation and creation of the AppHost. The available methods are `OnBuilderCreating`, `OnBuilderCreated`, `OnBuilding`, and `OnBuilt`.

For example, we can use the `OnBuilderCreating` method to set configuration, such as the subscription and resource group information for Azure, before the AppHost is created and any dependent Azure resources are provisioned, resulting in our tests using the correct Azure environment.

```csharp
public class TestingAspireAppHost() 
    : DistributedApplicationFactory(typeof(Projects.AspireApp_AppHost))
{
    protected override void OnBuilderCreating(
        DistributedApplicationOptions applicationOptions,
        HostApplicationBuilderSettings hostOptions)
    {
        hostOptions.Configuration ??= new();
        hostOptions.Configuration["environment"] = "Development";
        hostOptions.Configuration["AZURE_SUBSCRIPTION_ID"] = "00000000-0000-0000-0000-000000000000";
        hostOptions.Configuration["AZURE_RESOURCE_GROUP"] = "my-resource-group";
    }
}
```

Because of the order of precedence in the .NET configuration system, the environment variables will be used over anything in the _appsettings.json_ or _secrets.json_ file.

Another scenario you might want to use in the lifecycle is to configure the services used by the AppHost. In the following example, consider a scenario where you override the `OnBuilderCreated` API to add resilience to the `HttpClient`:

```csharp
protected override void OnBuilderCreated(
    DistributedApplicationBuilder applicationBuilder)
{
    applicationBuilder.Services.ConfigureHttpClientDefaults(clientBuilder =>
    {
        clientBuilder.AddStandardResilienceHandler();
    });
}
```
