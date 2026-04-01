namespace Aspire.Dev.Docs.Components.Landing;

public record CapabilityItem(string Icon, string Title, string Description, string? Href = null);

public record FeatureItem(
    string Icon,
    string Title,
    string Description,
    string Href,
    string? Label = null,
    string Accent = "purple");

public record JourneyStep(
    string Icon,
    string Title,
    string Description,
    string Href,
    string? Label = null);
