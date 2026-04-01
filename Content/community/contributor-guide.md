---
title: Contributor guide for aspire.dev
order: 100
---



Thank you for your interest in contributing to `aspire.dev`! Whether you're fixing typos, adding new content, or improving existing pages, this guide will help you get started and your contributions are greatly appreciated.

## 🚀 About this site

This documentation site is built using [Starlight](https://starlight.astro.build/), a full-featured documentation theme built on top of [Astro](https://astro.build/). Starlight provides a fast, accessible, and SEO-friendly foundation, while Astro's component-based architecture makes it easy to create and maintain content.

## 🤔 Ways to contribute

There are several ways you can contribute to `aspire.dev`:

- **Small fixes** - Correct typos, grammar mistakes, or formatting issues.
- **Content additions** - Add new documentation pages or sections to cover missing topics.
- **Content improvements** - Enhance existing documentation with clearer explanations, updated information, or additional examples.
- **Code contributions** - Improve the site's codebase, fix bugs, or add new features.

There's also different methods for contributing, from selecting the **Edit page** button at the bottom of any documentation page, to using [GitHub Codespaces](/docs/get-started/setup-and-tooling/github-codespaces) for a fully configured development environment, or setting up a local development environment on your machine.

### Click-edit

Selecting the **Edit page** button at the bottom of any documentation page will take you to the corresponding file in the GitHub repository. From there, you can make changes directly in the GitHub web interface and submit a pull request. This is best suited for small fixes or minor content additions.

> [!TIP]
> If you haven't noticed the **Edit page** button, scroll to the bottom of this page and you should see it there:
> 
> ### Codespaces

To avoid local setup, you can use [GitHub Codespaces](/docs/get-started/setup-and-tooling/github-codespaces) for a fully configured development environment in the cloud. This is ideal for larger contributions or if you prefer not to set up a local environment.

> [!NOTE]
> If you choose to use Codespaces, please still refer to the rest of this guide for information on writing style, code quality, and the contribution workflow. Continue reading at the [Writing style section below](#️-writing-style-guide).

### Local development

If you prefer to work locally, you can set up a development environment on your machine. Follow the instructions below to get started.

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- [Node.js](https://nodejs.org/en/download) (LTS version recommended) - For running the development server
- [pnpm](https://pnpm.io/installation) - Fast, disk space efficient package manager
- [Visual Studio Code](https://code.visualstudio.com/) - Recommended code editor
- [Git](https://git-scm.com/downloads) - For version control

## ⚙️ Local dev setup

<Steps>
<Step stepNumber="1">
Clone the `aspire.dev` repository.

```bash
git clone https://github.com/microsoft/aspire.dev.git
```

</Step>
<Step stepNumber="2">
Navigate to the `aspire.dev` directory.

```bash
cd aspire.dev
```

</Step>
<Step stepNumber="3">
Install dependencies



**pnpm**

```bash
pnpm install
```

**npm**

```bash
npm install
```

:::tip[Stop using npm]{icon='seti:todo'}
If you're not already using `pnpm`, consider switching! It's faster and more efficient than `npm` or `yarn`.

Install globally with:

```bash
npm install -g pnpm
```
:::

</Step>
<Step stepNumber="4">
Run the development server

```bash
pnpm dev
```

This starts the Vite development server for the front end and provide hot-reload capabilities.

</Step>
<Step stepNumber="5">
View the site locally

Open your browser to `http://localhost:4321` (or the port shown in your terminal)

:::tip
During local development, the site search functionality is disabled. This is normal behavior as the search index is built during the production build process. To test search functionality, run a production build locally using `pnpm build` and then preview it with `pnpm preview`.
:::

</Step>
</Steps>

### Known formatting limitations

We expose `lint` and `format` scripts in the `package.json` to help maintain code quality and consistency. This isn't something that you're need to run manually. However, regardless of whether or not you run these scrips, be aware of the following known limitation when working with MDX files.

> [!CAUTION]
> **Prettier and Steps components**
> 
> The Prettier formatter has a known limitation when formatting MDX files containing the `Steps` component. Prettier incorrectly converts all ordered lists (`ol`) as a single line, which causes the steps to render malformed.
> 
> **Workaround:** Always ensure there is a blank line between each step item in a `Steps` component. For example:
> 
> ```md
> 
> 
> 1. First step with content
> 
> 1. Second step with content
> 
> 1. Third step with content
> 
> 
> ```
> 
> Without the blank lines between steps, the inner content won't render correctly. If you notice steps appearing malformed after running `pnpm format`, manually add the blank lines back before committing.

## ➡️ Git workflow

<Steps>
<Step stepNumber="1">
Start from an issue (or a discussion that leads to an issue)

</Step>
<Step stepNumber="2">
Fork the repository

As mentioned in the local dev setup section, start by forking the `aspire.dev` repository to your own GitHub account

</Step>
<Step stepNumber="3">
Create a new branch for your changes

```bash
git checkout -b feature/your-feature-name
```

</Step>
<Step stepNumber="4">
Make your changes, considering the writing style guide

</Step>
<Step stepNumber="5">
Commit with descriptive messages

</Step>
<Step stepNumber="6">
Push to your fork

</Step>
<Step stepNumber="7">
Create a pull request, and always follow the [Code of Conduct](https://github.com/microsoft/aspire.dev/blob/main/CODE_OF_CONDUCT.md)

</Step>
</Steps>

## ✍️ Writing style guide

<span id='-writing-style-guide'></span>

When contributing to `aspire.dev`, follow these writing guidelines to ensure consistency and clarity:

- **Use clear and concise language** - Aim for simplicity. Avoid jargon unless necessary, and explain technical terms when they first appear.
- **Be consistent** - Follow existing conventions in terminology, formatting, and structure. Refer to other documentation pages for examples.
- **Use active voice** - Write in active voice to make instructions and explanations more direct and engaging.
- **Use sentence case** - Capitalize only the first word and proper nouns in headings, sidebars, and table of contents.
- **Be inclusive** - Use inclusive language that respects all readers. Avoid gendered terms and stereotypes.
- **Provide examples** - Where applicable, include code snippets or examples to illustrate concepts.
- **Use proper grammar and spelling** - Proofread your contributions to ensure they are free of errors and typos.
- **Structure content logically** - Use headings, subheadings, and lists to organize information in a way that is easy to follow.
- **Link to relevant resources** - When mentioning concepts, tools, or related documentation, provide links to help readers find more information.
- **Follow formatting conventions** - Use consistent formatting for code snippets, commands, and technical terms. Refer to the examples in this guide for guidance.
- **Review existing content** - Before adding new content, review existing documentation to avoid duplication and ensure coherence.

## 📝 Write Markdown

Here are some common Markdown formatting examples to help you write documentation:

### Frontmatter

You can customize individual pages in `aspire.dev` by setting values in their frontmatter.
Frontmatter is set at the top of your files between `---` separators:

```md title="src/content/docs/example.md"
---
title: My page title
---

Page content follows the second `---`.
```

Every page must include at least a `title`. See the [frontmatter reference](https://starlight.astro.build/reference/frontmatter/) for all available fields and how to add custom fields.

### Headings

Use `#` symbols to create headings. More `#` symbols create smaller headings:

```md title="src/content/docs/example.md"
## Heading 2
### Heading 3
#### Heading 4
```

Headings are automatically created as bookmarks (shareable deep links) for easy navigation.

> [!NOTE]
> Avoid using Heading 1 (`#`) in your content, as it is reserved for the page title defined in frontmatter.

> [!TIP]
> To configure which headings appear in the **On this page** sidebar, use the `tableOfContents` frontmatter field. See the [tableOfContents reference](https://starlight.astro.build/reference/frontmatter/#tableofcontents) for more details.

### Text formatting

**Bold text** is created with double asterisks:

```md title="src/content/docs/example.md"
**Bold text**
```

_Italic text_ is created with an `_` (or single asterisks `*`—while valid, for consistency we recommend using `_`):

```md title="src/content/docs/example.md"
_Italic text_
```

`Inline code` is created with backticks:

```md title="src/content/docs/example.md"
`Inline code`
```

### Links

Links are created with square brackets and parentheses:

```md title="src/content/docs/example.md"
[David Pine](https://davidpine.net)
```

Renders as:

[David Pine](https://davidpine.net)

Additionally, when linking to other pages within `aspire.dev`, use site relative paths:

```md title="src/content/docs/example.md"
[Build your first Aspire app](/docs/get-started/first-app)
```

Renders as:

[Build your first Aspire app](/docs/get-started/first-app)

> [!NOTE]
> Site relative links should NOT include a trailing slash. Use `/docs/get-started/first-app` instead of `/docs/get-started/first-app/`.
> ### Lists

Unordered lists use `-` (or `*`—while valid, for consistency we recommend using `-`):

```md title="src/content/docs/example.md"
- First item
- Second item
- Third item
```

Renders as:

- First item
- Second item
- Third item

Ordered lists use numbers:

```md title="src/content/docs/example.md"
1. First step
2. Second step
3. Third step
```

Renders as:

1. First step
2. Second step
3. Third step

### Code blocks

Use triple backticks with a language identifier for syntax highlighting:

````md title="src/content/docs/example.md"
```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.ApiService>("apiservice");
```
````

Renders as:

```csharp title="C# — AppHost.cs"
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.ApiService>("apiservice");
```

To add a title to a code block, use this syntax:

````md title="src/content/docs/example.md"
```csharp title="Program.cs"
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.ApiService>("apiservice");
```
````

Renders as:

```csharp title="Program.cs"
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.ApiService>("apiservice");
```

### Blockquotes

Use `>` to create blockquotes:

```md title="src/content/docs/example.md"
> This is a note or important callout.
```

Renders as:

> This is a note or important callout.

### Tables

Create tables using pipes `|` and hyphens `-`:

```md title="src/content/docs/example.md"
| Feature | Description | Status |
|--|--|--|
| Dashboard | Web-based monitoring | ✅ Available |
| Telemetry | OpenTelemetry support | ✅ Available |
| Deployment | Kubernetes deployment | 🚧 Preview |
```

Renders as:

| Feature | Description | Status |
|--|--|--|
| Dashboard | Web-based monitoring | ✅ Available |
| Telemetry | OpenTelemetry support | ✅ Available |
| Deployment | Kubernetes deployment | 🚧 Preview |

> [!TIP]
> You can align columns using colons: `| :--- |` for left, `| :---: |` for center, and `| ---: |` for right alignment.

### Horizontal rules

Create a horizontal rule with three or more hyphens, asterisks, or underscores:

```md title="src/content/docs/example.md"
---
```

Renders as:

---

### Strikethrough

Use double tildes to create strikethrough text:

```md title="src/content/docs/example.md"
~~This text is crossed out~~
```

Renders as:

~~This text is crossed out~~

### Task lists

Create interactive task lists in Markdown:

```md
- [x] Add Aspire to your project
- [x] Configure service defaults
- [ ] Deploy to Azure
- [ ] Set up monitoring
```

Renders as:

- [x] Add Aspire to your project
- [x] Configure service defaults
- [ ] Deploy to Azure
- [ ] Set up monitoring

### Nested lists

You can nest lists by indenting with two spaces:

```md
- Aspire components
  - Databases
    - PostgreSQL
    - Redis
  - Messaging
    - RabbitMQ
    - Azure Service Bus
```

Renders as:

- Aspire components
  - Databases
    - PostgreSQL
    - Redis
  - Messaging
    - RabbitMQ
    - Azure Service Bus

### Escaping characters

Use a backslash `\` to escape special Markdown characters:

```md
\*This text is not italic\*
\[This is not a link\]
```

Renders as:

\*This text is not italic\*
\[This is not a link\]

### Line breaks

End a line with two or more spaces to create a line break:

```md
First line with two spaces at the end  
Second line
```

Or use an empty line to create a paragraph break.

## ➕ Markdown extensions

The `aspire.dev` site supports several Markdown extensions to enhance your documentation:

### Mermaid diagrams

You can write mermaid diagrams as code blocks:

````md title="src/content/docs/example.md"
```mermaid
graph TD
    A[build-apiservice] --> C[push-apiservice]
    B[provision-container-registry] --> C
    C --> D[deploy-apiservice]
    E[provision-cosmosdb] --> D
    F[provision-identity] --> D
```
````

Renders as:

```mermaid
graph TD
    A[build-apiservice] --> C[push-apiservice]
    B[provision-container-registry] --> C
    C --> D[deploy-apiservice]
    E[provision-cosmosdb] --> D
    F[provision-identity] --> D
```

### Asides

[Asides](https://starlight.astro.build/components/asides/), "admonitions", "callouts", or "alerts" are special highlighted blocks used to draw attention to important information, tips, warnings, or notes.

The `:::` syntax creates asides given a type of `note`, `tip`, `caution`, or `danger` in both Markdown and [MDX](#write-mdx):

````md title="src/content/docs/example.md"
> [!NOTE]
> Some content in an aside.

> [!CAUTION]
> Some cautionary content.

> [!TIP]
> Other content is also supported in asides.
> 
> ```js
> // A code snippet, for example.
> ```

> [!DANGER]
> Do not give your password to anyone.
````

Renders as:

> [!NOTE]
> Some content in an aside.

> [!CAUTION]
> Some cautionary content.

> [!TIP]
> Other content is also supported in asides.
> 
> ```js
> // A code snippet, for example.
> ```

> [!DANGER]
> Do not give your password to anyone.

Additionally, `aspire.dev` supports [GitHub Alerts](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax#alerts) syntax with the [community plugin](https://starlight-github-alerts.netlify.app/getting-started/):

For example, you can write:

#### Note

```md title="src/content/docs/example.md"
> [!NOTE]
> Useful information that users should know, even when skimming content.
```

Renders as:

> [!NOTE]
> Useful information that users should know, even when skimming content.

See the full demo here: [Starlight: GitHub Alerts](https://starlight-github-alerts.netlify.app/demo/).

## ☑️ Write MDX

<p id="write-mdx"></p>

MDX files use the `.mdx` extension and combine standard Markdown with the power of JSX. This means you can write content and seamlessly embed interactive components—all in one file.

With the power of Astro components, you can enhance your documentation with interactive elements, custom layouts, and dynamic content. To use any of the built-in Starlight or custom components available in `aspire.dev`, simply import them at the top of your MDX file and use them like regular JSX components.

### LinkButton component

```mdx title="src/content/docs/example.mdx"
---
title: Example MDX Page
---


Here's an example of an MDX page with a custom button:

Visit aspire.dev
```

Renders as:

Here's an example of an MDX page with a custom button:

Visit aspire.dev
### Aside component

The `Aside` component from Starlight can be used to create asides in your documentation:

````mdx title="src/content/docs/example.mdx"
---
title: Example MDX Page
---


<Aside>Some content in an aside.</Aside>

> [!CAUTION]
> Some cautionary content.

> [!TIP]
> Other content is also supported in asides.
> 
>   ```js
>   // A code snippet, for example.
>   ```

> [!DANGER]
> Do not give your password to anyone.
````

Renders as:

> [!NOTE]
> Some content in an aside.

> [!CAUTION]
> Some cautionary content.

> [!TIP]
> Other content is also supported in asides.
>
> ```js
> // A code snippet, for example.
> ```

> [!DANGER]
> Do not give your password to anyone.

### Using custom components


To use custom components available in `aspire.dev`, import them at the top of your MDX file. Custom component imports rely on configured aliases—have a look at the `tsconfig.json` file for more information:

By using the `@components` alias, you can easily import any custom component from the `frontend/src/components/` directory. For example, to import the `LearnMore` component used in this guide:

```mdx title="src/content/docs/example.mdx"
---
title: Example MDX Page
---


Here's an example of using the `LearnMore` component:

```

Renders as:

Here's an example of using the `LearnMore` component:

## 🆘 Getting help

- **Issues** - Report bugs or request features via [GitHub Issues](https://github.com/microsoft/aspire.dev/issues)
- **Discussions** - Join conversations in [GitHub Discussions](https://github.com/microsoft/aspire.dev/discussions)
- **Discord** - Connect with the community on the [Aspire Discord](https://discord.com/invite/raNPcaaSj8)