using GraspItEz.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GraspItEz
{
    public class Seeder   
    {
        private readonly GraspItEzContext _dbContext;
        public Seeder(GraspItEzContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public void Seed() 
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.QueryStatuses.Any())
                {
                    var QueryStatues = GetQueryStatus();
                    _dbContext.AddRange(QueryStatues);
                    _dbContext.SaveChanges();

                    
                }
            }
        }
        private static IEnumerable<QueryStatus> GetQueryStatus()
        {
            var queryStatuses = new List<QueryStatus>()
                        {
                            new QueryStatus()
                            {
                                
                                QueryStatusValue = "Not Started",
                            },
                            new QueryStatus()
                            {
                                
                                QueryStatusValue = "In progress",
                            },
                            new QueryStatus()
                            {
                                QueryStatusValue = "Learned",
                            }
                        };
            return queryStatuses;
        }
    }
}
