using Common.Enums;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IFeedbackService _feedbackService;

        //Add more
        private static readonly List<string> PositiveKeywords = new List<string> { "good", "great", "excellent", "love", "fantastic", "happy", "delicious","amazing"};
        private static readonly List<string> NegativeKeywords = new List<string> { "bad", "not good", "terrible", "awful", "horrible", "disgusting", "hate", "poor" };
        
        public RecommendationService(IMenuItemService menuItemService, IFeedbackService feedbackService)
        {
            _menuItemService = menuItemService;
            _feedbackService = feedbackService;
        }

        public async Task<string> AnalyzeSentimentForMenuItem(int id)
        {

            List<string> sentiments = new List<string>();
            List<Feedback> feedbacks = await _feedbackService.Where(x => x.MenuItemId == id).ToListAsync();

            Dictionary<string, int> sentimentCounts = new Dictionary<string, int>
                {
                    { "Positive", 0 },
                    { "Negative", 0 },
                    { "Neutral", 0 }
                };

            foreach (Feedback feedback in feedbacks)
            {
                string sentiment = AnalyzeSentimentForComment(feedback.Comment);
                sentimentCounts[sentiment]++;
            }
            string highestSentiment = sentimentCounts.OrderByDescending(x => x.Value).First().Key;

            return highestSentiment;
        }

        public async Task<List<MenuItemModel>> GetTopRecommendations(int mealTypeId ,int topN)
        {
            int menuTypeId = mealTypeId == 1 ? (int)MenuItemEnum.Breakfast : (int)MenuItemEnum.Meals;
            List<MenuItem> menuItems = await _menuItemService.Where(x => x.MenuItemTypeId == menuTypeId).Include(x => x.MenuItemType).ToListAsync();
            List<MenuItemModel> menuItemModel = new List<MenuItemModel>();
            foreach (MenuItem item in menuItems)
            {
                var averageScore = await CalculateScore(item);
                menuItemModel.Add(new MenuItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    AverageRating = averageScore,
                    MenuItemType = item.MenuItemType.Name,
                    Price = item.Price,
                    Sentiments = "TBC"
                });
            }

            return menuItemModel.OrderByDescending(x => x.AverageRating).Take(topN).ToList();
        }

        private string AnalyzeSentimentForComment(string comment)
        {
            string lowerComment = comment.ToLower();

            if (PositiveKeywords.Any(keyword => lowerComment.Contains(keyword)))
            {
                return "Positive";
            }
            else if (NegativeKeywords.Any(keyword => lowerComment.Contains(keyword)))
            {
                return "Negative";
            }
            else
            {
                return "Neutral";
            }
        }

        private async Task<double> CalculateScore(MenuItem item)
        {
            List<Feedback> feedbacks = await _feedbackService.Where(x => x.MenuItemId == item.Id).ToListAsync();
            if (feedbacks.Any())
            {
                double averageRating = feedbacks.Select(x => x.Rating).Average();
                return averageRating;
            }
            else
            {
                return 0.0;
            }
        }
    }
}

