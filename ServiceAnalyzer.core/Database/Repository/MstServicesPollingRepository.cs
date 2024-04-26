using Microsoft.EntityFrameworkCore;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.core.Database.Model;

namespace ServiceAnalyzer.Core.Database.Repository
{
    public class MstServicesPollingRepository : IMstServicesPollingRepository
    {
        private readonly MstmonitoraggioContext _dbContext;
        public MstServicesPollingRepository(MstmonitoraggioContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int ClearServicesPollingTable(int logDaysBackup)
        {
            var dateFromDelete = DateTime.Now.AddDays(-logDaysBackup);
            var liLogToDelete = _dbContext.MstservicesPollings.Where(x => x.DataChiamata < dateFromDelete);

            _dbContext.MstservicesPollings.RemoveRange(liLogToDelete);
            var res = _dbContext.SaveChanges();

            return res;
        }
        public void Update(MstservicesPolling updatedRecord)
        {
            var original = _dbContext.MstservicesPollings.First(x => x.IdServicesPolling == updatedRecord.IdServicesPolling);

            if (original != null)
            {
                _dbContext.Entry(original).CurrentValues.SetValues(updatedRecord);
                _dbContext.SaveChanges();
            }
        }
        public List<MstservicesPolling> GetLastPollingMethods(int tipo, int idServizio)
        {
            var res = _dbContext.MstservicesPollings.FromSqlRaw<MstservicesPolling>("spGetLastPollingMethods {0},{1}", tipo, idServizio).ToList();
            return res;
        }
    }
}
