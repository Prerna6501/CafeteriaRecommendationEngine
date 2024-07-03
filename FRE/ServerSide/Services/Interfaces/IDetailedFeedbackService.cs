using Common.Models;
using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IDetailedFeedbackService : IGenericService<DetailedFeedback>
    {
        public Task<string> GiveDetailedFeedback(DetailedFeedbackModel feedbackModel);
    }
}
