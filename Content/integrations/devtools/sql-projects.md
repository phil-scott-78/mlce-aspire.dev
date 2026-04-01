---
title: SQL Database Projects integration
description: Learn about the Aspire hosting integration for SQL Database Projects.
order: 377
---



<IntegrationIcon Src="/assets/icons/sql-database-projects-icon.png" Alt="SQL Database Projects icon">
<Badge text="⭐ Community Toolkit" size="small" />

The Aspire SQL Database Projects hosting integration enables you to deploy SQL database schemas from SQL projects or DACPAC files during local development and testing. This integration supports both [MSBuild.Sdk.SqlProj](https://github.com/rr-wfm/MSBuild.Sdk.SqlProj) and [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) project formats.
</IntegrationIcon>

## Hosting integration

To get started with the Aspire SQL Database Projects hosting integration, install the [CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects](https://www.nuget.org/packages/CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects) NuGet package in the app host project.

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects" />

### Add SQL project

To deploy a SQL project to a SQL Server database, use the `AddSqlProject` extension method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("sqldb");

builder.AddSqlProject<Projects.DatabaseProject>("database-project")
       .WithReference(sql);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(sql);

// After adding all resources, run the app...
```

This method takes a project reference to your SQL database project and deploys it to the specified database when the app host starts.

> [!NOTE]
> Your SQL database project must use either [MSBuild.Sdk.SqlProj](https://github.com/rr-wfm/MSBuild.Sdk.SqlProj) or [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) SDK. Add one of these to your SQL project file:
> 
> ```xml
>   <Project Sdk="MSBuild.Sdk.SqlProj/3.3.0">
>   <!-- or -->
>   <Project Sdk="Microsoft.Build.Sql/2.0.0">
> ```

### Add SQL package (NuGet)

To deploy a SQL database schema from a NuGet package, use the `AddSqlPackage` extension method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("sqldb");

builder.AddSqlPackage("Contoso.DatabasePackage", "1.0.0")
       .WithReference(sql);

builder.AddProject<Projects.ExampleProject>()
       .WithReference(sql);

// After adding all resources, run the app...
```

This method downloads the specified NuGet package and deploys the DACPAC it contains to the database.

> [!TIP]
> When using NuGet packages, mark them with the `IsAspirePackageResource` metadata to indicate they're only used for Aspire deployment:
> 
> ```xml
> <PackageReference Include="Contoso.DatabasePackage" Version="1.0.0">
>   <IsAspirePackageResource>true</IsAspirePackageResource>
> </PackageReference>
> ```

### Add DACPAC file

To deploy a DACPAC file directly, use the `WithDacpac` extension method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("sqldb");

sql.WithDacpac("path/to/database.dacpac");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(sql);

// After adding all resources, run the app...
```

### Configure DAC deployment options

You can customize the DACPAC deployment behavior using the `WithConfigureDacDeployOptions` method:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .AddDatabase("sqldb");

sql.AddSqlProject<Projects.DatabaseProject>("database-project")
   .WithConfigureDacDeployOptions(options =>
   {
       options.BlockOnPossibleDataLoss = false;
       options.DropObjectsNotInSource = true;
   });

// After adding all resources, run the app...
```

### Redeploy action

The SQL Database Projects integration adds a custom "Redeploy" action to the Aspire dashboard. This action allows you to manually trigger a redeployment of the database schema without restarting the entire app host.

To use the Redeploy action:
1. Open the Aspire dashboard
2. Find your SQL database resource
3. Click the "Redeploy" action in the resource's menu

This is useful during development when you make changes to your database schema and want to quickly apply them.

### Multiple SQL projects

You can add multiple SQL projects to the same database or different databases:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

var db1 = sql.AddDatabase("db1");
db1.AddSqlProject<Projects.Database1Project>("db1-project");

var db2 = sql.AddDatabase("db2");
db2.AddSqlProject<Projects.Database2Project>("db2-project");

// After adding all resources, run the app...
```

### Ability to skip deployment

You can use the `WithSkipWhenDeployed` method to avoid re-deploying your SQL Database Project if no changes have been made. This is useful in scenarios where the SQL container database is persisted to permanent storage and will significantly reduce the app host startup time.

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume("data")
    .WithLifetime(ContainerLifetime.Persistent);

var db1 = sql.AddDatabase("db1");

db1.AddSqlProject<Projects.Database1Project>("db1-project")
   .WithSkipWhenDeployed();

// After adding all resources, run the app...
```

## See also

- [MSBuild.Sdk.SqlProj](https://github.com/rr-wfm/MSBuild.Sdk.SqlProj)
- [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql)
- [SQL Server Data Tools (SSDT)](https://learn.microsoft.com/sql/ssdt/sql-server-data-tools)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
