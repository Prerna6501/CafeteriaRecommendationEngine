using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class FeedbackService : GenericService<Feedback>, IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository) : base(feedbackRepository)
        {

        }
    }
}
