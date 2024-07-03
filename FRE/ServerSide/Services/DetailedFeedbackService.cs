using Common.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class DetailedFeedbackService : GenericService<DetailedFeedback>, IDetailedFeedbackService
    {
        private readonly IDetailedFeedbackRepository _detailedFeedbackRepository;
        private readonly IDiscardItemService _discardItemService;
        public DetailedFeedbackService(IDetailedFeedbackRepository detailedFeedbackRepository, IDiscardItemService discardItemService) : base(detailedFeedbackRepository)
        {
            _detailedFeedbackRepository = detailedFeedbackRepository;
            _discardItemService = discardItemService;

        }
        public async Task<string> GiveDetailedFeedback(DetailedFeedbackModel feedbackModel)
        {
            DiscardItem? discardItem = await _discardItemService.Where(x => x.Id == feedbackModel.DiscardItemId).FirstOrDefaultAsync();
            if (discardItem == null)
            {
                return "Invalid Discard Item Id.";
            }

            await SaveFeedback(feedbackModel.DiscardItemId, 1, feedbackModel.Answer1, feedbackModel.UserId);
            await SaveFeedback(feedbackModel.DiscardItemId, 2, feedbackModel.Answer2, feedbackModel.UserId);
            await SaveFeedback(feedbackModel.DiscardItemId, 3, feedbackModel.Answer3, feedbackModel.UserId);

            return "Detailed feedback saved successfully.";
        }

        private async Task SaveFeedback(int discardItemId, int questionTypeId, string answer, int userId)
        {
            DetailedFeedback detailedFeedback = new DetailedFeedback
            {
                DiscardItemId = discardItemId,
                QuestionTypeId = questionTypeId,
                Comment = answer,
                CreatedDate = DateTime.Now,
                UserId = userId
            };

            await _detailedFeedbackRepository.CreateAsync(detailedFeedback);
        }
    }
}
