using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Menu.Database;
using Restaurant.Menu.Services;

namespace Restaurant.Menu.Tests.Services
{
    [TestClass()]
    public class MenuItemsServiceTests
    {
        private const string MenuItem1 = "{\"menuId\": \"1\", \"description\": \"MenuItemTest1\", \"price\": \"1.23\"}";
        private const string MenuItem2 = "{\"menuId\": \"2\", \"description\": \"MenuItemTest2\", \"price\": \"4.56\"}";

        private readonly MenuItemsService _menuItemsService = new MenuItemsService(new MongoAuthConnectionFactory());


        [TestInitialize()]
        public void TestInitialize()
        {
            _menuItemsService.DeleteMenuItems();
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            _menuItemsService.DeleteMenuItems();
        }

        [TestMethod()]
        public void PostMenuTest()
        {

            _menuItemsService.PostMenu();
            var menuItems = _menuItemsService.GetMenuItems();

            Assert.AreEqual(10, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemsTest()
        {
            _menuItemsService.PostMenuItem(MenuItem1);
            _menuItemsService.PostMenuItem(MenuItem2);
            var menuItems = _menuItemsService.GetMenuItems();

            Assert.AreEqual(2, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemTest()
        {
            _menuItemsService.PostMenuItem(MenuItem1);
            var menuItem = _menuItemsService.GetMenuItem(1);

            Assert.AreEqual(1, menuItem.MenuId);
            Assert.AreEqual("MenuItemTest1", menuItem.Description);
            Assert.AreEqual(1.23, menuItem.Price);
        }

        [TestMethod()]
        public void DeleteMenuItemsTest()
        {
            _menuItemsService.PostMenu();
            _menuItemsService.DeleteMenuItems();
            var menuItems = _menuItemsService.GetMenuItems();
            Assert.AreEqual(0, menuItems.Count);
        }

        [TestMethod()]
        public void DeleteMenuItemTest()
        {
            _menuItemsService.PostMenuItem(MenuItem1);
            _menuItemsService.DeleteMenuItem(1);
            var menuItem = _menuItemsService.GetMenuItem(1);
            Assert.IsNull(menuItem);
        }

        [TestMethod()]
        public void PostMenuItemTest()
        {
            _menuItemsService.PostMenuItem(MenuItem2);
            var menuItem = _menuItemsService.GetMenuItem(2);

            Assert.AreEqual(2, menuItem.MenuId);
            Assert.AreEqual("MenuItemTest2", menuItem.Description);
            Assert.AreEqual(4.56, menuItem.Price);
        }

        [TestMethod()]
        public void PutMenuItemTest()
        {
            const string menuItem2Revised = "{\"menuId\": \"2\", \"description\": \"MenuItemTest2 Revised\", \"price\": \"5.49\"}";

            _menuItemsService.PostMenuItem(MenuItem2);
            _menuItemsService.PutMenuItem(2, menuItem2Revised);

            var menuItem = _menuItemsService.GetMenuItem(2);

            Assert.AreEqual(2, menuItem.MenuId);
            Assert.AreEqual("MenuItemTest2 Revised", menuItem.Description);
            Assert.AreEqual(5.49, menuItem.Price);
        }
    }
}