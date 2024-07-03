using Common.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class ProfileService : GenericService<EmployeeProfile>, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository) : base(profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<string> SetupProfile(EmployeeProfileModel profile)
        {
            var existingProfile = await _profileRepository.Where(p => p.UserId == profile.UserId).FirstOrDefaultAsync();
            if (existingProfile != null)
            {
                existingProfile.DietPreference = profile.DietPreference.ToString();
                existingProfile.SpiceLevel = profile.SpiceLevel.ToString();
                existingProfile.CuisinePreference = profile.CuisinePreference.ToString();
                existingProfile.HasSweetTooth = profile.HasSweetTooth;


                return "Succesfully updated as you had already setup the profile.";
            }
            else
            {
                EmployeeProfile employeeProfile = new EmployeeProfile
                {
                    UserId = profile.UserId,
                    SpiceLevel = profile.SpiceLevel.ToString(),
                    CuisinePreference = profile.CuisinePreference.ToString(),
                    DietPreference = profile.DietPreference.ToString(),
                    HasSweetTooth = profile.HasSweetTooth
                };
                await _profileRepository.CreateAsync(employeeProfile);
                return "Successfully created the profile";
        }
    }
}
}
