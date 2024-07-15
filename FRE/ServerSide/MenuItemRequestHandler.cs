using Common.CustomExceptions;
using Common.Models;
using Common.Utilities;
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
                return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(response, Formatting.Indented));
            }
            catch (EmptyArgumentException ex)
            {
                throw new EmptyArgumentException(ex.Message);
            }
        }

        public static async Task<string> HandleUpdateMenuItem(string parameter, IMenuItemService menuItemService)
        {
            try
            {
                string[] updateParams = parameter.Split(',');
                var menuItemUpdate = await menuItemService.Where(x => x.Id == Convert.ToInt32(updateParams[0])).FirstOrDefaultAsync();
                if (menuItemUpdate != null)
                {
                    menuItemUpdate.Name = updateParams[1];
                    menuItemUpdate.Price = Convert.ToInt32(updateParams[2]);
                    menuItemUpdate.IsAvailable = Convert.ToBoolean(updateParams[3]);
                    menuItemUpdate.IsDeleted = Convert.ToBoolean(updateParams[4]);
                    menuItemUpdate.MenuItemTypeId = Convert.ToInt32(updateParams[5]);

                    var updatedResponse = await menuItemService.UpdateAsync(menuItemUpdate);
                    return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(updatedResponse, Formatting.Indented));
                }
                throw new EntityNotFoundException($"Menu Item with ID: {Convert.ToInt32(updateParams[0])} not found.");
            }

            catch (EntityNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> HandleViewMenuItem(IMenuItemService menuItemService)
        {
            try
            {
                var response = await menuItemService.Where(x => x.IsDeleted == false && x.IsAvailable == true).ToListAsync();
                return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(response, Formatting.Indented));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> HandleDeleteMenuItem(string parameter, IMenuItemService menuItemService)
        {
            int menuItemId = Convert.ToInt32(parameter);
            var response = await menuItemService.RemoveMenuItem(menuItemId);

            if (response == null) { return "No menuitem found"; }

            else { return ResponseUtils.CreateSuccessJsonResponse(JsonConvert.SerializeObject(response, Formatting.Indented)); }
        }

        public static async Task<string> ChangeAvailability(string parameter, IMenuItemService menuItemService)
        {
            string[] updateParams = parameter.Split(',');
            return await menuItemService.UpdateAvailability(Convert.ToInt32(updateParams[0]), Convert.ToBoolean(updateParams[1]));
        }
    }
}

