using Common.Models;
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
    public class VotingResultService : GenericService<VotingResult> , IVotingResultService
    {
        private readonly IVotingResultRepository _votingResultRepository;
        public VotingResultService(IVotingResultRepository votingResultRepository) : base(votingResultRepository)
        {
            _votingResultRepository = votingResultRepository;
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
    }
}
