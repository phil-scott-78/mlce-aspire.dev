---
title: Get started with the PostgreSQL integrations
description: Learn how to set up the Aspire PostgreSQL Hosting and Client integrations simply.
order: 312
---



<IntegrationIcon Src="/assets/icons/postgresql-icon.png" Alt="PostgreSQL logo">

[PostgreSQL](https://www.postgresql.org/) is a powerful, open source object-relational database system with many years of active development that has earned it a strong reputation for reliability, feature robustness, and performance. The Aspire PostgreSQL integration provides a way to connect to existing PostgreSQL databases, or create new instances from the [`docker.io/library/postgres` container image](https://hub.docker.com/_/postgres).
</IntegrationIcon>

Use this guide to add PostgreSQL to an Aspire solution. Choose the AppHost language for your solution first, then follow the matching quickstart. For the full PostgreSQL hosting API surface in either C# or TypeScript, see [PostgreSQL Hosting integration](/integrations/databases/postgres/postgres-host).

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up the PostgreSQL Hosting integration

Choose your AppHost language, then add PostgreSQL to the AppHost. For the full C# and TypeScript hosting API surface, see [PostgreSQL Hosting integration](/integrations/databases/postgres/postgres-host).

## Next steps

Now that you have an Aspire app with PostgreSQL integrations up and running, you can use the following reference documents to learn how to configure and interact with the PostgreSQL resources:

<CardGrid>
  <LinkCard Title="Understand the PostgreSQL hosting integration"
    
    Href="/integrations/databases/postgres/postgres-host">Discover how to configure persistence, pgAdmin resources, and more.</LinkCard>
  <LinkCard Title="Understand the PostgreSQL client integration"
    
    Href="/integrations/databases/postgres/postgres-client">Discover how to configure keyed clients, client integration health checks, and more.</LinkCard>
  <LinkCard Title="Understand the PostgreSQL community extensions"
    
    Href="/integrations/databases/postgres/postgresql-extensions">Discover how to add the DbGate management user interface, and more.</LinkCard>
</CardGrid>
