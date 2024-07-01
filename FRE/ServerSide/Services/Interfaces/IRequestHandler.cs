using ServerSide.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Services.Interfaces
{
    public interface IRequestHandler
    {
        public Task<string> GetTopMenuItemsByMealType(string parameters);
        public Task<string> GetVotingResults();
        public Task<string> RolloutChoices(string message);
        public Task<string> RolloutFinalMeal(string message);
        public Task<string> VoteMenuItems(string parameters);
    }
}
