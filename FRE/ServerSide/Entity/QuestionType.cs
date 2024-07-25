namespace ServerSide.Entity
{
    public class QuestionType
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<DetailedFeedback> DetailedFeedbacks { get; set; }
    }
}
