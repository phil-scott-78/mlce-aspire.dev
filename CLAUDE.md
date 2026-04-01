# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Is

A static documentation site for .NET Aspire (aspire.dev), built as a Blazor SSR app that renders markdown content to static HTML. It uses **MyLittleContentEngine (MLCE)** — an external library referenced via local project paths — as the content engine, with **MonorailCss** for utility-first CSS and **Mdazor** for rendering custom Blazor components inside markdown.

## Build & Run

```bash
dotnet run                          # serve at http://localhost:5123
dotnet watch                        # live-reload (watches Content/**)
dotnet run -- build                 # static-site output (RunOrBuildContent)
```

The project targets `net10.0`. MLCE projects are referenced from `B:\MyLittleContentEngine\` — these must be present locally.

## Architecture

**Content pipeline:** Markdown files in `Content/` are processed by MLCE's `IMarkdownContentService<T>`. Each content section (docs, integrations, dashboard, deployment, reference, community) has its own marker front-matter type (e.g. `DocsFrontMatter`) registered in `Program.cs` so DI resolves the correct service per route. Front-matter types are in `AspireFrontMatter.cs`.

**Routing pattern:** Each section has a `{Section}Pages.razor` layout component (e.g. `DocsPages.razor`) with a route like `/docs/{*fileName:nonfile}`. Section index URLs show a landing component; all other URLs render through the shared `SectionPage.razor` which handles breadcrumbs, prev/next nav, and the on-page outline. Root-level pages use `Pages.razor` with `/{fileName:nonfile}`.

**Custom markdown components:** Mdazor lets you embed Blazor components in markdown via HTML-like tags. Registered in `Program.cs` with `AddMdazorComponent<T>()`: Badge, Card, CardGrid, LinkCard, Step, Steps, InstallPackage, YouTubeEmbed.

**Styling:** MonorailCss (Tailwind-like utility classes) plus custom CSS in `AspireStyles.cs`. The design system uses Aspire brand colors defined as CSS custom properties (`--aspire-purple`, `--aspire-primary`, etc.) with full dark mode support.

**Layout:** `MainLayout.razor` is the shell — sidebar with section nav and table of contents, sticky header with search/dark-mode/GitHub, and a right-side outline panel. Section tabs are hardcoded in the layout's `_sections` array.

## Content Structure

```
Content/
  index.md                  # home page
  docs/                     # get-started, fundamentals, architecture, app-host, etc.
  integrations/
  dashboard/
  deployment/
  reference/
  community/
  assets/                   # static images
```

Markdown files use YAML front matter with fields: `title`, `description`, `isDraft`, `tags`, `order`, `redirectUrl`, `section`, `uid`. Navigation order is controlled by `order` (default: int.MaxValue).

## Known Issues

`upgrade-todo.md` tracks migration debt from the original Astro site — primarily `<!-- Image: ... -->` placeholder comments where screenshots need to be restored, and a missing `<Integrations>` gallery component.
