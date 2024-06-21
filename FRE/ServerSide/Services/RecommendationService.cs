using Common.Enums;
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

        public async Task<List<MenuItem>> GetTopRecommendations(int mealTypeId ,int topN)
        {
            int menuItemId = mealTypeId == 1 ? (int)MenuItemEnum.Breakfast : (int)MenuItemEnum.Meals;
            List<MenuItem> menuItems = await _menuItemService.Where(x => x.MenuItemTypeId ==menuItemId).ToListAsync();
            var recommendedList = menuItems.Select(item => new
            {
                Item = item,
                Score = CalculateScore(item)
            })
            .OrderByDescending(x => x.Score)
            .Take(topN)
            .Select(x => x.Item)
            .ToList();

            return recommendedList;
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

        private double CalculateScore(MenuItem item)
        {
            if (item.Feedbacks.Count == 0) return 0;
            
            double averageRating = item.Feedbacks.Select(x => x.Rating).Average();
            return averageRating;
        }
    }
}

