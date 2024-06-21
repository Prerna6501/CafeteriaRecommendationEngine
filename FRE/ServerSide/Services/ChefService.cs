using Newtonsoft.Json;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class ChefService : IChefService
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IVotingResultService _votingResultService;

        public ChefService(IRecommendationService recommendationService, IVotingResultService votingResultService)
        {
            _recommendationService = recommendationService;
            _votingResultService = votingResultService;
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
            return JsonConvert.SerializeObject(result);
        }
    }
}
