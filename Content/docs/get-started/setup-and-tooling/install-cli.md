---
title: Install Aspire CLI
description: Learn how to install the Aspire CLI as a native executable or .NET global tool to manage your cloud-native applications.
order: 13
---



Aspire provides a command-line interface (CLI) tool to help you create and manage Aspire-based apps. The CLI streamlines your development workflow with an interactive-first experience. This guide shows you how to install the Aspire CLI on your system.

## Install the Aspire CLI

To install the Aspire CLI download and run the install script:

> [!NOTE] Download and install
> You can download and run the installation script in a single command to install the Aspire CLI:
>
> The script installs the latest stable release of Aspire CLI. See [Validation](#validation) to verify the installation.

### Two-step installation process

Alternatively, you can download the script and run it as a two-step process:


<Steps>
<Step stepNumber="1">
Open a terminal.

</Step>
<Step stepNumber="2">
Download the script and save it as a file:

</Step>
<Step stepNumber="3">
Run the script to install the stable release build of Aspire.

You should see output similar to the following snippet:

</Step>
</Steps>

## Validation

To validate that the Aspire CLI is installed, use `aspire --version` to query Aspire CLI for a version number:

```bash title="Aspire CLI"
aspire --version
```

Consider the following example of running the command:

If that command works, you're presented with the version of the Aspire CLI tool:

```bash title="Aspire CLI — Output"
13.2.0+{commitSHA}
```

The `+{commitSHA}` suffix indicates the specific commit from which the Aspire CLI was built.

> [!CAUTION] Troubleshooting
> If you're not seeing a version number, it's possible that your terminal
>   session needs to be restarted to pick up the changes to your PATH environment
>   variable. Simply close and reopen your terminal, then try running the `aspire
>   --version` command again.

## See also

- [aspire-install script reference](/reference/cli/install-script)
- [`aspire` command reference](/reference/cli/commands/aspire)
- [Aspire VS Code extension](/docs/get-started/setup-and-tooling/aspire-vscode-extension) — install the CLI and use Aspire commands from within VS Code
