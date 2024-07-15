namespace ServerSide.Entity
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string ActivityType { get; set; }
    }
}
