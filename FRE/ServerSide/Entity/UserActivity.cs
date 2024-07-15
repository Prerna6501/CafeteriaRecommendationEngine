namespace ServerSide.Entity
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ActivityTime { get; set; }
        public string ActivityType { get; set; }
    }
}
