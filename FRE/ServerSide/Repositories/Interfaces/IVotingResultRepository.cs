using ServerSide.Entity;

namespace ServerSide.Repositories.Interfaces
{
    public interface IVotingResultRepository : IGenericRepository<VotingResult>
    {
        public Task<List<VotingResult>> GetVotingResults();
        public Task<int> GetVoteCount(int menuItemId);
        public Task<string> CreateVotingForRolledOutChoices(string request);
    }
}
