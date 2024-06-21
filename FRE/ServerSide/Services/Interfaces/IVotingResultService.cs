using ServerSide.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Services.Interfaces
{
    public interface IVotingResultService : IGenericService<VotingResult>
    {
        public Task<List<VotingResult>> GetVotingResults();
        public Task<int> GetVoteCount(int menuItemId);
    }
}
