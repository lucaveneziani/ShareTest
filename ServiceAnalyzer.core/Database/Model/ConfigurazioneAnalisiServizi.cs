namespace ServiceAnalyzer.Core.Database.Model
{
    public class ConfigurazioneAnalisiServizi
    {
        public int IdService { get; set; }
        public int IdMetodo { get; set; }
        public string? Descrizione { get; set; } = "";
        public int IdServiceConfig { get; set; }
        public string? ParName { get; set; } = "";
        public string? ParValue { get; set; } = "";
    }
}
