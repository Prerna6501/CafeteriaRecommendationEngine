using Common.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class VotingResultService : GenericService<VotingResult>, IVotingResultService
    {
        private readonly IVotingResultRepository _votingResultRepository;
        private readonly IMealTypeRepository _mealTypeRepository;
        public VotingResultService(IVotingResultRepository votingResultRepository, IMealTypeRepository mealTypeRepository) : base(votingResultRepository)
        {
            _votingResultRepository = votingResultRepository;
            _mealTypeRepository = mealTypeRepository;
        }
        public async Task<List<VotingResultModel>> GetVotingResults()
        {
            var votingResult = await _votingResultRepository.GetVotingResults();
            var votingResultModel = new List<VotingResultModel>();
            foreach (var result in votingResult)
            {
                votingResultModel.Add(new VotingResultModel
                {
                    Id = result.Id,
                    MenuItemId = result.MenuItemId,
                    MenuItemName = result.MenuItem.Name,
                    MealType = result.MealType.Name,
                    Votes = result.NoOfVotes
                });
            }
            return votingResultModel;
        }

        public async Task<int> GetVoteCount(int menuItemId)
        {
            return await _votingResultRepository.GetVoteCount(menuItemId);
        }

        public async Task<string> CreateVotingForRolledOutChoices(string request)
        {
            return await _votingResultRepository.CreateVotingForRolledOutChoices(request);
        }

        public async Task<string> VoteMenuItems(string parameters)
        {
            var segments = parameters.Split(';');
            List<int> InvalidIds = new List<int>();
            foreach (var segment in segments)
            {
                var mealTypeAndItems = segment.Split(':');
                if (mealTypeAndItems.Length != 2)
                {
                    continue;
                }

                var mealTypeName = mealTypeAndItems[0];
                var itemIds = mealTypeAndItems[1].Split(',').Select(int.Parse);
                var mealType = await _mealTypeRepository.Where(mt => mt.Name == mealTypeName).FirstOrDefaultAsync();

                foreach (var itemId in itemIds)
                {
                    var existingResult = await _votingResultRepository.Where(vr => vr.MenuItemId == itemId && vr.MealtypeId == mealType.Id).FirstOrDefaultAsync();
                    if (existingResult != null)
                    {
                        existingResult.NoOfVotes++;
                        await _votingResultRepository.UpdateAsync(existingResult);
                    }
                    else { InvalidIds.Add(itemId); }
                }
            }
            if (InvalidIds.Count != 0) { return $"Invalid Ids : {InvalidIds}"; }
            return "Votes recorded successfully";
        }
    }
}
