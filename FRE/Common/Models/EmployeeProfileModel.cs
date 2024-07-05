using Common.Enums;

namespace Common.Models
{
    public class EmployeeProfileModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DietPreferenceEnum DietPreference { get; set; }
        public SpiceLevelEnum SpiceLevel { get; set; }
        public CuisinePreferenceEnum CuisinePreference { get; set; }
        public bool HasSweetTooth { get; set; }
    }
}
