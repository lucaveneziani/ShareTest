using Microsoft.EntityFrameworkCore;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.Core.Database.Model;

namespace ServiceAnalyzer.Core.Database.Repository
{
    public class MstServicesConfigRepository : IMstServicesConfig
    {
        private readonly MstmonitoraggioContext _dbContext;
        public MstServicesConfigRepository(MstmonitoraggioContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ConfigurazioneAnalisiServizi> GetConfigurazioneAnalisiServizi()
        {
            var res = _dbContext.ConfigurazioneAnalisiServizis.FromSqlRaw<ConfigurazioneAnalisiServizi>("spGetConfigurazioneAnalisiServizi").ToList();
            return res;
        }
    }
}
