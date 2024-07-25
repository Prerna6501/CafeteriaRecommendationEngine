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
        private readonly IProfileService _profileService;
        private readonly IVotingResultService _votingResultService;

        private static readonly List<string> PositiveKeywords = new List<string> { "good", "great", "excellent", "love", "fantastic", "happy", "delicious", "amazing", "tasty", "awesome", "wonderful", "superb", "perfect", "outstanding", "pleasing", "satisfying", "fabulous", "magnificent", "phenomenal" };
        private static readonly List<string> NegativeKeywords = new List<string> { "bad", "not good", "terrible", "awful", "horrible", "disgusting", "hate", "poor", "disappointing", "unpleasant", "displeasing", "unsatisfactory", "dreadful", "repulsive", "lousy", "miserable" };

        public RecommendationService(IMenuItemService menuItemService, IFeedbackService feedbackService, IProfileService profileService, IVotingResultService votingResultService)
        {
            _menuItemService = menuItemService;
            _feedbackService = feedbackService;
            _profileService = profileService;
            _votingResultService = votingResultService;
        }

        public async Task<string> AnalyzeSentimentForMenuItem(int id)
        {

            List<string> sentiments = new List<string>();
            List<Feedback> feedbacks = await _feedbackService.Where(x => x.MenuItemId == id).ToListAsync();
            if (feedbacks.Count == 0) { return "No feedbacks at the momemt"; }
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

        public async Task<List<MenuItemModel>> GetTopRecommendations(int mealTypeId, int topN)
        {
            int menuTypeId = mealTypeId == 1 ? (int)MenuItemEnum.Breakfast : (int)MenuItemEnum.Meals;
            List<MenuItem> menuItems = await _menuItemService.Where(x => x.MenuItemTypeId == menuTypeId).Include(x => x.MenuItemType).ToListAsync();                      
            var menuItemModel = await GetMenuItemModels(menuItems);
            
            return menuItemModel.OrderByDescending(x => x.AverageRating).Take(topN).ToList();
        }

        public async Task<(double averageRating, string sentiment)> GetMonthlySentimentRatingForMenuItem(int menuItemId)
        {
            DateTime now = DateTime.Now;
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime startOfNextMonth = startOfMonth.AddMonths(1);

            List<Feedback> feedbacks = await _feedbackService
                .Where(x => x.MenuItemId == menuItemId && x.CreatedDate >= startOfMonth && x.CreatedDate < startOfNextMonth)
                .ToListAsync();

            if (!feedbacks.Any())
            {
                return (0.0, "No feedbacks for the current month");
            }

            double averageRating = feedbacks.Average(x => x.Rating);

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

            return (averageRating, highestSentiment);
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

        public async Task<List<MenuItemModel>> GetRollOutMenuSortedByPreferences(int userId)
        {
            var userProfile = await _profileService.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if(userProfile == null)
            {
                throw new Exception("User profile not found");//handle later
            }
            List<MenuItem> rolledOutMenuItems = await _votingResultService.Where(x => x.CreatedDate.Date == DateTime.Now.Date).Include(x => x.MenuItem).ThenInclude(x => x.MenuItemType).Select(x => x.MenuItem).ToListAsync();
            var sortedRolledOutMenuItem =  await GetSortedMenuItems(rolledOutMenuItems, userProfile);
            
            return await GetMenuItemModels(sortedRolledOutMenuItem);
        }
      
        public async Task<List<MenuItemModel>> GetMenuItemModels(List<MenuItem> menuItems)
        {
            List<MenuItemModel> menuItemModel = new List<MenuItemModel>();
            foreach (MenuItem item in menuItems)
            {
                var averageScore = await CalculateScore(item);
                var sentiment = await AnalyzeSentimentForMenuItem(item.Id);
                menuItemModel.Add(new MenuItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    AverageRating = averageScore,
                    MenuItemType = item.MenuItemType.Name,
                    Price = item.Price,
                    Sentiments = sentiment
                });
            }
            return menuItemModel;
        }

        private async Task<List<MenuItem>> GetSortedMenuItems(List<MenuItem> menuItems, EmployeeProfile profile)
        {
            return menuItems
                .Select(item => new
                {
                    MenuItem = item,
                    Score = CalculatePreferenceScore(item, profile)
                })
                .OrderByDescending(x => x.Score)
                .Select(x => x.MenuItem)
                .ToList();
        }

        private int CalculatePreferenceScore(MenuItem item, EmployeeProfile profile)
        {
            int score = 0;

            if (!string.IsNullOrEmpty(item.DietPreference) && item.DietPreference.Equals(profile.DietPreference.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                score += 10;
            }

            if (!string.IsNullOrEmpty(item.SpiceLevel) && item.SpiceLevel.Equals(profile.SpiceLevel.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                score += 5;
            }

            if (!string.IsNullOrEmpty(item.CuisinePreference) && item.CuisinePreference.Equals(profile.CuisinePreference.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                score += 3;
            }

            if (item.HasSweetTooth == profile.HasSweetTooth)
            {
                score += 1;
            }

            return score;
        }
    }
}

