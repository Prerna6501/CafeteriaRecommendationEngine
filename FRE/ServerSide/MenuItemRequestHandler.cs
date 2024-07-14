using Common.Models;
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
            try
            {
            CreateMenuItemModel createMenuItemModel = JsonConvert.DeserializeObject<CreateMenuItemModel>(parameter);
            var menuItemAdd = new MenuItem
            {
                Name = createMenuItemModel.Name,
                Price = createMenuItemModel.Price,
                IsAvailable = createMenuItemModel.AvailabilityStatus,
                IsDeleted = false,
                MenuItemTypeId = createMenuItemModel.MenuItemTypeId,
                DietPreference = createMenuItemModel.DietPreference.ToString(),
                SpiceLevel = createMenuItemModel.SpiceLevel.ToString(),
                CuisinePreference = createMenuItemModel.CuisinePreference.ToString(),
                HasSweetTooth = createMenuItemModel.HasSweetTooth
            };
            var response = await menuItemService.AddMenuItem(menuItemAdd);
            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
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

