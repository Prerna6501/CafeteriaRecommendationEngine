using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IMenuItemService : IGenericService<MenuItem>
    {
        public Task<MenuItem> AddMenuItem(MenuItem menuItem);
        public Task<MenuItem> RemoveMenuItem(int menuItemId);
        public Task<string> UpdateAvailability(int menuItemId, bool availability);
    }
}
