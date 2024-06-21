namespace ServerSide.Entity
{
    public class VotingResult
    {
        public int Id {  get; set; }
        public int MenuItemId { get; set; }
        public int NoOfVotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MealtypeId { get; set; }
        public MealType MealType { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}