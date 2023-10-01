using GraspItEz.Database;
using Microsoft.EntityFrameworkCore;

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
            }
        }
    }
}
