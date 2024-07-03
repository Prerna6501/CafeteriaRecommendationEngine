namespace ServerSide.Entity
{
    public class DetailedFeedback
    {
        public int Id { get; set; }
        public int DiscardItemId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int QuestionTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DiscardItem DiscardItem { get; set; }
        public User User { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
