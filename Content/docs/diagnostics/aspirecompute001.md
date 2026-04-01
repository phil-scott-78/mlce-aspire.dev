---
title: Compiler Error ASPIRECOMPUTE001
description: Learn more about compiler Error ASPIRECOMPUTE001. Compute related types and members are for evaluation purposes only and are subject to change or removal in future updates.
order: 710
---



<Badge
  text="Version introduced: 9.3"
  variant="note"
  size="large"
/>

> Compute related types and members are for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

## Example

The following code generates `ASPIRECOMPUTE001`:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("env");
// or
builder.AddAzureContainerAppEnvironment("env")
// or
builder.AddKubernetesEnvironment("env")
```

## To correct this error

Suppress the error with either of the following methods:

- Set the severity of the rule in the _.editorconfig_ file.

  ```ini title=".editorconfig"
  [*.{cs,vb}]
  dotnet_diagnostic.ASPIRECOMPUTE001.severity = none
  ```

  For more information about editor config files, see [Configuration files for code analysis rules](/docs/diagnostics/overview/#suppress-in-the-editorconfig-file).

- Add the following `PropertyGroup` to your project file:

  ```xml title="C# project file"
  <PropertyGroup>
      <NoWarn>$(NoWarn);ASPIRECOMPUTE001</NoWarn>
  </PropertyGroup>
  ```

- Suppress in code with the `#pragma warning disable ASPIRECOMPUTE001` directive.
