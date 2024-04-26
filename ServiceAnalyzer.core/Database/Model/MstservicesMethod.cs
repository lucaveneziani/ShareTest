namespace ServiceAnalyzer.core.Database.Model;

public partial class MstservicesMethod
{
    public int IdMetodo { get; set; }
    public int IdService { get; set; }
    public int Tipo { get; set; }
    public string Descrizione { get; set; } = null!;
}
