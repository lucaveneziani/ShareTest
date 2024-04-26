using ServiceAnalyzer.core.Database;

namespace ServiceAnalyzer.Core.Database.Repository
{
    public class MstLogRepository : IMstLogRepository
    {
        private readonly MstmonitoraggioContext _dbContext;
        public MstLogRepository(MstmonitoraggioContext dbContext) 
        {
            _dbContext = dbContext; 
        }
        public int ClearLogTable(int logDaysBackup)
        {
            var dateFromDelete = DateTime.Now.AddDays(-logDaysBackup);
            var liLogToDelete = _dbContext.Mstlogs.Where(x => x.Data < dateFromDelete);

            _dbContext.Mstlogs.RemoveRange(liLogToDelete);
            var res = _dbContext.SaveChanges();

            return res;
        }
    }
}
