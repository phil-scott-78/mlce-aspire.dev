# Upgrade TODO

Items that could not be automatically fixed during the MLCE migration and need manual attention.

---

## [dashboard/explore.md](Content/dashboard/explore.md)
- **Issue**: 51 `<!-- Image: ... -->` placeholder comments where screenshots should be restored
- **Original**: Astro `<Image>` components referencing files in `@assets/dashboard/explore/` (e.g., `aspire-login.png`, `projects.png`, `trace-span-details.png`, etc.)
- **Suggestion**: Copy image assets from `src/frontend/src/assets/dashboard/explore/` to the MLCE static assets directory, then replace each `<!-- Image: alt -->` comment with `![alt](/assets/dashboard/explore/filename.png)`. See the original MDX import list in `src/frontend/src/content/docs/dashboard/explore.mdx` lines 14-66 for the full mapping.

## [dashboard/overview.md](Content/dashboard/overview.md)
- **Issue**: 1 `<!-- Image: ... -->` placeholder comment for the Resources page screenshot
- **Original**: `<Image src={projectsImage} alt="..." />` referencing `@assets/dashboard/explore/projects.png`
- **Suggestion**: Replace `<!-- Image: A screenshot of the Aspire dashboard Resources page. -->` with `![A screenshot of the Aspire dashboard Resources page.](/assets/dashboard/explore/projects.png)` once assets are copied.

## [dashboard/standalone.md](Content/dashboard/standalone.md)
- **Issue**: 1 `<!-- Image: ... -->` placeholder comment for the container logs screenshot
- **Original**: `<Image src={containerLogImage} alt="..." />` referencing `@assets/dashboard/standalone/aspire-dashboard-container-log.png`
- **Suggestion**: Replace `<!-- Image: Screenshot of the Aspire dashboard container logs. -->` with `![Screenshot of the Aspire dashboard container logs.](/assets/dashboard/standalone/aspire-dashboard-container-log.png)` once assets are copied.

## [dashboard/copilot.md](Content/dashboard/copilot.md)
- **Issue**: 5 `<!-- Image: ... -->` placeholder comments for Copilot feature screenshots
- **Original**: Astro `<Image>` components referencing files in `@assets/dashboard/copilot/` (e.g., `copilot-headerbutton.png`, `copilot-chatquestion.png`, etc.)
- **Suggestion**: Copy image assets from `src/frontend/src/assets/dashboard/copilot/` and restore image references. See original MDX at `src/frontend/src/content/docs/dashboard/copilot.mdx` lines 8-12 for mapping.

## [dashboard/mcp-server.md](Content/dashboard/mcp-server.md)
- **Issue**: 1 `<!-- Image: ... -->` placeholder comment for the MCP dialog screenshot
- **Original**: `<Image src={mcpDialogImage} alt="..." />` referencing `@assets/dashboard/mcp-server/mcp-dialog.png`
- **Suggestion**: Replace with `![A screenshot of the Aspire MCP dialog...](/assets/dashboard/mcp-server/mcp-dialog.png)` once assets are copied.

## [community/thanks.md](Content/community/thanks.md)
- **Issue**: 41 `<!-- Image: ... -->` placeholder comments for project/technology logos in the thanks/credits page
- **Original**: Astro `<Image>` components referencing icon files in `@assets/icons/` (e.g., `opentelemetry-icon.svg`, `redis-icon.png`, `docker.svg`, etc.)
- **Suggestion**: Copy icon assets from `src/frontend/src/assets/icons/` and restore image references. See original MDX at `src/frontend/src/content/docs/community/thanks.mdx` lines 20-109 for the full mapping. Note that some icons have light/dark theme variants using `<ThemeImage>`.

## [community/thanks.md](Content/community/thanks.md) - JSX remnants (FIXED)
- **Issue**: The file contained `{' '}` JSX expressions in anchor tag text (34 occurrences) -- these have been removed.

## [gallery.md](Content/integrations/gallery.md)
- **Issue**: Contains `<Integrations>` custom Astro component with no MLCE equivalent
- **Original**: `<Integrations integrations={integrationJson.filter(...)} availableDocs={integrationDocsJson} />` - a React/Astro component that renders a searchable/filterable gallery of all Aspire integrations from JSON data files
- **Suggestion**: Build an MLCE Blazor component (`Integrations.razor`) that reads integration data and renders a gallery view, then register it via `AddMdazorComponent<Integrations>()`. Alternatively, generate a static markdown list of integrations from the JSON data.

