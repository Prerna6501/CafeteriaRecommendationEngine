using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class DiscardItemRepository : GenericRepository<DiscardItem>, IDiscardItemRepository
    {
        public DiscardItemRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
