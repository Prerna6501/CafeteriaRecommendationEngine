using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Services
{
    public class UserActivityService : GenericService<UserActivity>, IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public UserActivityService(IUserActivityRepository userActivityRepository) : base(userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task LogUserActivity(int userId, string activityType)
        {
            var userActivity = new UserActivity
            {
                UserId = userId,
                ActivityTime = DateTime.Now, 
                ActivityType = activityType
            };

            await _userActivityRepository.CreateAsync(userActivity);
        }
    }
}
