using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class ProfileRepository : GenericRepository<EmployeeProfile>, IProfileRepository
    {
        public ProfileRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
