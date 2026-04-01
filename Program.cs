using Aspire.Dev.Docs;
using Aspire.Dev.Docs.Components;
using Mdazor;
using MyLittleContentEngine;
using System.Collections.Immutable;
using MonorailCss.Parser.Custom;
using MonorailCss.Theme;
using MyLittleContentEngine.MonorailCss;
using MyLittleContentEngine.UI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();

builder.Services.AddMdazor()
    .AddMdazorComponent<Badge>()
    .AddMdazorComponent<Card>()
    .AddMdazorComponent<CardGrid>()
    .AddMdazorComponent<LinkCard>()
    .AddMdazorComponent<Step>()
    .AddMdazorComponent<Steps>()
    .AddMdazorComponent<InstallPackage>()
    .AddMdazorComponent<YouTubeEmbed>()
    .AddMdazorComponent<IntegrationIcon>();

builder.Services.AddContentEngineService(_ => new ContentEngineOptions
    {
        SiteTitle = "Aspire",
        SiteDescription = "Your stack, streamlined.",
        ContentRootPath = "Content",
    })
    // Root content (home page, standalone pages)
    .WithMarkdownContentService(_ => new MarkdownContentOptions<AspireFrontMatter>
    {
        ContentPath = "Content",
        BasePageUrl = string.Empty,
        ExcludeSubfolders = true,
    })
    // Docs section: get-started, fundamentals, architecture, app-host, etc.
    .WithMarkdownContentService(_ => new MarkdownContentOptions<DocsFrontMatter>
    {
        ContentPath = "Content/docs",
        BasePageUrl = "/docs",
        TableOfContentsSectionKey = "docs",
    })
    // Integrations section
    .WithMarkdownContentService(_ => new MarkdownContentOptions<IntegrationsFrontMatter>
    {
        ContentPath = "Content/integrations",
        BasePageUrl = "/integrations",
        TableOfContentsSectionKey = "integrations",
    })
    // Dashboard section
    .WithMarkdownContentService(_ => new MarkdownContentOptions<DashboardFrontMatter>
    {
        ContentPath = "Content/dashboard",
        BasePageUrl = "/dashboard",
        TableOfContentsSectionKey = "dashboard",
    })
    // Deployment section
    .WithMarkdownContentService(_ => new MarkdownContentOptions<DeploymentFrontMatter>
    {
        ContentPath = "Content/deployment",
        BasePageUrl = "/deployment",
        TableOfContentsSectionKey = "deployment",
    })
    // Reference section
    .WithMarkdownContentService(_ => new MarkdownContentOptions<ReferenceFrontMatter>
    {
        ContentPath = "Content/reference",
        BasePageUrl = "/reference",
        TableOfContentsSectionKey = "reference",
    })
    // Community section
    .WithMarkdownContentService(_ => new MarkdownContentOptions<CommunityFrontMatter>
    {
        ContentPath = "Content/community",
        BasePageUrl = "/community",
        TableOfContentsSectionKey = "community",
    });

builder.Services.AddMonorailCss(_ => new MonorailCssOptions
{
    ColorScheme = new NamedColorScheme()
    {
        PrimaryColorName = "aspire-purple",
        AccentColorName = "",
        TertiaryOneColorName = "",
        TertiaryTwoColorName = "",
        BaseColorName = "aspire-base",
    },
    CustomCssFrameworkSettings = settings => settings with
    {
        Theme = settings.Theme.AddColorPalette("aspire-base", new Dictionary<string, string>
        {
            { "50", "oklch(98.4% 0.003 272.97)" },
            { "100", "oklch(96.8% 0.007 273.01)" },
            { "200", "oklch(92.9% 0.013 280.62)" },
            { "300", "oklch(86.9% 0.022 278.01)" },
            { "400", "oklch(70.4% 0.04 281.9)" },
            { "500", "oklch(55.4% 0.046 282.53)" },
            { "600", "oklch(44.6% 0.043 282.4)" },
            { "700", "oklch(37.2% 0.044 282.4)" },
            { "800", "oklch(27.9% 0.041 285.15)" },
            { "900", "oklch(20.8% 0.042 290.87)" },
            { "950", "oklch(12.9% 0.042 289.81)" },
        }.ToImmutableDictionary())
        .AddColorPalette("aspire-purple", new Dictionary<string, string>
        {
            { "50", "oklch(96.2% 0.018 277.69)" },
            { "100", "oklch(93.071% 0.03329 278.16)" },
            { "200", "oklch(87.143% 0.06357 279.41)" },
            { "300", "oklch(78.714% 0.11286 280.09)" },
            { "400", "oklch(67.586% 0.17914 282.31)" },
            { "500", "oklch(58.857% 0.22943 282.49)" },
            { "600", "oklch(51.529% 0.25771 282.34)" },
            { "700", "oklch(46.2% 0.235 282.4)" },
            { "800", "oklch(40.133% 0.19167 282.74)" },
            { "900", "oklch(36.067% 0.14233 284.07)" },
            { "950", "oklch(25.7% 0.09 286.66)" },
        }.ToImmutableDictionary())
        .AddFontFamily("sans", "'Outfit', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif")
        .AddFontFamily("mono", "'Fira Code', ui-monospace, monospace"),
        CustomUtilities = [
            new UtilityDefinition
            {
                Pattern = "scrollbar-thin",
                Declarations = ImmutableList.Create(
                    new CssDeclaration("scrollbar-width", "thin")
                )
            },
            new UtilityDefinition
            {
                Pattern = "scrollbar-thumb-*",
                IsWildcard = true,
                Declarations = ImmutableList.Create(
                    new CssDeclaration("--tw-scrollbar-thumb-color", "--value(--color-*)")
                )
            },
            new UtilityDefinition
            {
                Pattern = "scrollbar-track-*",
                IsWildcard = true,
                Declarations = ImmutableList.Create(
                    new CssDeclaration("--tw-scrollbar-track-color", "--value(--color-*)")
                )
            },
            new UtilityDefinition
            {
                Pattern = "scrollbar-color",
                Declarations = ImmutableList.Create(
                    new CssDeclaration("scrollbar-color", "var(--tw-scrollbar-thumb-color) var(--tw-scrollbar-track-color)")
                )
            },
        ],
    },
    ExtraStyles = AspireStyles.Css,
});

var app = builder.Build();
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>();
app.UseMonorailCss();

await app.RunOrBuildContent(args);
