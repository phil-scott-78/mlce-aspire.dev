---
title: Compiler Warning ASPIRE002
description: Learn more about compiler Warning ASPIRE002. Project is an Aspire AppHost project but necessary dependencies aren't present. Are you missing an Aspire.Hosting.AppHost PackageReference?
order: 710
---



<Badge
  text="Version introduced: 8.0.0"
  variant="note"
  size="large"
/>

> 'Project' is an Aspire AppHost project but necessary dependencies aren't present. Are you missing an Aspire.Hosting.AppHost PackageReference?

This diagnostic warning is reported when your project is missing reference to the Aspire AppHost.

## Suppress the warning

Add reference to the [📦 Aspire.Hosting.AppHost](https://www.nuget.org/packages/Aspire.Hosting.AppHost) NuGet package. For more information about editor config files, see [Configuration files for code analysis rules](/docs/diagnostics/overview/#suppress-in-the-editorconfig-file).
