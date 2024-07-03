using Common.Enums;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class DiscardItemService : GenericService<DiscardItem>, IDiscardItemService
    {
        private readonly IDiscardItemRepository _discardItemRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRecommendationService _recommendRecommendationService;
        public DiscardItemService(IDiscardItemRepository discardItemRepository, IMenuItemRepository menuItemRepository) : base(discardItemRepository)
        {
            _discardItemRepository = discardItemRepository;
            _menuItemRepository = menuItemRepository;

        }

        public async Task<List<DiscardItemModel>> GetDiscardItemList()
        {
            var discardItemList = await _discardItemRepository.Where(x => x.CreatedDate.Month == DateTime.Now.Month).Include(x => x.MenuItem).ToListAsync();
            if (discardItemList == null || !discardItemList.Any())
            {
                await GenerateDiscardItems();
                discardItemList = await _discardItemRepository.GetAllAsync();
            }
            List<DiscardItemModel> result = new List<DiscardItemModel>();
            foreach (var item in discardItemList)
            {
                result.Add(await ConvertToDiscardItemModel(item));
            }

            return result;
        }
     
        public async Task<string> RemoveMenuItem(int discardId)
        {
            var discardItem = await _discardItemRepository.Where(x => x.Id == discardId).FirstOrDefaultAsync();
            if (discardItem != null)
            {
                var menuItem = await _menuItemRepository.Where(x => x.Id == discardItem.MenuItemId).FirstOrDefaultAsync();
                if (menuItem != null)
                {
                    menuItem.IsDeleted = true;
                    await _menuItemRepository.UpdateAsync(menuItem);
                    //TODO:notification
                }

                await UpdateDiscardItemStatus(discardId, "Removed");
                return "Sucessfullt removed";
            }
            return "Invalid DiscardID";
        }

        public async Task<string> RequestDetailedFeedback(int discardId)
        {
            var discardItem = await _discardItemRepository.Where(x => x.Id == discardId).FirstOrDefaultAsync();
            if (discardItem != null)
            {
                var menuItem = await _menuItemRepository.Where(x => x.Id == discardItem.MenuItemId).FirstOrDefaultAsync();
                if (menuItem != null)
                {
                    await UpdateDiscardItemStatus(discardId, "Feedback Requested");
                    // TODO: send notification
                    return "Sucessfully requested.";
                }
            }
            return "Invalid DiscardID";
        }

        private async Task UpdateDiscardItemStatus(int discardId, string status)
        {
            var discardItem = await _discardItemRepository.Where(x => x.Id == discardId).FirstOrDefaultAsync();
            if (discardItem != null)
            {
                discardItem.Status = status;
                await _discardItemRepository.UpdateAsync(discardItem);
            }
        }

        private async Task<DiscardItemModel> ConvertToDiscardItemModel(DiscardItem item)
        {
            return new DiscardItemModel
            {
                Id = item.Id,
                Name = item.MenuItem.Name,
                Status = item.Status,
                AverageRating = item.AverageRating,
                Sentiments = item.Sentiments
            };
        }

        private async Task<List<DiscardItem>> GenerateDiscardItems()
        {
            var allMenuItems = await _menuItemRepository.GetAllAsync();
            List<DiscardItem> discardList = new List<DiscardItem>();

            foreach (var item in allMenuItems)
            {
                var (averageRating, sentiment) = await _recommendRecommendationService.GetMonthlySentimentRatingForMenuItem(item.Id);

                if (averageRating < 2 || sentiment == "Negative")
                {
                    var discardItem = new DiscardItem
                    {
                        MenuItemId = item.Id,
                        AverageRating = averageRating,
                        Status = EnumExtensions.GetDescription(StatusEnum.Discarded),
                        Sentiments = sentiment,
                        CreatedDate = DateTime.Now
                    };
                    await _discardItemRepository.CreateAsync(discardItem);
                }
            }
            return discardList;
        }
    }
}
