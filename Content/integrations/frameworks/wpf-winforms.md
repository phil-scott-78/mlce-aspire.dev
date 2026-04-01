---
title: WPF and Windows Forms with Aspire
description: Learn how to use WPF and Windows Forms desktop applications with Aspire for development-time service orchestration.
order: 349
---



<IntegrationIcon Src="/assets/icons/dotnet.svg" Alt=".NET logo">

WPF (Windows Presentation Foundation) and Windows Forms are .NET desktop user interface (UI) frameworks for building Windows desktop applications. While Aspire's primary focus is on cloud-native and web workloads, you can use WPF and Windows Forms applications alongside Aspire for **development-time orchestration** of backend services.
</IntegrationIcon>

> [!NOTE]
> WPF and Windows Forms applications are **not** containerized or deployed as part of `aspire deploy`. Aspire orchestrates them as local processes during development only. For production deployment, use your existing desktop deployment tooling (MSIX, ClickOnce, and so on).

## Add a WPF or Windows Forms app to Aspire

Because WPF and Windows Forms projects are standard .NET projects, you can reference them directly from your AppHost using `AddProject`. This starts the desktop app alongside your backend services when you run the AppHost.

### Step 1: Add a project reference

In your AppHost `.csproj`, add a project reference to your WPF or Windows Forms app:

```xml title="XML — AppHost.csproj"
<ItemGroup>
  <ProjectReference Include="..\MyWpfApp\MyWpfApp.csproj" />
</ItemGroup>
```

### Step 2: Register the project in the Aspire AppHost

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ApiService>("api");

// Start the WPF or WinForms app as a resource in local development
var desktop = builder.AddProject<Projects.MyWpfApp>("desktop")
    .WithReference(api);

// After adding all resources, run the app...
builder.Build().Run();
```

When you run the AppHost, the WPF or Windows Forms application starts automatically as a process resource.

### Step 3: Consume service URLs in the desktop app

Add the `Aspire.ServiceDefaults` (or `Microsoft.Extensions.ServiceDiscovery`) package to your desktop project to enable service discovery:

```csharp title="C# — App.xaml.cs (WPF)"
public partial class App : Application
{
    public IServiceProvider Services { get; private set; } = default!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddServiceDiscovery();
                services.AddHttpClient<ApiClient>(client =>
                {
                    // Ensure that the service name in the URI matches the name in AppHost
                    client.BaseAddress = new Uri("https+http://api");
                }).AddServiceDiscovery();
            })
            .Build();

        Services = host.Services;
        host.Start();
    }
}
```

> [!TIP]
> The `https+http://` scheme tells the HTTP client to prefer HTTPS but fall back to HTTP. The service name (`api`) must match the resource name defined in your `AppHost.cs`.

## Dev-only resources

A common pattern is to start certain resources, such as databases, mock services, or dev tools, only during local development and skip them in production. Use `builder.Environment.IsDevelopment()` to add resources conditionally:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .AddDatabase("mydb");

var api = builder.AddProject<Projects.ApiService>("api")
    .WithReference(postgres);

if (builder.Environment.IsDevelopment())
{
    // Only launch the desktop app during local development
    builder.AddProject<Projects.MyWpfApp>("desktop")
        .WithReference(api);
}

// After adding all resources, run the app...
builder.Build().Run();
```

## Limitations

- **Windows only**: WPF and Windows Forms require Windows. If your team develops on macOS or Linux, the desktop resource will fail to start on non-Windows machines.
- **No deployment support**: `aspire deploy` does not produce deployment artifacts for desktop apps. They are treated as local-only resources.
- **No auto-launch from dashboard**: Desktop apps registered with Aspire start automatically when the AppHost runs—they cannot be manually started or stopped from the Aspire dashboard the same way containerized services can be.

## Comparison with .NET MAUI

| Feature | WPF / WinForms | .NET MAUI |
|---------|---------------|-----------|
| First-class Aspire support | From the `AddProject` method (manual) | From the `Aspire.Hosting.Maui` package |
| Cross-platform | Windows only | Windows, macOS, iOS, Android |
| Dev Tunnels support | Not applicable | Built-in for iOS/Android |
| Production deployment with Aspire | Not supported | Not supported |

For cross-platform desktop and mobile scenarios, consider [.NET MAUI integration](/integrations/dotnet/maui).

## See also

- [.NET MAUI integration](/integrations/dotnet/maui)
- [AppHost overview](/docs/fundamentals/core-concepts/app-host)
- [WPF documentation](https://learn.microsoft.com/dotnet/desktop/wpf/)
- [Windows Forms documentation](https://learn.microsoft.com/dotnet/desktop/winforms/)
