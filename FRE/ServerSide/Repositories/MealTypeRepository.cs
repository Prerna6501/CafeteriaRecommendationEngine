using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class MealTypeRepository : GenericRepository<MealType>, IMealTypeRepository
    {
        public MealTypeRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
