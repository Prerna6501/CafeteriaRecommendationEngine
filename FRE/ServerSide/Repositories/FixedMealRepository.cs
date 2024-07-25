using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class FixedMealRepository : GenericRepository<FixedMeal>, IFixedMealRepository
    {
        public FixedMealRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
