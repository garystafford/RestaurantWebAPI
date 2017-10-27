using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Tests.Services
{
    [TestClass()]
    public class MenuItemsServiceTests
    {
        private readonly string _menuItem1 = "{ \"id\": \"1\", \"description\": \"MenuItemTest1\", \"price\": \"1.23\" }";
        private readonly string _menuItem2 = "{ \"id\": \"2\", \"description\": \"MenuItemTest2\", \"price\": \"4.56\" }";


        [TestInitialize()]
        public void TestInitialize()
        {
            MenuItemsService.DeleteMenuItems();
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            MenuItemsService.DeleteMenuItems();
        }

        [TestMethod()]
        public void PostMenuTest()
        {

            MenuItemsService.PostMenu();
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(10, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemsTest()
        {
            MenuItemsService.PostMenuItem(_menuItem1);
            MenuItemsService.PostMenuItem(_menuItem2);
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(2, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemTest()
        {
            MenuItemsService.PostMenuItem(_menuItem1);
            var menuItem = MenuItemsService.GetMenuItem(1);

            Assert.AreEqual(1, menuItem.Id);
            Assert.AreEqual("MenuItemTest1", menuItem.Description);
            Assert.AreEqual(1.23, menuItem.Price);
        }

        [TestMethod()]
        public void DeleteMenuItemsTest()
        {
            MenuItemsService.PostMenu();
            MenuItemsService.DeleteMenuItems();
            var menuItems = MenuItemsService.GetMenuItems();
            Assert.AreEqual(0, menuItems.Count);
        }

        [TestMethod()]
        public void DeleteMenuItemTest()
        {
            MenuItemsService.PostMenuItem(_menuItem1);
            MenuItemsService.DeleteMenuItem(1);
            var menuItem = MenuItemsService.GetMenuItem(1);
            Assert.IsNull(menuItem);
        }

        [TestMethod()]
        public void PostMenuItemTest()
        {
            MenuItemsService.PostMenuItem(_menuItem2);
            var menuItem = MenuItemsService.GetMenuItem(2);

            Assert.AreEqual(2, menuItem.Id);
            Assert.AreEqual("MenuItemTest2", menuItem.Description);
            Assert.AreEqual(4.56, menuItem.Price);
        }
    }
}