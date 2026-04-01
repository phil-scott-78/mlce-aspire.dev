---
title: Seed data in a database using Aspire
description: Learn about how to seed database data in Aspire
order: 275
---



In this article, you learn how to configure Aspire projects to seed data in a database during app startup. Aspire enables you to seed data using database scripts or Entity Framework Core (EF Core) for common platforms such as SQL Server, PostgreSQL, and MySQL.

## When to seed data

Seeding data pre-populates database tables with rows of data so they're ready for testing your app. You may want to seed data for the following scenarios:

- You want to develop and test different features of your app manually against a meaningful set of data, such as a product catalog or a list of customers.
- You want to run test suites to verify that features behave correctly with a given set of data.

Manually seeding data is tedious and time consuming, so you should automate the process whenever possible. You can seed your database either by running database scripts for Aspire projects during startup or by using tools like EF Core, which handles many underlying concerns for you.

## Understand containerized databases

By default, Aspire database integrations rely on containerized databases, which create the following challenges when trying to seed data:

- By default, Aspire destroys and recreates containers every time the app restarts, which means you have to re-seed your database on each run.
- Depending on your selected database technology, the new container instance may or may not create a default database, which means you might also have to create the database itself.
- Even if a default database exists, it most likely won't have the desired name or schema for your specific app.

Aspire enables you to resolve these challenges using volumes, bind mounts, and a few configurations to seed data effectively.

> [!TIP]
> Container hosts like Docker and Podman support volumes and bind mounts, both of which provide locations for data that persist when a container restarts. Volumes are the recommended solution, because they offer better performance, portability, and security. The container host creates and remains in control of volumes. Each volume can store data for multiple containers. Bind mounts have relatively limited functionality in comparison but enable you to access the data from the host machine.

> [!NOTE]
> Visit the [Database Container Sample App](https://github.com/dotnet/aspire-samples/blob/main/samples/DatabaseContainers/DatabaseContainers.AppHost/AppHost.cs) to view the full project and file structure for each database option.

## Seed data using SQL scripts

The recommended method for executing database seeding scripts depends on the database server you use:

## Seed data using EF Core

To seed data, call `UseSeeding` or `UseAsyncSeeding` methods to configure database seeding directly during context configuration. This approach is cleaner than manually running migrations during startup and integrates better with EF Core's lifecycle management.

> [!IMPORTANT]
> These types of configurations should only be done during development, so make sure to add a conditional that checks your current environment context.

### Seed data with UseSeeding and UseAsyncSeeding methods

Add the following code to the `Program.cs` file of your **API Service** project:

> [!NOTE]
> `UseSeeding` is called from the `EnsureCreated` method, and `UseAsyncSeeding` is called from the `EnsureCreatedAsync` method. When using this feature, it's recommended to implement both `UseSeeding` and `UseAsyncSeeding` methods using similar logic, even if the code using EF is asynchronous. EF Core tooling currently relies on the synchronous version of the method and will not seed the database correctly if the `UseSeeding` method isn't implemented.

The `UseSeeding` and `UseAsyncSeeding` methods provide several advantages over manual seeding approaches:

- **Integrated lifecycle**: Seeding is automatically triggered when the database is created or when migrations are applied.
- **Conditional execution**: The seeding logic only runs when the database is first created, preventing duplicate data on subsequent runs.
- **Better performance**: The seeding methods are optimized for bulk operations and integrate with EF Core's change tracking.
- **Cleaner code**: Seeding configuration is co-located with the context configuration, making it easier to maintain.

### Seed data manually

You can also seed data in Aspire projects using EF Core by explicitly running migrations during startup. EF Core handles underlying database connections and schema creation for you, which eliminates the need to use volumes or run SQL scripts during container startup.

> [!IMPORTANT]
> These types of configurations should only be done during development, so make sure to add a conditional that checks your current environment context.

Add the following code to the `Program.cs` file of your **API Service** project.

## Next steps

Database seeding is useful in a variety of app development scenarios. Try combining these techniques with the resource implementations demonstrated in the following articles:

- [Tutorial: Apply Entity Framework Core migrations in Aspire](/integrations/databases/efcore/migrations)
- [Aspire orchestration overview](/docs/fundamentals/core-concepts/app-host)
