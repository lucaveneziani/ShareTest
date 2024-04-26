using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using ServiceAnalyzer.core.Database;

namespace ServiceAnalyzer.Core.Logger
{
    public class LoggerService : ILoggerFile
    {
        private readonly ILoggerFileWriter m_writer;
        public string DefaultFormat = "dd/MM/yyyy HH:mm:ss";

        public LoggerService(ISessioneModel sessione, MstmonitoraggioContext dbContext)
        {
            m_writer = new LoggerFileWriterService(sessione, dbContext);
        }

        public string ToDefaultFormat(DateTime date)
        {
            return date.ToString(DefaultFormat);
        }

        public void Trace(string message, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | TRACE | {1}", ToDefaultFormat(DateTime.Now), message), idService, type, method);
        }

        public void Exception(string message, Exception ex, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | ERROR | {1} | {2} | {3} | {4}", ToDefaultFormat(DateTime.Now), message, ex.GetType().FullName, ex.Message, ex.StackTrace), idService, type, method);
        }

        public void FeatureInvoke(string message, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | FEATURE INVOKE | {1}", ToDefaultFormat(DateTime.Now), message), idService, type, method);
        }

        public void Info(string message, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | INFO | {1}", ToDefaultFormat(DateTime.Now), message), idService, type, method);
        }

        public bool Ask(string message, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | ASK | {1}", ToDefaultFormat(DateTime.Now), message), idService, type, method);
            return true;
        }

        public void Warming(string message, int idService, string type, string method)
        {
            m_writer.Write(string.Format("{0} | WARMING | {1}", ToDefaultFormat(DateTime.Now), message), idService, type, method);
        }     
    }
}
