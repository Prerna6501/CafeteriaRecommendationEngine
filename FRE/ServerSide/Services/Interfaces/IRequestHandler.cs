using Common.Models;

namespace ServerSide.Services.Interfaces
{
    public interface IRequestHandler
    {
        public Task<string> AddDetailedFeedback(string parameters);
        public Task<string> RequestDetailedFeedbackFromUser(string parameters);
        public Task<string> GetDiscardItems();
        public Task<string> GetNotifications();
        public Task<string> GetTopMenuItemsByMealType(string parameters);
        public Task<string> GetVotingResults();
        public Task<string> RemoveDiscardItem(string parameters);
        public Task<string> RolloutChoices(string message);
        public Task<string> RolloutFinalMeal(string message);
        public Task<string> ViewDetailedFeedbackOfItem(string parameters);
        public Task<string> ViewMonthlyReport();
        public Task<string> VoteMenuItems(string parameters);
    }
}
