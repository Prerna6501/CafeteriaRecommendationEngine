using Newtonsoft.Json;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IVotingResultService _votingResultService;
        private readonly IFixedMealService _fixedMealService;
        private readonly INotificationService _notificationService;

        public RequestHandler(IRecommendationService recommendationService, IVotingResultService votingResultService, IFixedMealService fixedMealService, INotificationService notificationService)
        {
            _recommendationService = recommendationService;
            _votingResultService = votingResultService;
            _fixedMealService = fixedMealService;
            _notificationService = notificationService;
        }

        public async Task<string> GetNotifications()
        {
            var result = await _notificationService.GetAllAsync();
            if(result == null) { return "No notifications at the moment."; }
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        public async Task<string> GetTopMenuItemsByMealType(string parameters)
        {
            string[] param = parameters.Split(',');
            int mealTypeId = int.Parse(param[0].Trim());
            int topN = int.Parse(param[1].Trim());
            var result = await _recommendationService.GetTopRecommendations(mealTypeId, topN);
            return JsonConvert.SerializeObject(result);
        }

        public async Task<string> GetVotingResults()
        {
            var result = await _votingResultService.GetVotingResults();
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        public async Task<string> RolloutChoices(string message)
        {
            return await _votingResultService.CreateVotingForRolledOutChoices(message);
        }  
        
        public async Task<string> RolloutFinalMeal(string message)
        {
            return await _fixedMealService.RolloutFinalMeal(message);
        }

        public Task<string> ViewMonthlyReport()
        {
            throw new NotImplementedException();
        }

        public async Task<string> VoteMenuItems(string parameters)
        {
           return await _votingResultService.VoteMenuItems(parameters);
        }
    }
}
