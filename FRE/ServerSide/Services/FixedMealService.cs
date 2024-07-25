using Common;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class FixedMealService : GenericService<FixedMeal>, IFixedMealService
    {
        private readonly IFixedMealRepository _fixedMealRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMealTypeRepository _mealTypeRepository;
        private readonly INotificationService _notificationService;

        public FixedMealService(IFixedMealRepository fixedMealRepository, IMenuItemRepository menuItemRepository, IMealTypeRepository mealTypeRepository, INotificationService notificationService) : base(fixedMealRepository)
        {
            _fixedMealRepository = fixedMealRepository;
            _menuItemRepository = menuItemRepository;
            _mealTypeRepository = mealTypeRepository;
            _notificationService = notificationService;
        }

        public async Task<string> RolloutFinalMeal(string message)
        {
            try
            {
                var segments = message.Split(';');

                var breakfastItems = new List<string>();
                var lunchItems = new List<string>();
                var dinnerItems = new List<string>();

                foreach (var segment in segments)
                {
                    var mealTypeAndItems = segment.Split(':');
                    if (mealTypeAndItems.Length != 2)
                    {
                        continue;
                    }

                    var mealTypeName = mealTypeAndItems[0];
                    var itemIds = mealTypeAndItems[1].Split(',').Select(int.Parse);

                    var mealType = await _mealTypeRepository.Where(mt => mt.Name == mealTypeName).FirstOrDefaultAsync();

                    foreach (var itemId in itemIds)
                    {
                        var menuItem = await _menuItemRepository.Where(x => x.Id == itemId).FirstOrDefaultAsync();
                        if (menuItem == null)
                        {
                            return $"MenuItem with ID {itemId} does not exist.";
                        }

                        await CreateFixedMeal(menuItem, mealType);

                        AddMenuItemToMealTypeList(mealTypeName, menuItem.Name, breakfastItems, lunchItems, dinnerItems);
                    }
                }

                var finalMessage = GenerateNotificationMessage(breakfastItems, lunchItems, dinnerItems);
                await _notificationService.CreateNotification((int)NotificationTypeEnum.FinalPreparation, finalMessage);
                return "Final Menu recorded successfully";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void AddMenuItemToMealTypeList(string mealTypeName, string menuItemName, List<string> breakfastItems, List<string> lunchItems, List<string> dinnerItems)
        {
            switch (mealTypeName.ToLower())
            {
                case "breakfast":
                    breakfastItems.Add(menuItemName);
                    break;
                case "lunch":
                    lunchItems.Add(menuItemName);
                    break;
                case "dinner":
                    dinnerItems.Add(menuItemName);
                    break;
            }
        }

        private string GenerateNotificationMessage(List<string> breakfastItems, List<string> lunchItems, List<string> dinnerItems)
        {
            return string.Format(AppConstants.FixedMealNotification, DateTime.Now.AddDays(1).ToShortDateString(), string.Join(", ", breakfastItems), string.Join(", ", lunchItems), string.Join(", ", dinnerItems));
        }

        private async Task CreateFixedMeal(MenuItem menuItem, MealType mealType)
        {
            var fixedMeal = new FixedMeal
            {
                MenuItemId = menuItem.Id,
                MealTypeId = mealType.Id,
                PreparedDate = DateTime.Now.AddDays(1),
            };

            await _fixedMealRepository.CreateAsync(fixedMeal);
        }
    }
}

