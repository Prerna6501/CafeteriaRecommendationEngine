namespace ServerSide.Entity
{
    public class Notification
    {
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
