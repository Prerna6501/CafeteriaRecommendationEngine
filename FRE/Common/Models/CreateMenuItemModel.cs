using Common.Enums;

namespace Common.Models
{
    public class CreateMenuItemModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MenuItemTypeId { get; set; }
        public DietPreferenceEnum DietPreference { get; set; }
        public SpiceLevelEnum SpiceLevel { get; set; }
        public CuisinePreferenceEnum CuisinePreference { get; set; }
        public bool HasSweetTooth { get; set; }
        public bool AvailabilityStatus { get; set; }

    }
}
