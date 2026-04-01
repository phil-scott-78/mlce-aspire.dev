---
title: Translation guide for aspire.dev
order: 101
---



Thank you for your interest in helping translate `aspire.dev`! Localization makes Aspire documentation accessible to developers around the world, and your contributions are greatly appreciated.

> [!TIP]
> Scroll to the bottom of any documentation page to find the **Translate this page** link:
> 
> ## 🗺️ Change locale

You can also switch between available languages using the language selector in the footer of any page:

This allows you to view translated content for pages that have already been localized.

## 🌍 About translations

The `aspire.dev` documentation supports multiple languages to help developers worldwide learn and use Aspire in their preferred language. We use [Lunaria](https://lunaria.dev/) to track translation progress and manage the localization workflow.

### Supported languages

We currently support the following languages:

| Language | Code | Status |
|----------|------|--------|
| English | `en` | Source (default) |
| Deutsch (German) | `de` | In progress |
| Español (Spanish) | `es` | In progress |
| Français (French) | `fr` | In progress |
| Italiano (Italian) | `it` | In progress |
| 日本語 (Japanese) | `ja` | In progress |
| 한국어 (Korean) | `ko` | In progress |
| Português do Brasil | `pt-br` | In progress |
| Русский (Russian) | `ru` | In progress |
| 简体中文 (Simplified Chinese) | `zh-cn` | In progress |
| Türkçe (Turkish) | `tr` | In progress |
| हिंदी (Hindi) | `hi` | In progress |
| Dansk (Danish) | `da` | In progress |
| Bahasa Indonesia | `id` | In progress |
| Українська (Ukrainian) | `uk` | In progress |

## 📊 Check translation status

Before starting a translation, check the current status of translations using our Lunaria dashboard:

View Translation Status
The dashboard shows:

- **Overall progress** for each language
- **Individual page status** (translated, outdated, or missing)
- **Quick links** to create or update translations

### Understanding the status page

The Lunaria dashboard displays translation progress with the following indicators:

- ✅ **Done** - The page is fully translated and up to date
- 🔄 **Outdated** - The source content has changed since the translation was made
- ❌ **Missing** - The page has not been translated yet

## 🚀 Getting started with translations

<Steps>
<Step stepNumber="1">
**Visit the translation dashboard**

Go to [aspire.dev/i18n/](https://aspire.dev/i18n/) to see the current translation status for all languages.

</Step>
<Step stepNumber="2">
**Find a page to translate**

Look for pages marked as "Missing" or "Outdated" in your language. Missing pages are great starting points!

</Step>
<Step stepNumber="3">
**Create or update the translation file**

Click on the page link in the dashboard to navigate to the source file. The translated file should be created at the corresponding path under your language's directory.

For example, if you're translating `src/content/docs/get-started/what-is-aspire.mdx` to Japanese, create:

```mdx
src/content/docs/ja/get-started/what-is-aspire.mdx
```

</Step>
<Step stepNumber="4">
**Submit a pull request**

Once you've completed your translation, submit a pull request to the repository. See our [Contributor guide](/community/contributor-guide) for detailed instructions on the PR process.

</Step>
</Steps>

## 📁 File structure for translations

Translations follow a specific directory structure:

> [!TIP]
> When creating a new translation file, copy the original English file first, then translate the content. This ensures you maintain the correct frontmatter structure and any component imports.

## ✍️ Translation best practices

Follow these best practices to ensure your translations are helpful, accurate, and consistent with the rest of the documentation.

### General guidelines

1. **Preserve frontmatter** - Keep the `title` and other frontmatter fields, but translate their values where appropriate.

2. **Keep code blocks unchanged** - Code examples, command-line instructions, and file paths should generally remain in English.

3. **Translate alt text** - Image `alt` attributes should be translated for accessibility.

4. **Maintain links** - Keep internal links pointing to the same slugs; Starlight handles language routing automatically.

5. **Preserve component syntax** - Astro components like `<Aside>`, ``, and `` should keep their original syntax, but translate the content within them.

### Quality over quantity

- **Accuracy matters** - It's better to have fewer, high-quality translations than many poor ones.
- **Technical terms** - Some technical terms are better left in English if there's no commonly accepted translation in your language.
- **Consistency** - Use consistent terminology throughout your translations. Check existing translations in your language for reference.

### Example translation

Here's an example of translating a simple page:


**English (source)**

```mdx
---
title: What is Aspire?
description: Learn how Aspire simplifies development.
---

Aspire streamlines building, running, debugging, and deploying distributed apps.

## Why Aspire?

Building modern applications means juggling multiple services, databases, and dependencies.
```

**Japanese (translation)**

```mdx
---
title: Aspire とは何ですか？
description: Aspire が分散アプリケーションの開発をどのように簡素化するかを学ぶ
---

Aspire は、分散アプリケーションの構築、実行、デバッグ、デプロイを効率化します。

## 主なメリット

モダンなアプリケーションを構築するには、複数のサービス、データベース、依存関係を管理する必要があります。
```


## 🤖 Machine translations

Some content may initially be machine-translated to provide a starting point for human translators. We appreciate contributors who:

- **Review machine translations** for accuracy and natural phrasing
- **Improve existing translations** with better terminology or clearer explanations
- **Fix errors** in grammar, spelling, or technical accuracy
- **Add cultural context** where appropriate

> [!NOTE]
> Machine translations are a starting point, not a finished product. Human review and improvement are essential for quality documentation.

## 🔄 Updating outdated translations

When the source English content changes, translations become "outdated" in the Lunaria dashboard. To update an outdated translation:

<Steps>
<Step stepNumber="1">
Check what changed in the source file by reviewing the git history or comparing versions.

</Step>
<Step stepNumber="2">
Update the translated file to reflect the changes in the source.

</Step>
<Step stepNumber="3">
Submit a pull request with your updates.

</Step>
</Steps>

## 💡 Tips for translators

- **Start small** - Begin with shorter pages to get familiar with the process.
- **Use translation memory** - Keep notes of how you translate common terms for consistency.
- **Ask questions** - If you're unsure about something, open a discussion or ask in the PR.
- **Collaborate** - Connect with other translators for your language to maintain consistency.

## 🆘 Getting help

If you have questions about translations or need help:

- **GitHub Discussions** - Ask questions in [GitHub Discussions](https://github.com/microsoft/aspire.dev/discussions)
- **Discord** - Join the [Aspire Discord](https://discord.com/invite/raNPcaaSj8) community
- **Issues** - Report translation-related issues on [GitHub Issues](https://github.com/microsoft/aspire.dev/issues)

## See also

- [Contributor guide](/community/contributor-guide) - General contribution guidelines
- [Starlight i18n documentation](https://starlight.astro.build/guides/i18n/) - Technical details about Starlight's internationalization
- [Lunaria documentation](https://lunaria.dev/) - Learn more about the translation tracking tool
