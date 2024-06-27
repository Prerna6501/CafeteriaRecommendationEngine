using Common.Models;
using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IRecommendationService
    {
        public Task<string> AnalyzeSentimentForMenuItem(int id);
        public Task<List<MenuItemModel>> GetTopRecommendations(int mealTypeId, int topN);
    }
}
