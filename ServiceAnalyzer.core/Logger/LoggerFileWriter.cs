using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.core.Database.Model;

namespace ServiceAnalyzer.Core.Logger
{
    public class LoggerFileWriter : ILoggerFileWriter
    {
        private static readonly object Locker = new object();
        private readonly ISessioneModel m_sessione;
        private readonly MstmonitoraggioContext _dbContext;

        public LoggerFileWriter(ISessioneModel sessione, MstmonitoraggioContext dbContext)
        {
            m_sessione = sessione;
            _dbContext = dbContext;
        }

        public void Write(string message, int idService, string type, string method)
        {
            lock (Locker)
            {
                var log = new Mstlog() { Data = DateTime.Now, IdService = idService, Messaggio = message, Tipo = type, Metodo = method };

                _dbContext.Mstlogs.Add(log);
                _dbContext.SaveChanges();
            }
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
