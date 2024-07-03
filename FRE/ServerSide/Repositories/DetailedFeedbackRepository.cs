using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class DetailedFeedbackRepository : GenericRepository<DetailedFeedback>, IDetailedFeedbackRepository
    {
        public DetailedFeedbackRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
