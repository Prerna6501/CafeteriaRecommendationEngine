namespace ServerSide.Entity
{
    public class FixedMeal
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int MealTypeId { get; set; }
        public DateTime PreparedDate { get; set; }
        public MenuItem MenuItem { get; set; }
        public MealType MealType { get; set; }
    }
}
