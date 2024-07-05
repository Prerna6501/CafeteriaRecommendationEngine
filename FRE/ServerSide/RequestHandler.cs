using Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IVotingResultService _votingResultService;
        private readonly IFixedMealService _fixedMealService;
        private readonly INotificationService _notificationService;
        private readonly IDetailedFeedbackService _detailedFeedbackService;
        private readonly IDiscardItemService _discardItemService;
        private readonly IProfileService _profileService;

        public RequestHandler(IRecommendationService recommendationService, IVotingResultService votingResultService, IFixedMealService fixedMealService, INotificationService notificationService, IDetailedFeedbackService detailedFeedbackService, IDiscardItemService discardItemService, IProfileService profileService)
        {
            _recommendationService = recommendationService;
            _votingResultService = votingResultService;
            _fixedMealService = fixedMealService;
            _notificationService = notificationService;
            _detailedFeedbackService = detailedFeedbackService;
            _discardItemService = discardItemService;
            _profileService = profileService;
        }

        public async Task<string> AddDetailedFeedback(string parameters)
        {
            DetailedFeedbackModel detailedFeedbackModel = JsonConvert.DeserializeObject<DetailedFeedbackModel>(parameters);
            return await _detailedFeedbackService.GiveDetailedFeedback(detailedFeedbackModel);
        }

        public async Task<string> RequestDetailedFeedbackFromUser(string parameters)
        {
            int discardId = int.Parse(parameters.Trim());
            return await _discardItemService.RequestDetailedFeedback(discardId);
        }

        public async Task<string> GetDiscardItems()
        {
            var result = await _discardItemService.GetDiscardItemList();
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        public async Task<string> GetNotifications()
        {
            var result = await _notificationService.GetAllAsync();
            var notificationList = JsonConvert.SerializeObject(result, Formatting.Indented);

            if (string.IsNullOrEmpty(notificationList))
            {
                return "No notifications at the moment.";
            }

            return notificationList;
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

        public async Task<string> RemoveDiscardItem(string parameters)
        {
            int discardId = int.Parse(parameters.Trim());
            return await _discardItemService.RemoveMenuItem(discardId);
        }

        public async Task<string> RolloutChoices(string message)
        {
            return await _votingResultService.CreateVotingForRolledOutChoices(message);
        }

        public async Task<string> RolloutFinalMeal(string message)
        {
            return await _fixedMealService.RolloutFinalMeal(message);
        }

        public async Task<string> ViewDetailedFeedbackOfItem(string parameters)
        {
            int discardId = int.Parse(parameters.Trim());
            //var detailedFeedback = _detailedFeedbackService.Where(x => x.DiscardItemId== discardId).Include(x => x.QuestionType).Include(y => y.DiscardItem).ThenInclude(x => x.MenuItem);
            var detailedFeedback = await _detailedFeedbackService.Where(x => x.DiscardItemId == discardId).Include(x => x.QuestionType).ToListAsync();
            List<DetailedFeedbackViewModel> result = new List<DetailedFeedbackViewModel>();
            foreach (var item in detailedFeedback)
            {
                result.Add(new DetailedFeedbackViewModel
                {
                    Id = item.Id,
                    DiscardItemId = item.DiscardItemId,
                    UserId = item.UserId,
                    Comment = item.Comment,
                    Question = item.QuestionType.Question
                });
            }
            var serilizedResult = JsonConvert.SerializeObject(result, Formatting.Indented);
            return serilizedResult;
        }

        public Task<string> ViewMonthlyReport()
        {
            throw new NotImplementedException();
        }

        public async Task<string> VoteMenuItems(string parameters)
        {
            return await _votingResultService.VoteMenuItems(parameters);
        }

        public async Task<string> SetupProfile(string parameters)
        {
            EmployeeProfileModel employeeProfileModel = JsonConvert.DeserializeObject<EmployeeProfileModel>(parameters);
            return await _profileService.SetupProfile(employeeProfileModel);
        }
    }
}
