namespace ServerSide.Entity
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FixedMeal> FixedMeals { get; set; }
        public List<VotingResult> VotingResults { get; set; }
    }
}
