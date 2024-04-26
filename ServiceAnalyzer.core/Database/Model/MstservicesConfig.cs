namespace ServiceAnalyzer.core.Database.Model;

public partial class MstservicesConfig
{
    public int IdServiceConfig { get; set; }

    public int IdService { get; set; }

    public int IdMetodo { get; set; }

    public string ParName { get; set; } = null!;

    public string? ParValue { get; set; }
}
