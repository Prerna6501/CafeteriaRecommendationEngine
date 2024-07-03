using Common.Models;
using ServerSide.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Services.Interfaces
{
    public interface IDetailedFeedbackService : IGenericService<DetailedFeedback>
    {
        public Task<string> GiveDetailedFeedback(DetailedFeedbackModel feedbackModel);
    }
}
