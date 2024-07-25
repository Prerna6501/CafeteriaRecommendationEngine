using Common.Utilities;
using Moq;
using Newtonsoft.Json;
using ServerSide;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace MenuItemRequestHandlerTests.Tests
{
    [TestClass]
    public class MenuItemRequestHandlerTests
    {
        private Mock<IMenuItemService> _menuItemServiceMock;
        private List<MenuItem> _menuItems;
        private MenuItem _menuItem;
        private MenuItem _menuItemAdded;

        [TestInitialize]
        public void Setup()
        {
            _menuItemServiceMock = new Mock<IMenuItemService>();

            _menuItem = new MenuItem
            {
                Id = 1,
                Name = "Upma",
                Price = 10,
                IsAvailable = true,
                IsDeleted = false,
                MenuItemTypeId = 1,
                CuisinePreference = "NorthIndian",
                DietPreference = "Vegetarian",
                SpiceLevel = "Medium",
                HasSweetTooth = false
            };

            _menuItemAdded = new MenuItem
            {
                Name = "Upma",
                Price = 10,
                IsAvailable = true,
                IsDeleted = false,
                MenuItemTypeId = 1,
                CuisinePreference = "NorthIndian",
                DietPreference = "Vegetarian",
                SpiceLevel = "Medium",
                HasSweetTooth = false
            };

            _menuItems = new List<MenuItem> { _menuItem, new MenuItem { Id = 2, Name = "Poha", Price = 5, IsAvailable = true, IsDeleted = false, MenuItemTypeId = 2, CuisinePreference = "NorthIndian", DietPreference = "Vegetarian", SpiceLevel = "Medium", HasSweetTooth = false } };

            SetupMockServices();
        }

        private void SetupMockServices()
        {
            _menuItemServiceMock.Setup(service => service.AddMenuItem(It.IsAny<MenuItem>())).ReturnsAsync((MenuItem menuItem) =>
            {
                _menuItems.Add(menuItem);
                return menuItem;
            });

            _menuItemServiceMock.Setup(service => service.RemoveMenuItem(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var menuItem = _menuItems.FirstOrDefault(x => x.Id == id);
                if (menuItem != null)
                {
                    menuItem.IsDeleted = true;
                    menuItem.IsAvailable = false;
                    return menuItem;
                }
                return null;
            });

            _menuItemServiceMock.Setup(service => service.UpdateAvailability(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((int id, bool availabilityStatus) =>
            {
                var menuItem = _menuItems.FirstOrDefault(x => x.Id == id);
                if (menuItem != null)
                {
                    menuItem.IsAvailable = availabilityStatus;
                    return "Successfully updated";
                }
                return "Not present at the moment";
            });

            _menuItemServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(_menuItems);
        }

        [TestMethod]
        public async Task HandleAddMenuItem_ShouldReturnSerializedMenuItem()
        {
            string parameter = "{\"Name\":\"Upma\",\"Price\":10,\"AvailabilityStatus\":true,\"MenuItemTypeId\":1,\"DietPreference\": 1,\"SpiceLevel\": 2,\"CuisinePreference\": 1, \"HasSweetTooth\": false}";

            var result = await MenuItemRequestHandler.HandleAddMenuItem(parameter, _menuItemServiceMock.Object);
            MenuItem menuItem = new MenuItem();
            if (ResponseUtils.HandleResponse(result, out string data)) { }
            var expectedJson = JsonConvert.SerializeObject(_menuItemAdded, Formatting.Indented);

            Assert.AreEqual(expectedJson, data);
        }

        [TestMethod]
        public async Task HandleViewMenuItem_ShouldReturnSerializedAvailableMenuItems()
        {
            var result = await MenuItemRequestHandler.HandleViewMenuItem(_menuItemServiceMock.Object);

            if (ResponseUtils.HandleResponse(result, out string data)) { }
            var expectedJson = JsonConvert.SerializeObject(_menuItems, Formatting.Indented);

            Assert.AreEqual(expectedJson, data);
        }

        [TestMethod]
        public async Task HandleDeleteMenuItem_ShouldReturnSerializedDeletedMenuItem()
        {
            string parameter = "1";

            var result = await MenuItemRequestHandler.HandleDeleteMenuItem(parameter, _menuItemServiceMock.Object);
            if (ResponseUtils.HandleResponse(result, out string data)) { }
            var expectedJson = JsonConvert.SerializeObject(_menuItem, Formatting.Indented);

            Assert.AreEqual(expectedJson, data);
        }

        [TestMethod]
        public async Task HandleDeleteMenuItem_InvalidID_ShouldReturnErrorMessage()
        {
            string parameter = "3";

            var result = await MenuItemRequestHandler.HandleDeleteMenuItem(parameter, _menuItemServiceMock.Object);

            Assert.AreEqual("No menuitem found", result);
        }

        [TestMethod]
        public async Task ChangeAvailability_ShouldReturnSuccessMessage()
        {
            string parameter = "1,true";

            var result = await MenuItemRequestHandler.ChangeAvailability(parameter, _menuItemServiceMock.Object);
           
            Assert.AreEqual("Successfully updated", result);
        }
    }
}
