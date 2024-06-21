namespace ServerSide.Entity
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FixedMeal> Menu { get; set; }
        public List<VotingResult> VotingResults { get; set; }
    }
}
