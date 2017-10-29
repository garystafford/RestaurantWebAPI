using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Newtonsoft.Json;
using Restaurant.Order.Database;
using Restaurant.Order.Models;

namespace Restaurant.Order.Services
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
                    TimePlaced = order.TimePlaced,
                    OrderNumber = order.OrderNumber,
                    Message = "Thank you for your order."

                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    TimePlaced = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString(),
                    Message = "Sorry, an error has occurred. Please place your order again."
                };
            }


        }

        public static Models.Order GetOrder(string orderNumber)
        {
            var collectionOrders = GetCollectionOrders();
            return collectionOrders.Find(order => order.OrderNumber == orderNumber).SingleOrDefault();
        }

        public static List<Models.Order> GetOrders()
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
            AtlasConnectionFactory.GetDatabase("restaurant").DropCollection("orders");
        }

        private static IMongoCollection<Models.Order> GetCollectionOrders()
        {
            return AtlasConnectionFactory.GetDatabase("restaurant").GetCollection<Models.Order>("orders");
        }

        private static Models.Order DeserializeOrder(string restaurantOrder)
        {
            restaurantOrder = Utilities.NormalizeJsonPString(restaurantOrder);
            var order = JsonConvert.DeserializeObject<List<OrderItem>>(restaurantOrder);
            return new Models.Order(order);
        }
    }
}