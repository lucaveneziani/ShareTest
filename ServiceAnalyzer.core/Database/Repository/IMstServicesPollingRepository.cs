using ServiceAnalyzer.core.Database.Model;

namespace ServiceAnalyzer.Core.Database.Repository
{
    public interface IMstServicesPollingRepository
    {
        int ClearServicesPollingTable(int logDaysBackup);
        void Update(MstservicesPolling updatedRecord);
        List<MstservicesPolling> GetLastPollingMethods(int tipo, int idServizio);
    }
}
