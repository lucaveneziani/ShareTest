namespace ServiceAnalyzer.core.Database.Model;

public partial class Mstservice
{
    public int IdService { get; set; }

    public string GuidServizio { get; set; } = null!;

    public string HostName { get; set; } = null!;
}
