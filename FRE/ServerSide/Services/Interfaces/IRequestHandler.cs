namespace ServerSide.Services.Interfaces
{
    public interface IRequestHandler
    {
        public Task<string> AddDetailedFeedback(string parameters);
        public Task<string> GetNotifications();
        public Task<string> GetTopMenuItemsByMealType(string parameters);
        public Task<string> GetVotingResults();
        public Task<string> RolloutChoices(string message);
        public Task<string> RolloutFinalMeal(string message);
        public Task<string> ViewMonthlyReport();
        public Task<string> VoteMenuItems(string parameters);
    }
}
