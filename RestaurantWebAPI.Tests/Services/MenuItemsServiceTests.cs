using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Tests.Services
{
    [TestClass()]
    public class MenuItemsServiceTests
    {

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
        public static void PostMenuTest()
        {

            MenuItemsService.PostMenu();
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(10, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemsTest()
        {
            var menuItem = "{ \"Description\": \"GetMenuItemsTest1\", \"Price\": \"9.99\" }";
            MenuItemsService.PostMenuItem(menuItem);
            menuItem = "{ \"Description\": \"GetMenuItemsTest2\", \"Price\": \"9.99\" }";
            MenuItemsService.PostMenuItem(menuItem);
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(2, menuItems.Count);
        }

        [TestMethod()]
        public void GetMenuItemTest()
        {
            const string menuItem = "{ \"Description\": \"GetMenuItemTest\", \"Price\": \"9.99\" }";
            MenuItemsService.PostMenuItem(menuItem);
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(1, menuItems.Count);
            Assert.AreEqual("GetMenuItemTest", menuItems[0].Description);
            Assert.AreEqual(9.99, menuItems[0].Price);
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
            const string menuItem = "{ \"Description\": \"DeleteMenuItemTest\", \"Price\": \"9.99\" }";
            MenuItemsService.PostMenuItem(menuItem);
            MenuItemsService.DeleteMenuItem("DeleteMenuItemTest");
            var menuItems = MenuItemsService.GetMenuItems();
            Assert.AreEqual(0, menuItems.Count);
        }

        [TestMethod()]
        public void PostMenuItemTest()
        {
            const string menuItem = "{ \"Description\": \"PostMenuItemTest\", \"Price\": \"9.99\" }";
            MenuItemsService.PostMenuItem(menuItem);
            var menuItems = MenuItemsService.GetMenuItems();

            Assert.AreEqual(1, menuItems.Count);
            Assert.AreEqual("PostMenuItemTest", menuItems[0].Description);
            Assert.AreEqual(9.99, menuItems[0].Price);
        }
    }
}