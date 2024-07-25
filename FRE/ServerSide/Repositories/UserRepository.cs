using Microsoft.EntityFrameworkCore;
using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserType> GetUserType(int id)
        {
            return await _dbContext.UserTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
