using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class MenuItemService : GenericService<MenuItem>, IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository) : base(menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
    }
}
