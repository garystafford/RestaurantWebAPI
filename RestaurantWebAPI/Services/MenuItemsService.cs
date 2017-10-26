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
                new MenuItem {Description = "Cheeseburger", Price = 3.99},
                new MenuItem {Description = "Hamburger", Price = 2.99},
                new MenuItem {Description = "Hot Dog", Price = 2.49},
                new MenuItem {Description = "Grilled Chicken Sandwich", Price = 4.99},
                new MenuItem {Description = "French Fries", Price = 1.29},
                new MenuItem {Description = "Onion Rings", Price = 2.29},
                new MenuItem {Description = "Soft Drink", Price = 1.19},
                new MenuItem {Description = "Coffee", Price = 0.99},
                new MenuItem {Description = "Water", Price = 0.00},
                new MenuItem {Description = "Ice Cream Cone", Price = 1.99}
            };
            var collectionMenuItems = CollectionMenuItems();
            collectionMenuItems.InsertMany(menu);
        }

        public static List<MenuItem> GetMenuItems()
        {
            var collectionMenuItems = CollectionMenuItems();
            return collectionMenuItems.Find(x => x.Description != null).SortBy(x => x.Description).ToList();
        }

        public static MenuItem GetMenuItem(string menuItemDescription)
        {
            var collectionMenuItems = CollectionMenuItems();
            return collectionMenuItems.Find(item => item.Description == menuItemDescription).SingleOrDefault();
        }

        public static void DeleteMenuItems()
        {
            MongoAuthConnectionFactory.GetDatabase("restaurant").DropCollection("menu");
        }

        private static IMongoCollection<MenuItem> CollectionMenuItems()
        {
           return AtlasConnectionFactory.GetDatabase("restaurant").GetCollection<MenuItem>("menu");
        }

        public static void DeleteMenuItem(string menuItemDescription)
        {
            var collectionMenuItems = CollectionMenuItems();
            collectionMenuItems.FindOneAndDelete(item => item.Description == menuItemDescription);
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