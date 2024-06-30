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
    public class FixedMealRepository : GenericRepository<FixedMeal> , IFixedMealRepository
    {
        public FixedMealRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
