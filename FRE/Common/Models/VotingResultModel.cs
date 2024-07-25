namespace Common.Models
{
    public class VotingResultModel
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int Votes { get; set; }
        public string MealType { get; set; }
    }
}