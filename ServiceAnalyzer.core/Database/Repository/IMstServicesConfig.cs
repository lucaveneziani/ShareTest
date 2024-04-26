using ServiceAnalyzer.Core.Database.Model;

namespace ServiceAnalyzer.Core.Database.Repository
{
    public interface IMstServicesConfig
    {
        List<ConfigurazioneAnalisiServizi> GetConfigurazioneAnalisiServizi();
    }
}
