using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace ServerSide
{
    public static class MenuItemRequestHandler
    {
        public static async Task<string> HandleAddMenuItem(string parameter, IMenuItemService menuItemService)
        {

            string[] addParams = parameter.Split(',');
            var menuItemAdd = new MenuItem
            {
                Name = addParams[0],
                Price = Convert.ToInt32(addParams[1]),
                IsAvailable = Convert.ToBoolean(addParams[2]),
                IsDeleted = false,
                MenuItemTypeId = Convert.ToInt32(addParams[3])
            };
            var response = await menuItemService.AddMenuItem(menuItemAdd);
            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }

        public static async Task<string> HandleUpdateMenuItem(string parameter, IMenuItemService menuItemService)
        {
            string[] updateParams = parameter.Split(',');
            var menuItemUpdate = new MenuItem
            {
                Id = Convert.ToInt32(updateParams[0]),
                Name = updateParams[1],
                Price = Convert.ToInt32(updateParams[2]),
                IsAvailable = Convert.ToBoolean(updateParams[3]),
                IsDeleted = Convert.ToBoolean(updateParams[4]),
                MenuItemTypeId = Convert.ToInt32(updateParams[5])
            };
            var updatedResponse = await menuItemService.UpdateAsync(menuItemUpdate);
            return JsonConvert.SerializeObject(updatedResponse, Formatting.Indented);
        }

        public static async Task<string> HandleViewMenuItem(IMenuItemService menuItemService)
        {
            var response = await menuItemService.Where(x => x.IsDeleted == false && x.IsAvailable == true).ToListAsync();
            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }

        public static async Task<string> HandleDeleteMenuItem(string parameter, IMenuItemService menuItemService)
        {
            int menuItemId = Convert.ToInt32(parameter);
            var response = await menuItemService.RemoveMenuItem(menuItemId);

            if (response == null) { return "No menuitem found"; }

            else { return JsonConvert.SerializeObject(response, Formatting.Indented); }
        }

        public static async Task<string> ChangeAvailability(string parameter, IMenuItemService menuItemService)
        {
            string[] updateParams = parameter.Split(',');
            return await menuItemService.UpdateAvailability(Convert.ToInt32(updateParams[0]), Convert.ToBoolean(updateParams[1]));
        }
    }
}

