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

        public async Task<string> CreateVotingForRolledOutChoices(string request)
        {
            try
            {
                var segments = request.Split(';');
                
                foreach (var segment in segments)
                {
                    var mealTypeAndItems = segment.Split(':');
                    if (mealTypeAndItems.Length != 2)
                    {
                        continue;
                    }

                    var mealTypeName = mealTypeAndItems[0];
                    var itemIds = mealTypeAndItems[1].Split(',').Select(int.Parse);

                    var mealType = await _dbContext.MealTypes.FirstOrDefaultAsync(mt => mt.Name == mealTypeName);

                    foreach (var itemId in itemIds)
                    {
                        var menuItem = await _dbContext.MenuItems.FindAsync(itemId);
                        if (menuItem == null)
                        {
                            return $"MenuItem with ID {itemId} does not exist.";
                        }

                        var votingResult = new VotingResult
                        {
                            MenuItemId = menuItem.Id,
                            MealtypeId = mealType.Id,
                            NoOfVotes = 0,  
                            CreatedDate = DateTime.Now
                        };

                        _dbContext.VotingResults.Add(votingResult);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return "Choices recorded successfully";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
