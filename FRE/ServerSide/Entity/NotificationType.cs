namespace ServerSide.Entity
{
    public class NotificationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
