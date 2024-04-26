using MasterSoft.Core.Config;

namespace MasterSoft.Core.Sessione
{
    public interface ISessioneModel
    {
        IConfigurazioneModel Configurazione { get; set; }
        string LogFilePath { get; set; }
        int IdServizio { get; set; }
        string GuidServizio { get; set; }
        string IndirizzoIpServizio { get; set; }
    }
}
