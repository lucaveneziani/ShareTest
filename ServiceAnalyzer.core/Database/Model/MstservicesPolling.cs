namespace ServiceAnalyzer.core.Database.Model;

public partial class MstservicesPolling
{
    public int IdServicesPolling { get; set; }

    public int IdService { get; set; }

    public DateTime DataChiamata { get; set; }

    public int Tipo { get; set; }

    public int IdMetodo { get; set; }

    public string Testo { get; set; } = null!;

    public DateTime? DataOraMailInviata { get; set; }
}
