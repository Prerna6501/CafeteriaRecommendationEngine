namespace ServerSide.Entity
{
    public class Feedback
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }     
        public MenuItem MenuItem { get; set; }
        public User User { get; set; }
    }
}
