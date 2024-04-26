using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.core.Database.Model;

namespace ServiceAnalyzer.Core.Logger
{
    public class LoggerFileWriterService : ILoggerFileWriter
    {
        private static readonly object Locker = new object();
        private readonly ISessioneModel m_sessione;
        private readonly MstmonitoraggioContext _dbContext;

        public LoggerFileWriterService(ISessioneModel sessione, MstmonitoraggioContext dbContext)
        {
            m_sessione = sessione;
            _dbContext = dbContext;
        }

        public void Write(string message, int idService, string type, string method)
        {
            lock (Locker)
            {
                CreateDirectoryIfNeed();
                DeleteOld();

                if (_dbContext.Database.CanConnect())
                {
                    var log = new Mstlog() { Data = DateTime.Now, IdService = idService, Messaggio = message, Tipo = type, Metodo = method };

                    _dbContext.Mstlogs.Add(log);
                    _dbContext.SaveChanges();
                }
                else
                    WriteToFile(string.Format("SERVICE {0} - TYPE {1} - METHOD {2} - {3}", idService, type, method, message));
            }
        }

        private void WriteToFile(string message)
        {
            var data = DateTime.Today;
            var fileName = Path.Combine(m_sessione.LogFilePath, string.Format("{0}{1}{2}.txt", data.Year.ToString("0000"), data.Month.ToString("00"), data.Day.ToString("00")));

            using (var sw = File.AppendText(Path.Combine(fileName)))
            {
                sw.WriteLine(message);
            }
        }

        private void CreateDirectoryIfNeed()
        {
            if (!Directory.Exists(m_sessione.LogFilePath))
                Directory.CreateDirectory(m_sessione.LogFilePath);
        }

        private void DeleteOld()
        {
            var liLogs = Directory.GetFiles(m_sessione.LogFilePath).OrderByDescending(i => i).ToArray();
            var liLogToSave = liLogs.Take(Convert.ToInt32(m_sessione.Configurazione.LogDaysBackup)).ToArray();

            DoAction(liLogs.Except(liLogToSave).ToArray(), (File.Delete));
        }
        public static IEnumerable<T> DoAction<T>(IEnumerable<T> liItems, Action<T> doAction)
        {
            foreach (T liItem in liItems)
            {
                doAction(liItem);
            }

            return liItems;
        }
    }
}
