---
title: Get started with the Mailpit integrations
description: Learn how to set up the Aspire Mailpit hosting integration simply.
order: 374
---



<Badge text="⭐ Community Toolkit" size="small" />

[Mailpit](https://mailpit.axllent.org/) is an email testing tool for developers. It acts as an SMTP server, captures emails, and provides a web interface to view them. The Aspire Mailpit hosting integration enables you to run Mailpit alongside your Aspire projects for local email testing.

In this introduction, you'll see how to install and use the Aspire Mailpit hosting integration in a simple configuration. If you already have this knowledge, see [Mailpit hosting integration](/integrations/devtools/mailpit/mailpit-host) for full reference details.

> [!NOTE]
> To follow this guide, you must have created an Aspire solution to work with. To learn how to do that, see [Build your first Aspire app](/docs/get-started/first-app).

## Set up hosting integration

To begin, install the Aspire Mailpit hosting integration in your Aspire AppHost project:

<InstallPackage PackageName="CommunityToolkit.Aspire.Hosting.MailPit" />

Next, in the AppHost project, add a Mailpit resource and pass it to your consuming projects:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);

var mailpit = builder.AddMailPit("mailpit");

builder.AddProject<Projects.ExampleProject>()
       .WithReference(mailpit);

// After adding all resources, run the app...
```

## Sending emails to Mailpit

Mailpit doesn't require a dedicated client integration package. You can use any SMTP library to send emails to Mailpit. See the [Mailpit client integration](/integrations/devtools/mailpit/mailpit-client) page for examples using MailKit and `System.Net.Mail`.

For full reference details, see [Mailpit hosting integration](/integrations/devtools/mailpit/mailpit-host) and [Mailpit client integration](/integrations/devtools/mailpit/mailpit-client).

## See also

- [Mailpit documentation](https://mailpit.axllent.org/)
- [Mailpit GitHub repository](https://github.com/axllent/mailpit)
- [Aspire Community Toolkit](https://github.com/CommunityToolkit/Aspire)
- [Aspire integrations overview](/integrations/overview)
- [Aspire GitHub repo](https://github.com/microsoft/aspire)
