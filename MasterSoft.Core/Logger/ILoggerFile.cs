namespace MasterSoft.Core.Logger
{
    public interface ILoggerFile
    {
        string ToDefaultFormat(DateTime date);
        void Trace(string message, int idService, string type, string method);
        void Exception(string message, Exception ex, int idService, string type, string method);
        void FeatureInvoke(string message, int idService, string type, string method);
        void Info(string message, int idService, string type, string method);
        bool Ask(string message, int idService, string type, string method);
        void Warming(string message, int idService, string type, string method);
    }
}
