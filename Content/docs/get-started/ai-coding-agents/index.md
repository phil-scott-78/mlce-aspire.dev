---
title: Use AI coding agents
description: Set up your AI coding agents with skills, MCP servers, and browser automation tailored to Aspire.
order: 26
---



Aspire provides a first-class setup experience for AI coding agents. Run `aspire agent init` in your project and your AI assistant — whether it's GitHub Copilot, Claude Code, or another MCP-compatible tool — can immediately understand, build, debug, and monitor your distributed applications.

## Why Aspire for coding agents

Aspire gives coding agents the same visibility into your running application that a developer has. The resource data, structured logs, and distributed traces you see in the [Aspire Dashboard](/dashboard/overview) are exposed to agents through the [Aspire MCP server](/docs/get-started/ai-coding-agents/aspire-mcp-server) and the [Aspire CLI](/docs/get-started/setup-and-tooling/install-cli). Whether a person is debugging in the dashboard or an agent is diagnosing through MCP, they see the same picture.

The Aspire CLI is built for agent-driven workflows — every command works non-interactively to not block and prompts and supports `--format Json` for structured plain text output. Key commands include `aspire start` (background execution), `aspire start --isolated` (parallel worktrees), `aspire wait` (block until healthy), `aspire describe`, `aspire logs`, and `aspire docs search`.

The Aspire skill file (installed by `aspire agent init`) teaches agents all of these patterns automatically.

## Get started

When you create a new Aspire project with `aspire new` or `aspire init`, you're prompted to configure AI agent environments. You can also run `aspire agent init` at any time to set up or update the configuration.


<Steps>
<Step stepNumber="1">
Open a terminal in your Aspire project directory (the folder containing your AppHost).

</Step>
<Step stepNumber="2">
Run the following command:

```bash title="Aspire CLI"
aspire agent init
```

</Step>
<Step stepNumber="3">
Select the components you want to configure:

- **Aspire skill file** (recommended) — teaches your AI agent how to use Aspire CLI commands
- **Playwright CLI** — enables browser automation for testing web resources
- **Aspire MCP server** — gives your AI agent runtime access to resources, logs, and traces

</Step>
</Steps>

## What gets configured

The `aspire agent init` command detects your AI development environment and creates the appropriate configuration files:

### Aspire skill file

The skill file teaches your AI coding agent how to work with Aspire. It includes a CLI command reference, key workflows (starting apps, debugging issues, adding integrations), and important rules to follow. The file is created in the format your agent expects:

The skill file guides your agent on how to:

- Start and manage your Aspire application (`aspire start`, `aspire stop`, `aspire describe`)
- Debug issues using structured logs and distributed traces
- Add integrations with `aspire add` and search documentation with `aspire docs`
- Use resource MCP tools for database queries and other resource-specific operations

### Aspire MCP server

The MCP server gives your AI agent direct runtime access to your running Aspire application — resource status, logs, traces, and commands. See [Aspire MCP server](/docs/get-started/ai-coding-agents/aspire-mcp-server) for configuration details, available tools, and the security model.

## Migrate from AGENTS.md

If your project has an `AGENTS.md` file from a previous version of Aspire, you can migrate to the new skill file format. The skill file provides better structure and is recognized natively by AI coding agents.


<Steps>
<Step stepNumber="1">
Run `aspire agent init` and select **Install Aspire skill file**.

</Step>
<Step stepNumber="2">
The skill files are created at `.github/skills/aspire/SKILL.md` and `.claude/skills/aspire/SKILL.md`.

</Step>
<Step stepNumber="3">
Review and delete the old `AGENTS.md` file — the skill file replaces it.

</Step>
</Steps>


> [!TIP]
> The skill file is more structured than `AGENTS.md` and includes a complete CLI command reference table, workflow patterns, and rules that the agent follows automatically. You can customize it for your project's specific needs.

## Your first prompts

Once configured, start your preferred AI coding environment. Try asking your agent:

> "Start the Aspire app and show me the resource status."

> "Analyze HTTP request performance for my API."

> "Add a Redis cache to my AppHost."


  **VS Code**

    **Claude Code**

    **Copilot CLI**

    **OpenCode**

    ## Supported AI assistants

The `aspire agent init` command supports the following AI assistants:

- [VS Code](https://code.visualstudio.com/docs/copilot/customization/mcp-servers) with GitHub Copilot
- [Copilot CLI](https://docs.github.com/en/copilot/how-tos/use-copilot-agents/use-copilot-cli#add-an-mcp-server)
- [Claude Code](https://docs.claude.com/en/docs/claude-code/mcp)
- [OpenCode](https://opencode.ai/docs/mcp-servers/)

> [!NOTE]
> The Aspire MCP server uses the STDIO transport protocol and may work with other agentic coding environments that support this MCP communication protocol.

## See also

- [Aspire MCP server](/docs/get-started/ai-coding-agents/aspire-mcp-server) — tools, security, and troubleshooting for the MCP server
- [aspire agent command](/reference/cli/commands/aspire-agent)
- [aspire agent init command](/reference/cli/commands/aspire-agent-init)
- [aspire agent mcp command](/reference/cli/commands/aspire-agent-mcp)
- [Dashboard security considerations](/dashboard/security-considerations)
- [GitHub Copilot in the Dashboard](/dashboard/copilot)
