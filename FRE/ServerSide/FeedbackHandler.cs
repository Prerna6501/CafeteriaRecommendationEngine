using Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServerSide.Entity;
using ServerSide.Services;
using ServerSide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public static class FeedbackRequestHandler
    {
        public static async Task<string> HandleGiveFeedback(string parameters, FeedbackService feedbackService)
        {
            string[] param = parameters.Split(',');
            int menuItemId = int.Parse(param[0].Trim());
            int rating = int.Parse(param[1].Trim());
            string comments = param[2].Trim();
            int userId = int.Parse(param[3].Trim());

            Feedback feedback = new Feedback { MenuItemId = menuItemId, Rating = rating, Comment = comments, CreatedDate = DateTime.Now, UserId = userId };
            await feedbackService.CreateAsync(feedback);

            return "\nFeedback given successfully.\n";
        }

        public static async Task<string> HandleViewFeedbackForItem(string parameters, FeedbackService feedbackService)
        {
            int menuItemId = int.Parse(parameters.Trim());
            var feedbacks = await feedbackService.Where(x => x.MenuItemId == menuItemId).Include(x => x.MenuItem).ToListAsync(); 
            List<FeedbackModel> feedbackModels = new List<FeedbackModel>();

            foreach (var feedback in feedbacks)
            {
                feedbackModels.Add(new FeedbackModel
                {
                    Id = feedback.Id,
                    MenuItemId = menuItemId,
                    MenuItemName = feedback.MenuItem.Name,
                    Rating = feedback.Rating,
                    Comment = feedback.Comment
                });
            }
            return JsonConvert.SerializeObject(feedbackModels, Formatting.Indented);
        }

        public static async Task<string> HandleViewFeedbackByEmployee(string parameters, FeedbackService feedbackService)
        {
            int id = int.Parse(parameters.Trim());
            var feedbacks = await feedbackService.Where(x => x.UserId == id).ToListAsync();

            return JsonConvert.SerializeObject(feedbacks);
        }
    }
}

