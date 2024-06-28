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
        public async Task<List<VotingResult>> GetVotingResults()
        {
            return await _votingResultRepository.GetVotingResults();
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
