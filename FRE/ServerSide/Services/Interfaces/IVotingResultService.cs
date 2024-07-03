using Common.Models;
using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IVotingResultService : IGenericService<VotingResult>
    {
        public Task<List<VotingResultModel>> GetVotingResults();
        public Task<int> GetVoteCount(int menuItemId);
        public Task<string> CreateVotingForRolledOutChoices(string request);
        public Task<string> VoteMenuItems(string parameters);
    }
}
