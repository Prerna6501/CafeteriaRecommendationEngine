using ServerSide.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Repositories.Interfaces
{
    public interface IVotingResultRepository : IGenericRepository<VotingResult>
    {
        public Task<List<VotingResult>> GetVotingResults();
        public Task<int> GetVoteCount(int menuItemId);
    }
}
