namespace ServerSide.Entity
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public int MenuItemTypeId { get; set; }
        public string DietPreference { get; set; }
        public string SpiceLevel { get; set; }
        public string CuisinePreference { get; set; }
        public bool HasSweetTooth { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<FixedMeal> FixedMeals { get; set; }
        public List<VotingResult> VotingResults { get; set; }
        public List<DiscardItem> DiscardItems { get; set; }
        public MenuItemType MenuItemType { get; set; }
    }
}
