using Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServerSide.Entity;
using ServerSide.Services;
using Common.CustomExceptions;
using Common.Utilities;

namespace ServerSide
{
    public static class FeedbackRequestHandler
    {
        public static async Task<string> HandleGiveFeedback(string parameters, FeedbackService feedbackService)
        {
            try
            {
                if(parameters == null) {  throw new EmptyArgumentException("Empty parameters...."); }
                string[] param = parameters.Split(',');
                int menuItemId = int.Parse(param[0].Trim());
                int rating = int.Parse(param[1].Trim());
                string comments = param[2].Trim();
                int userId = int.Parse(param[3].Trim());

                Feedback feedbackPresent = await feedbackService.Where(x => x.MenuItemId == menuItemId && x.UserId == userId).FirstOrDefaultAsync();
                if (feedbackPresent != null)
                {
                    return ResponseUtils.CreateSuccessJsonResponse("\nFeedback already given for this item.\n");
                }
                else
                {
                    Feedback feedback = new Feedback { MenuItemId = menuItemId, Rating = rating, Comment = comments, CreatedDate = DateTime.Now, UserId = userId };
                    await feedbackService.CreateAsync(feedback);

                    return ResponseUtils.CreateSuccessJsonResponse("\nFeedback given successfully.\n");
                }              
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid input format.", ex.Message);
            }
            catch (EmptyArgumentException ex)
            {
                throw new EmptyArgumentException("Input cannot be null.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while giving feedback.", ex);
            }
        }

        public static async Task<string> HandleViewFeedbackForItem(string parameters, FeedbackService feedbackService)
        {
            try
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
                return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(feedbackModels, Formatting.Indented));
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid input format.", ex);
            }            
            catch (Exception ex)
            {
                throw new Exception("An error occurred while viewing feedback for item.", ex);
            }
        }

        public static async Task<string> HandleViewFeedbackByEmployee(string parameters, FeedbackService feedbackService)
        {
            try
            {
                int id = int.Parse(parameters.Trim());
                var feedbacks = await feedbackService.Where(x => x.UserId == id).Include(x => x.MenuItem).ToListAsync();
                List<FeedbackModel> feedbackModels = new List<FeedbackModel>();
                foreach (var feedback in feedbacks)
                {
                    feedbackModels.Add(new FeedbackModel
                    {
                        Id = feedback.Id,
                        Comment = feedback.Comment,
                        MenuItemName = feedback.MenuItem.Name,
                        Rating = feedback.Rating,
                        MenuItemId = feedback.MenuItem.Id,
                    });
                }
                return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(feedbackModels, Formatting.Indented));
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid input format.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while viewing feedback by employee.", ex);
            }
        }
    }
}

