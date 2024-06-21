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
        public List<Feedback> Feedbacks { get; set; }   
        public List<FixedMeal> FixedMeals { get; set; }
    }
}
