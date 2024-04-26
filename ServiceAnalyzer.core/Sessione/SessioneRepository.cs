using MasterSoft.Core.Config;
using MasterSoft.Core.Sessione;
using ServiceAnalyzer.Core.Sessione;
using System.Reflection;

namespace GestioneNotifiche.Core.Sessione
{
    public class SessioneRepository : ISessioneRepository
    {
        private readonly Assembly m_assembly;
        private readonly IConfigurazioneModel m_configurazione;
        private static bool IsDebug
        {
            get
            {
#if (DEBUG)
                return true;
#else
                return false;
#endif
            }
        }

        public SessioneRepository(Assembly assembly, IConfigurazioneModel configurazione)
        {
            m_assembly = assembly;
            m_configurazione = configurazione;
        }

        public ISessioneModel Get()
        {
            return new SessioneModel
            {
                Configurazione = m_configurazione,
                LogFilePath = GetLogFilePath(),
            };
        }

        private string GetLogFilePath()
        {
            var assemblyPath = Path.GetDirectoryName(m_assembly.Location);
            var path = assemblyPath == null ? "" : assemblyPath;
            return Path.Combine(path, "Log");
        }
    }
}
