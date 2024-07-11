using Common.Models;

namespace ServerSide.Services.Interfaces
{
    public interface IRecommendationService
    {
        public Task<string> AnalyzeSentimentForMenuItem(int id);
        public Task<List<MenuItemModel>> GetTopRecommendations(int mealTypeId, int topN);
        public Task<(double averageRating, string sentiment)> GetMonthlySentimentRatingForMenuItem(int menuItemId);
        public Task<List<MenuItemModel>> GetRollOutMenuSortedByPreferences(int userId);
    }
}
