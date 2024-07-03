using Common.Models;
using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IProfileService : IGenericService<EmployeeProfile>
    {
        public Task<string> SetupProfile(EmployeeProfileModel profile);
    }
}
