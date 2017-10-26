using System;
using System.Collections.Generic;
using System.Globalization;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestaurantWebAPI.Database;
using RestaurantWebAPI.Models;

namespace RestaurantWebAPI.Services
{
    public static class OrdersService
    {

        public static OrderResponse PostOrder(string restaurantOrder)
        {
            try
            {
                var order = DeserializeOrder(restaurantOrder);
                var collectionOrders = GetCollectionOrders();
                collectionOrders.InsertOne(order);

                return new OrderResponse
                {
                    OrderDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    OrderNumber = order.OrderNumber,
                    OrderMessage = "Thank you for your order."

                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    OrderDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    OrderNumber = Guid.NewGuid().ToString(),
                    OrderMessage = "Sorry, an error has occurred. Please place your order again."
                };
            }


        }

        public static Order GetOrder(string orderNumber)
        {
            var collectionOrders = GetCollectionOrders();
            return collectionOrders.Find(order => order.OrderNumber == orderNumber).SingleOrDefault();
        }

        public static List<Order> GetOrders()
        {
            var collectionOrders = GetCollectionOrders();
            return collectionOrders.Find(order => order.OrderNumber != null).ToList();
        }

        public static void DeleteOrder(string orderNumber)
        {
            var collectionOrders = GetCollectionOrders();
            collectionOrders.FindOneAndDelete(order => order.OrderNumber == orderNumber);
        }

        public static void DeleteOrders()
        {
            MongoAuthConnectionFactory.GetDatabase("restaurant").DropCollection("orders");
        }

        private static Order DeserializeOrder(string restaurantOrder)
        {
            restaurantOrder = Utilities.NormalizeJsonPString(restaurantOrder);
            var order = JsonConvert.DeserializeObject<List<OrderItem>>(restaurantOrder);
            return new Order(order);
        }

        private static IMongoCollection<Order> GetCollectionOrders()
        {
            return AtlasConnectionFactory.GetDatabase("restaurant").GetCollection<Order>("orders");
        }
    }
}