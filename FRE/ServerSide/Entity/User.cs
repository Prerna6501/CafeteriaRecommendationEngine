namespace ServerSide.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserTypeId { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public UserType UserType { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<VotingResult> VotingResults { get; set; }
    }
}
