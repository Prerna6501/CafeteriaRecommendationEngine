using ServerSide.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Services.Interfaces
{
    public interface IChefService
    {
        public Task<string> GetTopMenuItemsByMealType(string parameters);
        public Task<string> GetVotingResults();
    }
}
