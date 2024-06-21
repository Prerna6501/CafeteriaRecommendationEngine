using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback> , IFeedbackRepository
    {
        public FeedbackRepository(CafeteriaDbContext dbContext) : base (dbContext)
        {
            
        }
    }
}
