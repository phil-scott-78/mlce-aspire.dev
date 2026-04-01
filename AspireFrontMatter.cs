using MyLittleContentEngine.Models;

namespace Aspire.Dev.Docs;

public class AspireFrontMatter : IFrontMatter
{
    public string Title { get; init; } = "Untitled";
    public string Description { get; init; } = string.Empty;
    public bool IsDraft { get; init; }
    public string[] Tags { get; init; } = [];
    public int Order { get; init; } = int.MaxValue;
    public string? RedirectUrl { get; init; }
    public string? Section { get; init; }
    public string? Uid { get; init; }

    public Metadata AsMetadata() => new()
    {
        Title = Title,
        Description = Description,
        LastMod = DateTime.MinValue,
        RssItem = false,
        Order = Order
    };
}

// Marker types for DI — each section needs a distinct type so MLCE can resolve
// the correct IMarkdownContentService<T> per route.
public class DocsFrontMatter : AspireFrontMatter;
public class IntegrationsFrontMatter : AspireFrontMatter;
public class DashboardFrontMatter : AspireFrontMatter;
public class DeploymentFrontMatter : AspireFrontMatter;
public class ReferenceFrontMatter : AspireFrontMatter;
public class CommunityFrontMatter : AspireFrontMatter;
