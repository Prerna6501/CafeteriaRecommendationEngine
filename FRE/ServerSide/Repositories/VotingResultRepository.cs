using Microsoft.EntityFrameworkCore;
using ServerSide.Data;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;

namespace ServerSide.Repositories
{
    public class VotingResultRepository : GenericRepository<VotingResult>, IVotingResultRepository
    {
        public VotingResultRepository(CafeteriaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetVoteCount(int menuItemId)
        {
            return await _dbContext.VotingResults.Where(vr => vr.MenuItemId == menuItemId).SumAsync(vr => vr.NoOfVotes);
        }

        public async Task<List<VotingResult>> GetVotingResults()
        {
            return await _dbContext.VotingResults.Include(vr => vr.MenuItem).ToListAsync();
        }
    }
}
