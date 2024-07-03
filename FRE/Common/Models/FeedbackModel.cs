namespace Common.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; }
        public int MenuItemId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
