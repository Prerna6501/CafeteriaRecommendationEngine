using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
