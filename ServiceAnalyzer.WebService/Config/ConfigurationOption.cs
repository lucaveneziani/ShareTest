using MasterSoft.Core.Config;

namespace ServiceAnalyzer.WebService.Config
{
    public class ConfigurationOption : IConfigurazioneModel
    {
        public string ConnectionString { get; set; } = "";
        public int LogDaysBackup { get; set; }
    }
}
