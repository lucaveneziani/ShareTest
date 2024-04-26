namespace ServiceAnalyzer.Core.Database.Repository
{
    public interface IMstLogRepository
    {
        int ClearLogTable(int logDaysBackup);
    }
}
