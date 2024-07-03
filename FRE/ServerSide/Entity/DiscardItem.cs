namespace ServerSide.Entity
{
    public class DiscardItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string Status { get; set; }
        public double AverageRating { get; set; }
        public string Sentiments { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<DetailedFeedback> DetailedFeedbacks { get; set; }
    }
}
