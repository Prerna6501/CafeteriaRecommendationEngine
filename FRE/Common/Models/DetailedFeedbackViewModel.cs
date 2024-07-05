namespace Common.Models
{
    public class DetailedFeedbackViewModel
    {
        public int Id { get; set; }
        public int DiscardItemId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string Question { get; set; }
    }
}
