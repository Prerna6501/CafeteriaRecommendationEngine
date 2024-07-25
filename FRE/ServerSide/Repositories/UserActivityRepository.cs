using Microsoft.EntityFrameworkCore;
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
    public class UserActivityRepository : GenericRepository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {

        }
    }
}
