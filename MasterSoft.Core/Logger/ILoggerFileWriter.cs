namespace MasterSoft.Core.Logger
{
    public interface ILoggerFileWriter
    {
        public void Write(string message, int idService, string type, string method);
    }
}
