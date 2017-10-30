using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Restaurant.Menu.Database;
using Restaurant.Menu.Models;

namespace Restaurant.Menu.Services
{
    public class MenuItemsService
    {
        private readonly IMongoConnectionFactory _mongoConnectionFactory;

        public MenuItemsService(IMongoConnectionFactory mongoConnectionFactory)
        {
            _mongoConnectionFactory = mongoConnectionFactory;
        }

        public void PostMenu()
        {
            var menu = new List<MenuItem>
            {
                new MenuItem {MenuId = 1, Description = "Cheeseburger", Price = 3.99},
                new MenuItem {MenuId = 2, Description = "Hamburger", Price = 2.99},
                new MenuItem {MenuId = 3, Description = "Hot Dog", Price = 2.49},
                new MenuItem {MenuId = 4, Description = "Grilled Chicken Sandwich", Price = 4.99},
                new MenuItem {MenuId = 5, Description = "French Fries", Price = 1.29},
                new MenuItem {MenuId = 6, Description = "Onion Rings", Price = 2.29},
                new MenuItem {MenuId = 7, Description = "Soft Drink", Price = 1.19},
                new MenuItem {MenuId = 8, Description = "Coffee", Price = 0.99},
                new MenuItem {MenuId = 9, Description = "Water", Price = 0.00},
                new MenuItem {MenuId = 10, Description = "Ice Cream Cone", Price = 1.99}
            };

            var collection = GetMenuItemsCollection();
            collection.InsertMany(menu);
        }

        private IMongoDatabase GetMongoDatabase()
        {
            return _mongoConnectionFactory.GetDatabase("restaurantMenu");
        }

        private IMongoCollection<MenuItem> GetMenuItemsCollection()
        {
            return GetMongoDatabase().GetCollection<MenuItem>("menus");
        }

        private IMongoCollection<BsonDocument> GetMenuItemsCollectionAsBson()
        {
            return GetMongoDatabase().GetCollection<BsonDocument>("menus");
        }

        public List<MenuItem> GetMenuItems()
        {
            var collection = GetMenuItemsCollection();
            return collection.Find(x => x.Description != null).SortBy(x => x.Description).ToList();
        }

        public MenuItem GetMenuItem(int menuId)
        {
            var collection = GetMenuItemsCollection();
            return collection.Find(item => item.MenuId == menuId).SingleOrDefault();
        }

        public void DeleteMenuItems()
        {
            _mongoConnectionFactory.GetDatabase("restaurantMenu").DropCollection("menus");
        }

        public void DeleteMenuItem(int menuId)
        {
            var collection = GetMenuItemsCollection();
            collection.FindOneAndDelete(item => item.MenuId == menuId);
        }

        public void PostMenuItem(string menuItem)
        {
            var menuItemDeserialized = DeserializeMenuItems(menuItem);
            var collection = GetMenuItemsCollection();
            collection.InsertOne(menuItemDeserialized);
        }

        public void PutMenuItem(int menuId, string menuItem)
        {
            var menuItemDeserialized = DeserializeMenuItems(menuItem);
            var collection = GetMenuItemsCollectionAsBson();

            var filter = Builders<BsonDocument>.Filter.Eq("menu_id", menuId);
            var update = Builders<BsonDocument>.Update
                .Set("description", menuItemDeserialized.Description)
                .Set("price", menuItemDeserialized.Price)
                .CurrentDate(DateTime.Now.ToJson());
            var result = collection.UpdateOne(filter, update);
            Console.Write(result.IsAcknowledged);
        }

        private static MenuItem DeserializeMenuItems(string menuItems)
        {
            return JsonConvert.DeserializeObject<MenuItem>(menuItems);
        }
    }
}