using System.Collections.Generic;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestaurantWebAPI.Database;
using RestaurantWebAPI.Models;

namespace RestaurantWebAPI.Services
{
    public static class MenuItemsService
    {
        public static void PostMenu()
        {
            var menu = new List<MenuItem>
            {
                new MenuItem {Id = 1, Description = "Cheeseburger", Price = 3.99},
                new MenuItem {Id = 2, Description = "Hamburger", Price = 2.99},
                new MenuItem {Id = 3, Description = "Hot Dog", Price = 2.49},
                new MenuItem {Id = 4, Description = "Grilled Chicken Sandwich", Price = 4.99},
                new MenuItem {Id = 5, Description = "French Fries", Price = 1.29},
                new MenuItem {Id = 6, Description = "Onion Rings", Price = 2.29},
                new MenuItem {Id = 7, Description = "Soft Drink", Price = 1.19},
                new MenuItem {Id = 8, Description = "Coffee", Price = 0.99},
                new MenuItem {Id = 9, Description = "Water", Price = 0.00},
                new MenuItem {Id = 10, Description = "Ice Cream Cone", Price = 1.99}
            };
            var collectionMenuItems = CollectionMenuItems();
            collectionMenuItems.InsertMany(menu);
        }

        public static List<MenuItem> GetMenuItems()
        {
            var collectionMenuItems = CollectionMenuItems();
            return collectionMenuItems.Find(x => x.Description != null).SortBy(x => x.Description).ToList();
        }

        public static MenuItem GetMenuItem(int menuItemId)
        {
            var collectionMenuItems = CollectionMenuItems();
            return collectionMenuItems.Find(item => item.Id == menuItemId).SingleOrDefault();
        }

        public static void DeleteMenuItems()
        {
            MongoAuthConnectionFactory.GetDatabase("restaurant").DropCollection("menu");
        }

        private static IMongoCollection<MenuItem> CollectionMenuItems()
        {
           return MongoAuthConnectionFactory.GetDatabase("restaurant").GetCollection<MenuItem>("menu");
        }

        public static void DeleteMenuItem(int menuItemId)
        {
            var collectionMenuItems = CollectionMenuItems();
            collectionMenuItems.FindOneAndDelete(item => item.Id == menuItemId);
        }

        public static void PostMenuItem(string menuItem)
        {
            var menuItemDeserialized = DeserializeMenuItems(menuItem);
            var collectionMenuItems = CollectionMenuItems();
            collectionMenuItems.InsertOne(menuItemDeserialized);
        }

        private static MenuItem DeserializeMenuItems(string menuItems)
        {
            menuItems = Utilities.NormalizeJsonPString(menuItems);
            return JsonConvert.DeserializeObject<MenuItem>(menuItems);
        }

    }
}