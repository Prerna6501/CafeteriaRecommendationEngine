using Common;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class MenuItemService : GenericService<MenuItem>, IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly INotificationService _notificationService;

        public MenuItemService(IMenuItemRepository menuItemRepository, INotificationService notificationService) : base(menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            _notificationService = notificationService;
        }

        public async Task<MenuItem> AddMenuItem(MenuItem menuItem)
        {
            var response = await _menuItemRepository.CreateAsync(menuItem);
            await _notificationService.CreateNotification((int)NotificationTypeEnum.NewItemAdded, string.Format(AppConstants.AddMenuItemNotification, response.Name));

            return response;
        }

        public async Task<MenuItem> RemoveMenuItem(int menuItemId)
        {
            var itemToBeDeleted = _menuItemRepository.Where(x => x.Id == menuItemId).FirstOrDefault();
            if (itemToBeDeleted == null) { return null; }
            itemToBeDeleted.IsDeleted = true;
            itemToBeDeleted.IsAvailable = false;
            await _notificationService.CreateNotification((int)NotificationTypeEnum.Deleted, string.Format(AppConstants.DeletedMenuItemNotification, itemToBeDeleted.Name));

            return await _menuItemRepository.UpdateAsync(itemToBeDeleted);
        }

        public async Task<string> UpdateAvailability(int menuItemId, bool availability)
        {
            var menuItemTobeUpdated = await _menuItemRepository.Where(x => x.Id == menuItemId).FirstOrDefaultAsync();
            if (menuItemTobeUpdated != null)
            {
                menuItemTobeUpdated.IsAvailable = availability;
                await _menuItemRepository.UpdateAsync(menuItemTobeUpdated);

                string messageFormat = availability == true ? AppConstants.AvailabilityMenuItemNotification : AppConstants.UnavailabilityMenuItemNotification;
                await _notificationService.CreateNotification((int)NotificationTypeEnum.Deleted, string.Format(messageFormat, menuItemTobeUpdated.Name));

                return "Sucessfullyy updated";
            }
            else
            {
                return "Not present at the moment";
            }
        }
    }
}
