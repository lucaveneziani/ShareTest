namespace ServiceAnalyzer.core.Database.Model;

public partial class Mstlog
{
    public int IdLog { get; set; }

    public DateTime Data { get; set; }

    public int IdService { get; set; }

    public string Tipo { get; set; } = "";

    public string Metodo { get; set; } = "";

    public string? Messaggio { get; set; }
}
