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

        public FixedMealService(IFixedMealRepository fixedMealRepository, IMenuItemRepository menuItemRepository, IMealTypeRepository mealTypeRepository) : base(fixedMealRepository)
        {
            _fixedMealRepository = fixedMealRepository;
            _menuItemRepository = menuItemRepository;
            _mealTypeRepository = mealTypeRepository;
        }

        public async Task<string> RolloutFinalMeal(string message)
        {
            try
            {
                var segments = message.Split(';');

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

                        var fixedMeal = new FixedMeal
                        {
                            MenuItemId = menuItem.Id,
                            MealTypeId = mealType.Id,
                            PreparedDate = DateTime.Now.AddDays(1),
                        };

                        await _fixedMealRepository.CreateAsync(fixedMeal);
                    }
                }
                return "Final Menu recorded successfully";


            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

