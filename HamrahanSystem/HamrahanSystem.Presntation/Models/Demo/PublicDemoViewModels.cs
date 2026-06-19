namespace HamrahanSystem.Presntation.Models.Demo;

public class PublicDemoLandingViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Highlight { get; set; } = string.Empty;
    public List<DemoStat> Stats { get; set; } = [];
    public List<DemoFeature> Features { get; set; } = [];
    public List<DemoStory> Stories { get; set; } = [];
}

public class PublicDemoWorkspaceViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public List<DemoSummaryCard> SummaryCards { get; set; } = [];
    public List<DemoOrderRow> Orders { get; set; } = [];
    public List<DemoCatalogItem> Catalog { get; set; } = [];
    public List<DemoWorkflowStep> Workflow { get; set; } = [];
}

public record DemoStat(string Value, string Label, string Caption);
public record DemoFeature(string Title, string Description);
public record DemoStory(string Title, string Description);
public record DemoSummaryCard(string Title, string Value, string Caption, string AccentClass);
public record DemoOrderRow(string Code, string Customer, string Product, string Status, string UpdatedAt);
public record DemoCatalogItem(string Brand, string Design, string Indexes, string Extras);
public record DemoWorkflowStep(string Title, string Description);
